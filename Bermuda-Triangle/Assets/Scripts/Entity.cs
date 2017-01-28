using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Daniel Resio.
/// Class that is the base of all entities in the game. 
/// Some animations already implemented in code
/// TODO: collider info
/// </summary>
public abstract class Entity : MonoBehaviour {

    private List<Collision> collisions = new List<Collision>();
    private Animator anim;

    #region public variables
    public GameObject bullet;
    #endregion

    /// <summary>
    /// returns what the movement vector should be
    /// </summary>
    /// <returns></returns>
    public abstract Vector2 getMovement();

    /// <summary>
    /// Handles all of the melee information
    /// </summary>
    public abstract void melee();

    /// <summary>
    /// method that checks for parts needed and if absent, will return errors
    /// </summary>
    public abstract void checkForParts();

    /// <summary>
    /// method that allows for managing of animation and other items at start
    /// </summary>
    public abstract void atStart();

    /// <summary>
    /// checks for parts to avoid errors. Other things can go here too if need be
    /// </summary>
    public void Start()
    {
        checkForParts();
        atStart();
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void FixedUpdate()
    {
        handleCollisions();
        handleAnimation();
    }

    /// <summary>
    /// swings sword at target
    /// </summary>
    /// <param name="target"></param>
    public void meleeToTarget(Vector2 target)
    {
        Vector2 lOC = gameObject.transform.position;
        float difx = -(target.x - lOC.x);
        float dify = -(target.y - lOC.y);
        //uses math functions to find rotation angle needed
        float angle = Mathf.Atan(dify / difx);
        angle = angle * Mathf.Rad2Deg;

        //complicated logic to make angle in correct quadrant
        #region quadrant solver (angle now correct)
        if (difx < 0 && dify < 0)
        {
            //Debug.Log("Q1");
            angle -= 90;
        }
        else if (difx < 0 && dify > 0)
        {
            //Debug.Log("Q4");
            angle -= 90;
        }
        else if (difx > 0 && dify < 0)
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

        #region Handles attack directions
        if (angle < -45 && angle > -135)
        {
            swordAttackRight();
        }
        else if (angle > -45 && angle < 45)
        {
            swordAttackUp();
        }
        else if(angle > 45 && angle < 135)
        {
            swordAttackLeft();
        }
        else
        {
            swordAttackDown();
        }
        #endregion
    }

    /// <summary>
    /// Handels the firing of the projectile
    /// </summary>
    public void fire()
    {
        Quaternion angle;

        //finds mouse position
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 charPos = gameObject.transform.position;
        Vector2 direction = -(charPos - pos);

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x < 0)
            {
                angle = Quaternion.Euler(0,0,90);
            }
            else
            {
                angle = Quaternion.Euler(0, 0, -90);
            }
        }
        else
        {
            if (direction.y < 0)
            {
                angle = Quaternion.Euler(0, 0, 180);
            }
            else
            {
                angle = Quaternion.Euler(0, 0, 0);
            }
        }

        Instantiate(bullet, gameObject.transform.position, angle);
    }

    #region boolean questions
    /// <summary>
    /// returns true if the player should be moving
    /// </summary>
    /// <returns></returns>
    public bool isMoving()
    {
        if (getMovement().x != 0 || getMovement().y != 0 )
            return true;
        return false;
    }

    /// <summary>
    /// returns if entity can attack
    /// </summary>
    /// <returns></returns>
    public bool canAttack()
    {
        if (Player_Stats.stats.cooldown <= 0)
            return true;
        return false;
    }
    #endregion

    #region adds and removes collisions from list
    /// <summary>
    /// adds collisions to the list
    /// </summary>
    /// <param name="c"></param>
    void OnCollisionEnter(Collision c)
    {
        collisions.Add(c);
    }

    /// <summary>
    /// removes collisions from the list
    /// </summary>
    /// <param name="c"></param>
    void OnCollisionExit(Collision c)
    {
        string tag = c.gameObject.tag;
        for (int i = 0; i < collisions.Count; i++)
            if (collisions[i].gameObject.tag == tag)
                collisions.RemoveAt(i);
    }
    #endregion

    #region sword attack 
    private void swordAttackRight()
    {
        Debug.Log("Sword Attack Right");
    }
    
    private void swordAttackUp()
    {
        Debug.Log("Sword Attack Up");
    }

    private void swordAttackLeft()
    {
        Debug.Log("Sword Attack Left");
    }

    private void swordAttackDown()
    {
        Debug.Log("Sword Attack Down");
    }
    #endregion

    /// <summary>
    /// Checks if object is colliding with object with tag
    /// </summary>
    /// <param name="tag name"></param>
    /// <returns></returns>
    private Collision isColliding(string c)
    {
        for (int i = 0; i < collisions.Count; i++)
        {
            if (collisions[i].gameObject.tag == c)
                return collisions[i];
        }
        return null;
    }

    /// <summary>
    /// uses the collisions object to handle information about collisions
    /// </summary>
    private void handleCollisions()
    {
        Collision col;
        if ((col = isColliding("EnemyBullet")) != null)
        {
            Player_Stats.stats.health -= 10;
            Destroy(col.gameObject);
        }
    }

    private void handleAnimation()
    {
        if (isMoving())
        {
            Vector2 temp = getMovement();
            getAnimator().SetBool("Movement", true);
            #region sets variables for directions

            if (Mathf.Abs(temp.x) > Mathf.Abs(temp.y))
            {
                if (temp.x > 0 && getAnimator().GetInteger("Direction") != 3)
                {
                    //3 is right
                    getAnimator().SetInteger("Direction", 3);
                }
                else if (temp.x < 0 && getAnimator().GetInteger("Direction") != 2)
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
    }

    #region getter and setter for animator
    public void setAnimator(Animator newAnimator)
    {
        anim = newAnimator;
    }

    public Animator getAnimator()
    {
        return anim;
    }
    #endregion

}