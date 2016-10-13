using UnityEngine;
using System.Collections;

/**
 * Created by Daniel Resio
 * must be attached to character
 **/
public sealed class Player : Entity {

    /// <summary>
    /// checks for parts. Needed from Entity class
    /// </summary>
    public override void checkForParts()
    {
        if (gameObject.name != "Player")
            Debug.LogError("Controller is not attached to the player");
        if (Player_Stats.stats.moveSpeed == 0)
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
    /// returns what the movement vector should be
    /// </summary>
    /// <returns></returns>
    public override Vector2 getMovement()
    {
        //gets axis of movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //multiplies movement by speed
        h /= Player_Stats.stats.moveSpeed;
        v /= Player_Stats.stats.moveSpeed;
        //Debug.Log("Movement is (" + h + ") horizontal and (" + v + ") vertical");
        return new Vector2(h, v);
    }

    /// <summary>
    /// Handels the melee attack
    /// </summary>
    public override void melee()
    {
        if (GameObject.Find("" + sword.name + "clone") != null)
            return;
        //finds location of mouse
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        meleeToTarget(target);
    }

}