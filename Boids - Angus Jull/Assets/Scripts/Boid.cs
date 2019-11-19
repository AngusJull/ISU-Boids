//Unity script by Angus Jull on November 18th 2019
//This script applies the movements that BoidBehavours returns.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class Boids : MonoBehaviour
{
    #region Variables
    [HideInInspector]
    public BoidSettings settings;

    [HideInInspector]
    public List<Boid> boids;

    private Collider2D boidCollider;
    public Collider2D bCollider { get { return boidCollider; } }
    #endregion 
    
    #region Unity Methods
    void Start()
    {
        boidCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        
    }
    #endregion
}
