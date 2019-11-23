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

    public Vector2 Velocity;
    #endregion 
    
    #region Unity Methods
    void Start()
    {
        Velocity = transform.up;
        bCollider = GetComponent<Collider2D>();
        GetComponentInChildren<SpriteRenderer>().color = Random.ColorHSV(0, 1, 0.5f, 0.6f, 1, 1);
    }
    public void FixedUpdate()
    {
        move(Velocity);
    }
    public void move(Vector2 velocity)
    {
        transform.position += (Vector3)velocity * Time.deltaTime;
        transform.up = velocity.normalized;
    }
    #endregion
}
