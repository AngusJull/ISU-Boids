//Unity script by Angus Jull on November 18th 2019
//This script applies the movements that BoidBehavours returns.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class Boid : MonoBehaviour
{
    #region Variables
    private Collider2D bCollider;
    public Collider2D boidCollider { get { return bCollider; } }

    public Vector2 curVelocity;
    #endregion 
    
    #region Unity Methods
    void Start()
    {
        curVelocity = transform.up;
        bCollider = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {

    }
    public void move(Vector2 velocity)
    {
        transform.position += (Vector3)velocity * Time.deltaTime;
        transform.up = velocity;
    }
    #endregion
}
