using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// This class is to hold stats and info of player and have the ability to be saved
/// </summary>
[System.Serializable]
public class Player_Stats : MonoBehaviour {

    //variable to be accessed by all other classes
    public static Player_Stats stats;

    #region stats
    public int maxHealth = 10;
    public int health = 10;
    public float moveSpeed = 10;
    public int maxAmmo = 10;
    public int ammo =10;
    public int attackCooldown = 30;
    public int cooldown = 0;
    public int defence = 10;
    public int damage = 10;
    public int manna = 10;
    #endregion

    #region progress info

    #endregion

    void Awake()
    {
        if(stats == null)
        {
            DontDestroyOnLoad(gameObject);
            stats = this;
        }
        else if(stats != this)
            Destroy(gameObject);
    }

}
