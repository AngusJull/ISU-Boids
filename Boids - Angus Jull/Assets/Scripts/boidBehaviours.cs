//Created by Angus Jull on November 7th 2019
//Gives the boids their behaviours, including forward movement and the three boid 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidBehaviours : MonoBehaviour
{
    #region Variables
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
    private float preferedRotation;
    //This keeps track of the rotation of the boid
    private Vector3 direction = new Vector3(0, 0, 0);
    #endregion
    #region Unity Functions
    // Start is called before the first frame update
    private void Start()
    {
        Vector3 vector3 = new Vector3(0, 0, Random.Range(-360, 360));
        transform.eulerAngles = vector3;
        preferedRotation = 0;
    }
    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        moveForward();
        boidRules();
    }
    //Applies all three boid rules
    private void boidRules()
    {
        GameObject[] boids = GameObject.FindGameObjectsWithTag("Boid");
        //Sets the prefered rotation back to 0 (it still has last time's value
        preferedRotation = 0;
        //Changes the prefered rotation by what alignment wants
        preferedRotation += alignment(boids);
        //Rotates by the amount that each rule wants it to, clamped to the max rotation speed
        //ERROR IS HERE
        rotateBy(Mathf.Clamp(preferedRotation, -1 * rotateSpeed, rotateSpeed));
    }
    //Checks if the distance to the other boid is less than the boid's detection range
    //Then checks if the other boid is within the boid's view angle (it can't look directly behind itself)
    private float alignment(GameObject[] boid)
    {
        Vector3 averageHeading = new Vector3(0, 0, 0);
        int i = 0;
        foreach (GameObject obj in boid)
        {
            if (Vector3.Distance(transform.position, obj.transform.position) <= detectionRange &&
                Mathf.Abs(Vector3.Angle(transform.position, obj.transform.position)) < viewAngle)
            {
                averageHeading.z += obj.transform.eulerAngles.z;
                i++;
            }
        }
        averageHeading.z /= i;
        return (Mathf.Clamp(Vector3.SignedAngle(transform.eulerAngles, averageHeading, Vector3.up), -1 * alignmentStrength, alignmentStrength)); 
    }
    // Moves the boid forward (taking into consideration it's rotation)
    private void moveForward()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
    //Rotates the boid by a certain amount (regardless of the maximum rotation speed of the boid
    private void rotateBy(float degrees)
    {
        direction = transform.eulerAngles;
        direction.z += degrees * Time.deltaTime;
        if (direction.z > 360)
        {
            direction.z  -= 360;
        }
        else if (direction.z < 360)
        {
            direction.z += 360;
        }
        Debug.Log(direction.z);
        transform.eulerAngles = direction;
    }
    #endregion
}
