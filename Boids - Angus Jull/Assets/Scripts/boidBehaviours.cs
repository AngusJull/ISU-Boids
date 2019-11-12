//Created by Angus Jull on November 7th 2019
//Gives the boids their behaviours, including forward movement and the three boid 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidBehaviours : MonoBehaviour
{
    #region Variables
    ///List<GameObject> boids = new List<GameObject>();
    //MoveSpeed stores how fast the boid should move forward
    public float moveSpeed;
    //RotateSpeed defines the maximum rotation speed of the boid
    public float rotateSpeed;
    //Keeps track of how close boids need to be to be detected
    public float detectionRange;
    //This controls the field of view of the boid
    public float viewAngle;

    //Sets how much of an affect each rule will have on the rotation of the boid
    public float alignmentStrength;

    //This is to keep track of how much the boid would like to rotate
    private float preferedRotation = 0;
    //This keeps track of the rotation of the boid
    private Vector3 direction = new Vector3(0, 0, 0);

    [HideInInspector]
    public List<GameObject> boids;
    #endregion

    #region Unity Functions
    private void Start()
    {
        boids = GameObject.FindGameObjectWithTag("Boid Creator").GetComponent<createBoids>().curBoids;
    }
    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        boidRules();
        //Moves the boid forward according to it's direction
        transform.position += transform.up * moveSpeed * Time.deltaTime;
        if (boids == null)
        {
            boids = GameObject.FindGameObjectWithTag("Boid Creator").GetComponent<createBoids>().curBoids;
        }
    }
    //Applies all three boid rules
    private void boidRules()
    {
        //Sets the prefered rotation back to 0 (it still has last time's value
        preferedRotation = 0;
        //Changes the prefered rotation by what alignment wants
        preferedRotation += alignment();
        Debug.Log(preferedRotation);
        //Rotates by the amount that each rule wants it to, clamped to the max rotation speed
        transform.Rotate(Vector3.forward, Mathf.Clamp(preferedRotation, -rotateSpeed, rotateSpeed) * Time.deltaTime);
    }
    //Checks if the distance to the other boid is less than the boid's detection range
    //Then checks if the other boid is within the boid's view angle (it can't look directly behind itself)
    private float alignment()
    {
        Vector3 averageHeading = new Vector3(0, 0, 0);
        int i = 0;
        foreach (GameObject obj in boids)
        {
            if (Vector3.Magnitude(transform.position - obj.transform.position) <= detectionRange &&
                Vector3.Angle(transform.position, obj.transform.position) <= viewAngle)
            {
                averageHeading += obj.transform.eulerAngles;
                i++;
            }
        }
        if (i != 0)
        {
            averageHeading.z /= i;
            print(transform.eulerAngles - averageHeading);
            return (Mathf.Clamp((transform.eulerAngles - averageHeading).z, -1 * alignmentStrength, alignmentStrength));
        }
        else
        {
            return 0;
        }

    }
    #endregion
}
