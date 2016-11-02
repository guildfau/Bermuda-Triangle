using UnityEngine;
using System.Collections;

/// <summary>
/// Created by Daniel Resio
/// Base class for all bullets, but also used for the player
/// </summary>
public class Bullet : MonoBehaviour {

    #region variables
    private float speed = 0.5f;
    private int duration;
    private Vector2 direction;
    #endregion

    //initalizes location variables
    public virtual void Start()
    {
        direction = new Vector2(0, speed);
        duration = 50;
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        //Debug.Log("bullet position = " + gameObject.transform.position);
        //moves object towards vector until duration is up
        if (!(duration < 0))
        {
            gameObject.transform.Translate(direction);
            duration -= 1;
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}