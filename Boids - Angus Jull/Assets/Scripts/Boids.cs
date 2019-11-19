//Unity script by Angus Jull on November 18th 2019
//This script applies the movements that BoidBehavours returns.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Boid : MonoBehaviour
{
    Collider2D boidCollider;
    public Collider2D _collider { get { return boidCollider; } }
    #region Variables
    [HideInInspector]
    public BoidSettings settings;

    [HideInInspector]
    public List<Transform> context;
    #endregion 
    
    

    #region Unity Methods
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    #endregion
}
