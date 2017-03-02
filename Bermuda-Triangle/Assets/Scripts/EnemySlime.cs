using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : Entity {

    /// <summary>
    /// these will be set up as finalized private variables
    /// </summary>
    #region some basic variables for easy tweaking while editing
    public float sightRange = 5;
    public float speed = 0.035f;
    #endregion

    /// <summary>
    /// gives the slime its animation component
    /// </summary>
    public override void atStart()
    {
        setAnimator(GetComponent<Animator>());
    }

    /// <summary>
    /// Nothing important to go here right now
    /// </summary>
    public override void checkForParts()
    {
        //nothng here yet
    }

    /// <summary>
    /// major AI code here
    /// </summary>
    /// <returns></returns>
    public override Vector2 getMovement()
    {
        Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 slimePosition = gameObject.transform.position;
        float distance = Vector2.Distance(slimePosition, playerPosition);
        //checks sight range
        if (distance <= sightRange)
        {
            //moves to player
            if(Mathf.Abs(slimePosition.x - playerPosition.x) > Mathf.Abs(slimePosition.y - playerPosition.y))
            {
                if (slimePosition.x - playerPosition.x > 0)
                {
                    return new Vector2(-speed, 0);
                }
                else
                {
                    return new Vector2(speed, 0);
                }
            }
            else
            {
                if (slimePosition.y - playerPosition.y > 0)
                {
                    return new Vector2(0, -speed);
                }
                else
                {
                    return new Vector2(0, speed);
                }
            }
        }
        else
        {
            return new Vector2(0, 0);
        }
    }

    /// <summary>
    /// uses the collisions object to handle information about collisions
    /// </summary>
    public override void handleCollisions()
    {

    }

    /// <summary>
    /// this will be its "jump attack"
    /// </summary>
    public override void melee()
    {
        //nothing here yet
    }

}