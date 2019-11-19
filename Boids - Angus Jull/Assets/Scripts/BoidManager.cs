//Created by Angus Jull on November 5th 2019, creates a certain number of boids when the program starts,
//and keeps the number of boids there are and the amount the user wants the same at all times
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    #region Variables
    //This is the boid prefab that is used to create all other boids
    public GameObject parentBoid;
    //Keeps track of how many boids the user wants
    [Range(0, 200)]
    public int targetNumBoids;
    //Keeps track of how many boids there currently are
    [HideInInspector]
    public List<GameObject> curBoids = new List<GameObject>();

    #endregion
    public CameraBounds bounds = Camera.main.GetComponent<CameraBounds>();
    public GameObject boidPrefab;
    public BoidSettings boidSettings;
    public List<Boid> boids = new List<Boid>();
    [Range(0, 200)]
    public int boidsOnStart;

    #region Unity Functions
    void Awake()
    {
        createBoids(boidsOnStart);
    }
    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        
    }
    #endregion
    //Creates a certain number of boids, equal to the value of the perameter numBoids
    public void createBoids(int numBoids)
    {
        for (int i = 0; i < numBoids; i++)
        {
            //Creates a new boid
            GameObject newBoid = Instantiate(
                parentBoid,
                Random.insideUnitCircle * bounds.minimums.y, 
                Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)),
                transform);
            //Since lists are reference variables, they will always be up to date
            newBoid.GetComponent<Boid>().settings = boidSettings;
            newBoid.GetComponent<Boid>().boids = boids;
            newBoid.name = "Boid " + (transform.childCount + 1);
            boids.Add(newBoid.GetComponent<Boid>());
        }
    }
}
