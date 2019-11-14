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
    //Keeps track of how close boids need to be to be detected
    public float detectionRange;
    //This controls the field of view of the boid
    public float viewAngle;

    //Sets how much of an affect each rule will have on the rotation of the boid
    public float alignmentStrength;
    public float approachStrength;

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
    #endregion

    #region My Functions
    //Applies all three boid rules
    private void boidRules()
    {
        //Rotates from where it's currently pointing towards where alignment wants the boid to point, alignment strength sets the max rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, alignment(), alignmentStrength * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, approach(), approachStrength * Time.deltaTime);
    }
    //Checks if the distance to the other boid is less than the boid's detection range
    //Then checks if the other boid is within the boid's view angle (it can't look directly behind itself)
    private Quaternion alignment()
    {
        if (boids.Count > 1)
        {
            //Used to store rotation
            Quaternion averageHeading = transform.rotation;
            //Stores the number of times that an object has had it's rotation added to the average. This is used to average future rotations in.
            float j = 1f;
            //Loops through all boids, including itself
            for (int i = 0; i < boids.Count; i++)
            {
                /*Since the list of boids includes this boid, and this boid's rotation was already averaged in when the
                  quaternion was created, we don't need to use it again
                */
                if (boids[i].GetInstanceID() == gameObject.GetInstanceID())
                {
                    continue;
                }
                
                //Checks if the boid is within a specified distance and if the angle from the boid's view to the 
                if (Vector3.Distance(transform.position, boids[i].transform.position) <= detectionRange &&
                    Quaternion.Angle(transform.rotation, boids[i].transform.rotation) <= viewAngle)
                {
                    averageHeading = Quaternion.Slerp(averageHeading, boids[i].transform.rotation, (1f / ++j));
                }
            }
            return averageHeading;
        }
        else
        {
            return transform.rotation;
        }  
    }
    //Calculates the rotation the boid would have to turn to to get closer to the center of the other boids
    private Quaternion approach()
    {
        if (boids.Count < 1)
        {
            return transform.rotation;
        }
        else
        {
            Vector3 averageFlockPos = new Vector3(0, 0, 0);
            //i stores the number of gameObjects that are included in the average
            int i = 1;
            foreach (GameObject gameObj in boids)
            {
                //Checks if the gameobject can be seen by the boid (as I explained in the alignment function above)
                if (Vector3.Distance(transform.position, gameObj.transform.position) <= detectionRange &&
                    Quaternion.Angle(transform.rotation, gameObj.transform.rotation) <= viewAngle)
                {
                    //Gets the total position of boids around this component's boid
                    averageFlockPos += gameObj.transform.position;
                    ++i;
                }
            }
            //Averages the total
            averageFlockPos /= i;
            return Quaternion.LookRotation(averageFlockPos - transform.position, Vector3.up);
        }
    }
    #endregion
}
