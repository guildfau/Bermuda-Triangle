using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/**
 * Created by Daniel Resio
 * must be attached to character with sprites
 **/
public sealed class Player : Entity {

    /// <summary>
    /// adds animation item and things
    /// </summary>
    public override void atStart()
    {
        setAnimator(GetComponent<Animator>());
    }

    /// <summary>
    /// checks for parts. Needed from Entity class
    /// </summary>
    public override void checkForParts()
    {
        Player_Stats.stats.maxHealth = 5;
        if (gameObject.name != "Player")
            Debug.LogError("Controller is not attached to the player");
        if (Player_Stats.stats.moveSpeed == 0)
            Debug.LogError("Speed is set to 0!");
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

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
        //setting movement to left right up and down
        if (Mathf.Abs(h) > Mathf.Abs(v))
        {
            v = 0;
        }
        else
        {
            h = 0;
        }
        //multiplies movement by speed
        h /= Player_Stats.stats.moveSpeed;
        v /= Player_Stats.stats.moveSpeed;
        //Debug.Log("Movement is (" + h + ") horizontal and (" + v + ") vertical");
        return new Vector2(h, v);
    }
    
    /// <summary>
    /// colision handler
    /// </summary>
    public override void handleCollisions()
    {
        if (isColliding("Slime") != null)
        {
            Player_Stats.stats.gameNotOver = false;
        }
    }

    /// <summary>
    /// Handels the melee attack
    /// </summary>
    public override void melee()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        meleeToTarget(target);
    }

}