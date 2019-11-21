//Unity script by Angus Jull on November 19th 2019
//Provides a base set of functions for boids - calucates where they need to move and also allows boids to find other boids around them.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Basic Behaviour", menuName = "Behaviours/Basic")]
public class BasicBehaviours : ScriptableObject
{
    //Gets the list of transforms who's colliders are currently touching this boid.
    public List<Transform> getNearbyBoids(Boid boid, BoidManager manager)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(boid.transform.position, manager.settings.neighbourRadius);
        foreach (Collider2D c in colliders)
        {
            if (c != boid.boidCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
    public Vector2 basicBoidBehaviours(Boid boid, List<Transform> context, BoidManager manager)
    {
        Vector2 movement = Vector2.zero;
        movement += Vector2.ClampMagnitude(align(boid, context, manager) * manager.settings.alignmentWeight, manager.settings.alignmentWeight);
        movement += Vector2.ClampMagnitude(approach(boid, context, manager) * manager.settings.approachWeight, manager.settings.approachWeight);
        movement += Vector2.ClampMagnitude(avoid(boid, context, manager) * manager.settings.avoidWeight, manager.settings.avoidWeight);
        return movement;
    }
    private Vector2 align(Boid boid, List<Transform> context, BoidManager manager)
    {
        //If no boids around this boid, return no adjustment.
        if (context.Count == 0)
        {
            return boid.transform.up;
        }
        Vector2 averageHeading = boid.transform.up;
        foreach (Transform _boid in context)
        {
            averageHeading += (Vector2)_boid.up;
        }
        //Find the average position of the boids in context and then find the vector between the transform and it
        return averageHeading / context.Count;
    }
    private Vector2 approach(Boid boid, List<Transform> context, BoidManager manager)
    {
        //If no boids around this boid, return no adjustment.
        if (context.Count == 0)
        {
            return Vector2.zero;
        }
        Vector2 approachVector = Vector2.zero;
        foreach (Transform _boid in context)
        {
            approachVector += (Vector2)_boid.position;
        }
        //Find the average position of the boids in context and then find the vector between the transform and it
        approachVector /= context.Count;
        approachVector -= (Vector2)boid.transform.position;

        approachVector = Vector2.SmoothDamp(boid.transform.up, approachVector.normalized, ref boid.curVelocity, manager.settings.approachSmoothingFactor);
        return approachVector; 
    }
    private Vector2 avoid(Boid boid, List<Transform> context, BoidManager manager)
    {
        if(context.Count == 0)
        {
            return Vector2.zero;
        }
        //Stores the number of boids that are being avoided
        int nBoids = 0;
        Vector2 avoidForce = Vector2.zero;
        foreach(Transform _boid in context)
        {
            //Compares the square magnitude 
            if (Vector2.SqrMagnitude(_boid.position - boid.transform.position) < Mathf.Pow(manager.settings.avoidDistance, 2))
            {
                avoidForce += (Vector2)(boid.transform.position - _boid.position);
                ++nBoids;
            }
        }
        if (nBoids > 0)
        {
            avoidForce /= nBoids;
        }
        return avoidForce;
    }
}
