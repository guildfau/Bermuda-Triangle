using UnityEngine;

/// <summary>
/// Created by Daniel Resio.
/// Class that is the base of all entities in the game.  
/// </summary>
public abstract class Entity : MonoBehaviour {

    #region public variables
    public GameObject bullet;
    public GameObject sword;
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
    /// checks for parts to avoid errors. Other things can go here too if need be
    /// </summary>
    public void Start()
    {
        checkForParts();
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
        //compensates for half of swing
        angle += 20;


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

        //creates object using precalculated variables
        //rotates on z axis
        Debug.Log("creating sword");
        GameObject childObject = (GameObject)(Instantiate(sword, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, angle))));
        //sets object to child of character 
        childObject.transform.parent = gameObject.transform;
    }

    /// <summary>
    /// Handels the firing of the projectile
    /// </summary>
    public void fire()
    {
        //creates bullet object
        Instantiate(bullet, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
    }

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
    /// will handle all of the collisions
    /// </summary>
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "enemyBullet")
        {
            Bullet objBullet = (Bullet)(col.gameObject.GetComponent("Script"));
            Player_Stats.stats.health -= objBullet.getDamage();
        }

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
}