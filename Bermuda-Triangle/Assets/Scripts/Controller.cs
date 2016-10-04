using UnityEngine;
using System.Collections;

/**
 * Created by Daniel Resio
 * must be attached to character
 **/
public class Controller : MonoBehaviour {

    #region public variables
    public float moveSpeed;
    public GameObject bullet;
    public GameObject sword;
    #endregion 

    //checks for character parts
    void Start()
    {
        if (gameObject.name != "Character")
            Debug.LogError("Controller is not attached to the character");
        if (moveSpeed == 0)
            Debug.LogError("Speed is set to 0!");
    }

    //called every frame
	void FixedUpdate ()
    {
        //code that moves the player
        if (isMoving())
            gameObject.transform.Translate(getMovement());

        //code for melee attack
        if (Input.GetButtonDown("Fire1"))
        {
            melee();
            //Debug.Log("melee attack");
        }
        
        //code for ranged attack
        if (Input.GetButtonDown("Fire2"))
        {
            fire();
            //Debug.Log("ranged attack");
        }
	}

    /// <summary>
    /// returns true if the player should be moving
    /// </summary>
    /// <returns></returns>
    bool isMoving()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            return true;
        return false;
    }

    /// <summary>
    /// returns what the movement vector should be
    /// </summary>
    /// <returns></returns>
    Vector2 getMovement()
    {
        //gets axis of movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //multiplies movement by speed
        h *= moveSpeed;
        v *= moveSpeed;
        //Debug.Log("Movement is (" + h + ") horizontal and (" + v + ") vertical");
        return new Vector2(h, v);
    }

    /// <summary>
    /// Handels the firing of the projectile
    /// </summary>
    void fire ()
    {
        //creates bullet object
        Instantiate(bullet, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
    }

    /// <summary>
    /// Handels the melee attack
    /// </summary>
    void melee()
    {
        if (GameObject.Find("Sword(Clone)") != null)
            return;
        //finds locationi of mouse
        Vector2 lOM = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lOC = gameObject.transform.position;
        float difx = -(lOM.x - lOC.x);
        float dify = -(lOM.y - lOC.y);
        //uses math functions to find rotation angle needed
        float angle = Mathf.Atan(dify / difx);
        angle = angle* Mathf.Rad2Deg;
        //compensates for half of swing
        angle += 20;

        //complicated logic to make angle in correct quadrant
        #region quadrant solver (angle now correct)
        if (difx < 0 && dify < 0)
        {
            //Debug.Log("Q1");
            angle -= 90;
        }else if(difx < 0 && dify > 0)
        {
            //Debug.Log("Q4");
            angle -= 90;
        }else if(difx > 0 && dify < 0)
        {
            //Debug.Log("Q2");
            angle += 90;
        }
        else if (difx > 0 && dify > 0)
        {
            //Debug.Log("Q3");
            angle += 90;
        }
        #endregion

        //creates object using precalculated variables
        //rotates on z axis
        GameObject childObject = (GameObject)(Instantiate(sword, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, angle))));
        //sets object to child of character
        childObject.transform.parent = gameObject.transform;
    }

}