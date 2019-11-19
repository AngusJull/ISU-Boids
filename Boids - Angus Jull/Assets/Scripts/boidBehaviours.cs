//Created by Angus Jull on November 7th 2019
//This scriptable object holds functions that calculate where the boid should head
//This object is just a way of making the code simpler.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Basic Behaviours", menuName = "Behaviours/Basic Behaviours")]
public class BoidBehaviours : ScriptableObject
{
    public List<Transform> getNearbyBoids(Boid _boid, BoidManager manager)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_boid.transform.position, manager.settings.neighbourRadius);
        foreach (Collider2D c in colliders)
        {
            if (c != _boid.boidCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
    public Vector2 basicBoidBehaviours(Boid _boid, List<Transform> context, BoidManager manager)
    {
        return Vector2.zero;
    }
}
