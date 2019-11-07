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
            GameObject newBoid = Instantiate(parentBoid);
            Vector3 newPos = new Vector3(5f / (float)(numBoids + 1), 0);
            newBoid.GetComponent<Transform>().position = newPos;
            Debug.Log(numBoids);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
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
