//Created by Angus Jull on November 5th 2019, creates a certain number of boids when the program starts,
//and keeps the number of boids there are and the amount the user wants the same at all times
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createBoids : MonoBehaviour
{
    #region Variables
    public GameObject parentBoid;
    public int targetNumBoids;
    public int numBoids;
    #endregion

    #region Unity Functions
    void Start()
    {
        for (numBoids = 0; numBoids < targetNumBoids; ++numBoids)
        {
            Instantiate(parentBoid).GetComponent<Transform>().position.Set(numBoids, numBoids, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (numBoids < targetNumBoids)
        {
            Instantiate(parentBoid);
            numBoids++;
            Debug.Log("Created a Boid");
        }
        else if (numBoids > targetNumBoids)
        {
            GameObject[] Boids = GameObject.FindGameObjectsWithTag("Boid");
            Destroy(Boids[0], 0.1f);
            numBoids--;
            Debug.Log("Destroyed a boid");
        }
    }
    #endregion
}
