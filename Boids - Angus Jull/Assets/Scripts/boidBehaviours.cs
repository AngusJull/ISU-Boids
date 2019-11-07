//Created by Angus Jull on November 7th 2019
//Gives the boids their behaviours, including forward movement and the three boid rules
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidBehaviours : MonoBehaviour
{
    #region Variables
    public float moveSpeed;
    public float rotateSpeed;
    public float detectionRange;
    public float viewAngle;

    private float preferedDirection;
    private Vector3 direction = new Vector3(0, 0, 0);
    #endregion
    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveForward();
        rotateBy(rotateSpeed);
        
    }
    //Applies all three boid rules 
    private float boidRules()
    {
        GameObject[] boids = GameObject.FindGameObjectsWithTag("Boid");
        for (int i = 0; i < boids.Length; i++)
        {
            //Checks if the distance to the other boid is less than the boid's detection range
            //Then checks if the other boid is within the boid's view angle (it can't look directly behind itself)
            if  (Vector3.Distance(transform.position, boids[i].transform.position) <= detectionRange && 
                Mathf.Abs(Vector3.Angle(transform.position, boids[i].transform.position)) < viewAngle)
            {
                return 0;
            }
        }
        return 0;
    }
    private void moveForward()
    {
        transform.position += transform.up * moveSpeed;
    }
    private void rotateBy(float degrees)
    {
        direction.z += degrees;
        if (direction.z > 360)
        {
            direction.z -= 360;
        }
        else if (direction.z < 360)
        {
            direction.z += 360;
        }
        transform.eulerAngles = direction;
    }
    #endregion
}
