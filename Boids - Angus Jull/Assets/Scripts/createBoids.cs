//Created by Angus Jull on November 5th 2019, creates a certain number of boids when the program starts,
//and keeps the number of boids there are and the amount the user wants the same at all times
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createBoids : MonoBehaviour
{
    #region Variables
    //This is the boid prefab that is used to create all other boids
    public GameObject parentBoid;
    //Keeps track of how many boids the user wants
    public uint targetNumBoids;
    //Keeps track of how many boids there currently are
    [HideInInspector]
    public List<GameObject> curBoids = new List<GameObject>();

    #endregion

    #region Unity Functions
    void Awake()
    {
        if (targetNumBoids > 200)
        {
            targetNumBoids = 200;
            Debug.Log("STOP, THATS TOO MANY BOIDS");
        }
        for (int i = 0; i <= targetNumBoids; i++)
        {
            //Creates a new boid
            GameObject newBoid = Instantiate(parentBoid);
            //Gives the boid a random direction
            Vector3 vector3 = new Vector3(0, 0, Random.Range(-360, 360));
            newBoid.transform.eulerAngles = vector3;
            //Adds the boid to the list of boids
            newBoid.GetComponent<boidBehaviours>().boids = curBoids;
            curBoids.Add(newBoid);
        }
    }
    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        if (targetNumBoids > 200)
        {
            targetNumBoids = 200;
            Debug.Log("Too many boids, stop.");
        }
        //Checks if more boids are needed
        if (curBoids.Count < targetNumBoids)
        {
            for (int i = curBoids.Count; i <= targetNumBoids; i++)
            {
                //Creates a new boid
                GameObject newBoid = Instantiate(parentBoid);
                //Gives the boid a random direction
                Vector3 vector3 = new Vector3(0, 0, Random.Range(-360, 360));
                newBoid.transform.eulerAngles = vector3;
                //Adds the boid to the list of boids
                newBoid.GetComponent<boidBehaviours>().boids = curBoids;
                curBoids.Add(newBoid);
            }
        }
        //Checks if boids need to be destroyed
        else if (curBoids.Count > targetNumBoids)
        {
            //Destroys the boid in unity and removes it from the list
            GameObject boid = curBoids[0];
            curBoids.RemoveAt(0);
            Destroy(boid);
        }
    }
    #endregion
}
