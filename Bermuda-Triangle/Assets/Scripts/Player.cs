using UnityEngine;
using System.Collections;
using System;

/**
 * Created by Daniel Resio
 * must be attached to character
 **/
 //TODO: ANIMATION STUFF
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
        //code that moves the player
        if (isMoving())
        {
            Vector2 temp = getMovement();
            gameObject.transform.Translate(temp);
            //sets variable for movement in animator
            getAnimator().SetBool("Movement", true);
            #region sets variables for directions

            if(Mathf.Abs(temp.x) > Mathf.Abs(temp.y))
            {
                if(temp.x > 0 && getAnimator().GetInteger("Direction") !=3)
                {
                    //3 is right
                    getAnimator().SetInteger("Direction", 3);
                }
                else if(temp.x < 0 && getAnimator().GetInteger("Direction") != 2)
                {
                    //2 is left
                    getAnimator().SetInteger("Direction", 2);
                }
            }
            else
            {
                if (temp.y > 0 && getAnimator().GetInteger("Direction") != 1)
                {
                    //1 is up
                    getAnimator().SetInteger("Direction", 1);
                }
                else if (temp.y < 0 && getAnimator().GetInteger("Direction") != 0)
                {
                    //0 is down
                    getAnimator().SetInteger("Direction", 0);
                }
            }
            #endregion
        }
        else
        {
            getAnimator().SetBool("Movement", false);
        }
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
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        meleeToTarget(target);
    }

}