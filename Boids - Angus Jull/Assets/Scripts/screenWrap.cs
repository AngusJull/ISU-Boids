//Created by Angus Jull on November 5th 2019, makes the boid wrap around the screen whenever it's transform 
//Is no longer visible to the camera
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenWrap : MonoBehaviour
{
    #region Variables
    private CameraBounds bounds;
    #endregion
    void Start()
    {
        bounds = Camera.main.GetComponent<CameraBounds>();
    }
    #region Unity Functions
    // Update is called once per frame
    void FixedUpdate()
    {
        //If the player is no longer visible and they have moved over the screen boundaries, their x or y will be set to the opposite values
        if (!GetComponent<SpriteRenderer>().isVisible)
        {
            if (transform.position.x < bounds.minimums.x)
            {
                transform.position = new Vector3(bounds.maximums.x, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > bounds.maximums.x)
            {
                transform.position = new Vector3(bounds.minimums.x, transform.position.y, transform.position.z);
            }
            if (transform.position.y < bounds.minimums.y)
            {
                transform.position = new Vector3(transform.position.x, bounds.maximums.y, transform.position.z);
            }
            else if (transform.position.y > bounds.maximums.y)
            {
                transform.position = new Vector3(transform.position.x, bounds.minimums.y, transform.position.z);
            }
        }
        #endregion
    }
}
     