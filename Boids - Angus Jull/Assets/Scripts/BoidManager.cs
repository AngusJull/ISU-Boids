//Created by Angus Jull on November 5th 2019, creates a certain number of boids when the program starts,
//and keeps the number of boids there are and the amount the user wants the same at all times
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    #region Variables
    //Sets the amount of boids to be created on start
    [Range(0, 200)]
    public int boidsOnStart;
    private CameraBounds bounds;
    public GameObject boidPrefab;
    public BoidSettings settings;
    public BasicBehaviours behaviours;
    private List<Boid> boids = new List<Boid>();
    #endregion
    public CameraBounds getBounds() { return bounds; }
    #region Unity Functions
    void Start()
    {
        //Setting some values form the scriptable object since they don't always do so by themselves.
        settings.sqrAvoidDistance = Mathf.Pow(settings.avoidDistance, 2);
        settings.sqrNeighbourRadius = Mathf.Pow(settings.neighbourRadius, 2);
        settings.sqrWallAvoidDistance = Mathf.Pow(settings.wallAvoidDistance, 2);
        Debug.Log(settings.sqrWallAvoidDistance);
        bounds = Camera.main.GetComponent<CameraBounds>();
        createBoids(boidsOnStart);
    }
    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        foreach (Boid boid in boids)
        {
            //Finds all the boids within the boids neighbour radius
            List<Transform> context = behaviours.getNearbyBoids(boid, this);
            behaviours.basicBoidBehaviours(boid, context, this);
            //Applies the movement determined by the behaviours of the boid to the boid.
        }
    }
    #endregion
    //Creates a certain number of boids, equal to the value of the perameter numBoids
    public void createBoids(int numBoids)
    {
        for (int i = 0; i < numBoids; i++)
        {
            //Creates a new boid
            GameObject newBoid = Instantiate(
                boidPrefab,
                new Vector3(Random.Range(bounds.minimums.x, bounds.maximums.x), Random.Range(bounds.minimums.y, bounds.maximums.y)),
                Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)),
                transform);
            //Gives each boid a unique name
            newBoid.name = "Boid " + (transform.childCount);
            //Lets the boid manager keep track of which boid is which
            boids.Add(newBoid.GetComponent<Boid>());
        }
    }
}
