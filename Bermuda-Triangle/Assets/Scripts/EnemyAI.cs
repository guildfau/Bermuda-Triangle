using UnityEngine;
using System.Collections;

/**
 * Created by Daniel Resio
 * must be attached to mob
 **/
public class EnemyAI : MonoBehaviour
{

    #region public variables
    public float sightRange;
    public float attackRange;
    public float moveSpeed;
    public GameObject bullet;
    public GameObject sword;
    public bool rangedAttack;
    public bool meleeAttack;
    public int setAttackCooldown;
    #endregion

    #region private variables
    private bool canAttack = false;
    private float rangedRange = 15;
    private float meleeRange = 2;
    private int attackCooldown;
    #endregion

    //checks for character parts
    void Start()
    {
        if (gameObject.name != "Mob")
            Debug.LogError("Controller is not attached to the mob");
        if (moveSpeed == 0)
            Debug.LogError("Speed is set to 0!");
        if(sightRange == 0)
            Debug.LogError("Sight range is set to 0!");
    }

    void Update()
    {
        if (attackCooldown != 0)
            attackCooldown -= 1;
        if (canAttack && attackCooldown == 0)
            attack();
    }

    //called before physics processing
    void FixedUpdate()
    {
        //code that moves the mob
        if (isMoving())
            gameObject.transform.Translate(getMovement());
    }

    /// <summary>
    /// returns true if the player should be moving
    /// </summary>
    /// <returns></returns>
    bool isMoving()
    {
        if (getMovement().x !=0 || getMovement().y != 0)
            return true;
        return false;
    }

    /// <summary>
    /// returns what the movement vector should be
    /// </summary>
    /// <returns></returns>
    Vector2 getMovement()
    {
        Vector2 playerLocation = GameObject.FindWithTag("Player").transform.position;
        float distance = Vector2.Distance(transform.position, playerLocation);
        Debug.Log(distance);
        if (distance > sightRange)
        {
            return new Vector2(0, 0);
        }
        else if (distance <= attackRange)
        {
            canAttack = true;
            return new Vector2(0, 0);
        }
        else
        {
            Vector2 pos = gameObject.transform.position;
            Vector2 dif = (playerLocation - pos);
            dif = dif.normalized / moveSpeed;
            return dif;
        }
    }

    /// <summary>
    /// Handels all of the attacking with ranges and types
    /// </summary>
    void attack()
    {
        attackCooldown = setAttackCooldown;
        Vector2 playerLocation = GameObject.FindWithTag("Player").transform.position;
        float distance = Vector2.Distance(transform.position, playerLocation);
        if(distance < meleeRange && meleeAttack)
        {
            melee();
            return;
        }else if(distance < rangedRange && rangedAttack)
        {
            fire();
            return;
        }
    }

    /// <summary>
    /// Handels the firing of the projectile
    /// </summary>
    void fire()
    {
        //creates bullet object
        Instantiate(bullet, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
    }

    /// <summary>
    /// Handels the melee attack
    /// </summary>
    void melee()
    {
        if (GameObject.Find("EnemySword(Clone)") != null)
            return;
        //finds locationi of mouse
        Vector2 lOP = GameObject.FindWithTag("Player").transform.position;
        Vector2 lOC = gameObject.transform.position;
        float difx = -(lOP.x - lOC.x);
        float dify = -(lOP.y - lOC.y);
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
        GameObject childObject = (GameObject)(Instantiate(sword, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, angle))));
        //sets object to child of character
        childObject.transform.parent = gameObject.transform;
    }

}