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
    public uint numBoids;
    #endregion

    #region Unity Functions
    void Start()
    {
        for (numBoids = 0; numBoids < targetNumBoids; ++numBoids)
        {
            //Creates a new boid
            GameObject newBoid = Instantiate(parentBoid);
            //This vector is offset so that the boids won't stack a ton
            Vector3 newPos = new Vector3(5f / (float)(numBoids + 1), 0);
            newBoid.GetComponent<Transform>().position = newPos;
            Debug.Log(numBoids);
        }
    }
    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        //Checks if more boids are needed
        if (numBoids < targetNumBoids)
        {
                Instantiate(parentBoid);
                numBoids++;
                Debug.Log("Created a Boid");
        }
        //Checks if boids need to be destroyed
        else if (numBoids > targetNumBoids)
        {
            //Creates an array of boids, all of which
            GameObject boid = GameObject.FindGameObjectWithTag("Boid");
            Destroy(boid);
            numBoids--;
            Debug.Log("Destroyed a boid");
        }
    }
    #endregion
}
