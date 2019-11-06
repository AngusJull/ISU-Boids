//Unity script by Angus Jull, Created on November 5th 2019
//Sets the screen boundaries that the boids will stay within in two Vector2 objects. 

using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    #region Variables
    //These variables are meant to be used with boids.
    public Vector2 minimums = new Vector2();
    public Vector2 maximums = new Vector2();
    #endregion
    
    #region Unity Methods
    //Sets the two Vector2s
    void Start()
    {
        float vertExtent = GetComponent<Camera>().orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;
        minimums.Set(-1 * horzExtent, -1 * vertExtent);
        maximums.Set(horzExtent, vertExtent);
    }
    #endregion
}
