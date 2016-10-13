using UnityEngine;
using System.Collections;

/// <summary>
/// Created by Daniel Resio
/// Base class for all bullets, but also used for the player
/// </summary>
public class Bullet : MonoBehaviour {

    #region variables
    private Vector3 direction;
    private int duration;
    //default variable settings
    private int speed = 7;
    private int damage = 10;
    #endregion

    //initalizes location variables
    public virtual void Start()
    {
        //finds mouse position
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(pos);
        Vector2 charPos = GameObject.FindWithTag("Player").transform.position;
        Debug.Log(charPos);
        Vector2 dif = -(charPos - pos);
        //normalizes the vector
        direction = dif.normalized;
        //decreases speed
        direction.x /= speed;
        direction.y /= speed;
        //sets length that the bullet will fly
        duration = 50;
    }

    // Update is called once per frame
    public virtual void Update()
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

    #region getters

    /// <summary>
    /// returns direction of bullet heading
    /// </summary>
    /// <returns></returns>
    public Vector3 getDirection()
    {
        return direction;
    }

    /// <summary>
    /// returns duration of bullet
    /// </summary>
    /// <returns></returns>
    public int getDuration()
    {
        return duration;
    }

    /// <summary>
    /// returns speed of bullet
    /// </summary>
    /// <returns></returns>
    public int getSpeed()
    {
        return speed;
    }

    /// <summary>
    /// returns damage given by bullet
    /// </summary>
    /// <returns></returns>
    public int getDamage()
    {
        return damage;
    }
    #endregion
}