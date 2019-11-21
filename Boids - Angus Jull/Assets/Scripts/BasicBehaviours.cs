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
            if (c != boid.boidCollider && Vector2.Angle(boid.Velocity, (Vector2)c.transform.position) <= manager.settings.viewAngle)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
    public Vector2 basicBoidBehaviours(Boid boid, List<Transform> context, BoidManager manager)
    {
        Vector2 movement = Vector2.zero;
        movement += steerTowards(boid, align(boid, context, manager), manager) * manager.settings.alignmentWeight;
        movement += steerTowards(boid, approach(boid, context, manager), manager) * manager.settings.approachWeight;
        movement += steerTowards(boid, avoid(boid, context, manager), manager) * manager.settings.avoidWeight;

        boid.Velocity += movement * Time.deltaTime;
        float speed = boid.Velocity.magnitude;
        boid.Velocity.Normalize();
        speed = Mathf.Clamp(speed, manager.settings.minSpeed, manager.settings.maxSpeed);
        boid.Velocity *= speed;

        return boid.Velocity;
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
            return boid.Velocity.normalized;
        }
        Vector2 approachVector = Vector2.zero;
        foreach (Transform _boid in context)
        {
            approachVector += (Vector2)_boid.position;
        }
        //Find the average position of the boids in context and then find the vector between the transform and it
        return approachVector / context.Count - (Vector2)boid.transform.position;
    }
    private Vector2 avoid(Boid boid, List<Transform> context, BoidManager manager)
    {
        if(context.Count == 0)
        {
            return boid.Velocity.normalized;
        }
        //Stores the number of boids that are being avoided
        int nBoids = 0;
        Vector2 avoidForce = Vector2.zero;
        foreach(Transform _boid in context)
        {
            float sqrDst = Vector2.SqrMagnitude(_boid.position - boid.transform.position);
            //Compares the square magnitude 
            if (sqrDst < Mathf.Pow(manager.settings.avoidDistance, 2))
            {
                //If the boids are directly on top of eachother
                if (sqrDst == 0)
                {
                    //Add a random force with the maximum value to try and seperate the boids
                    avoidForce += new Vector2(Random.Range(0.1f, 1), Random.Range(0.1f, 1)) * manager.settings.maxSpeed;
                    ++nBoids;
                }
                //Add the vector from the other boid to this boid (in that direction)
                avoidForce += (Vector2)(boid.transform.position - _boid.position) / sqrDst;
                ++nBoids;
            }
        }
        //Ensures no div by 0 errors
        if (nBoids > 0)
        {
            avoidForce /= nBoids;
        }
        return avoidForce;
    }
    //Takes in a vector and returns a vector that is between the current velocity and that vector
    //The returned vector has a magnitude dependant on that of the original, multiplied by the max speed and clamped to the maximum turning force
    Vector2 steerTowards(Boid boid, Vector2 targetVector, BoidManager manager)
    {
        Vector2 v = targetVector.normalized * manager.settings.maxSpeed - boid.Velocity;
        return Vector2.ClampMagnitude(v, manager.settings.maxSteerForce);
    }
}
