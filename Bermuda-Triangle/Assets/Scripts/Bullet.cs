using UnityEngine;
using System.Collections;

/**
 * Created by Daniel Resio
 * Handles bullet movement and duration
 **/
public class Bullet : MonoBehaviour {

    #region public variables
    private Vector3 direction;
    private int duration;
    private int speed = 7;
    #endregion

    //initalizes location variables
    void Start()
    {
        //finds mouse position
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 charPos = GameObject.FindWithTag("Player").transform.position;
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
    void Update()
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
