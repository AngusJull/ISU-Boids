//Unity scriptable object by Angus Jull
//This stores the settings that each boid will use - making it easier to change the settings during runtime. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boid Settings", menuName = "Boid Settings", order = 1)]
public class BoidSettings : ScriptableObject
{
    //MoveSpeed stores how fast the boid should move forward
    [Range(0.1f, 5f)]
    public float maxSpeed;
    //Keeps track of how close boids need to be to be detected
    [Range(1, 3)]
    public float viewRange;
    //This controls the field of view of the boid
    [Range(1, 180)]
    public float viewAngle;
    //Sets the distance other boids must be within for a boid to avoid them
    [Range(0, 1)]
    public float avoidDistance;
    //Sets how much of an affect each rule will have on the rotation of the boid
    [Range(0, 1)]
    public float alignmentWeight;
    [Range(0, 1)]
    public float approachWeight;
    [Range(0, 1)]
    public float avoidWeight;
}
