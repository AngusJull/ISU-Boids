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
    public BoidBehaviours behaviours;
    private List<Boid> boids = new List<Boid>();
    #endregion


    #region Unity Functions
    void Start()
    {
        bounds = Camera.main.GetComponent<CameraBounds>();
        createBoids(boidsOnStart);
    }
    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        foreach (Boid _boid in boids)
        {
            List<Transform> context = behaviours.getNearbyBoids(_boid, this);
            /////Just to see.
            //_boid.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

            //Vector2 movement = behaviours.basicBoidBehaviours(_boid, context, this);
            //movement *= settings.driveFactor;
            //Vector2.ClampMagnitude(movement, settings.maxSpeed);
            //_boid.move(movement);
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
                Random.insideUnitCircle * bounds.minimums.y,
                Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)),
                transform);
            //Gives each boid a unique name
            newBoid.name = "Boid " + (transform.childCount + 1);
            //Lets the boid manager keep track of which boid is which
            boids.Add(newBoid.GetComponent<Boid>());
        }
    }
}
