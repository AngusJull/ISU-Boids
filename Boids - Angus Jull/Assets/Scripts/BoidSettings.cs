//Unity scriptable object by Angus Jull
//This stores the settings that each boid will use - making it easier to change the settings during runtime. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boid Settings", menuName = "Boid Settings", order = 1)]
public class BoidSettings : ScriptableObject
{
    //MoveSpeed stores how fast the boid should move forward
    [Range(0.1f, 10)]
    public float maxSpeed;
    [Range(0.1f, 10)]
    public float minSpeed;
    //Keeps track of how close other boids need to be to be detected.
    [Range(0, 2)]
    public float neighbourRadius;
    [HideInInspector]
    public float sqrNeighbourRadius;
    [Range(0, 180)]
    public int viewAngle;
    //Sets the distance other boids must be within for a boid to avoid them
    [Range(0, 1f)]
    public float avoidDistance;
    [HideInInspector]
    public float sqrAvoidDistance;
    [Range(0, 5)]
    public float avoidWallWeight;
    [Range(1, 5)]
    public float wallAvoidDistance;
    [HideInInspector]
    public float sqrWallAvoidDistance;
    //Sets how much of an affect each rule will have on the rotation of the boid
    [Range(1, 5)]
    public float alignmentWeight;
    [Range(1, 5)]
    public float approachWeight;
    [Range(1, 5)]
    public float avoidWeight;
    [Range(1, 5)]
    public float maxSteerForce;
}
