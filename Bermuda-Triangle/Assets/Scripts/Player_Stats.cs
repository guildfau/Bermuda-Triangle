using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.UI;

/// <summary>
/// This class is to hold stats and info of player and have the ability to be saved
/// </summary>
public class Player_Stats : MonoBehaviour {

    //variable to be accessed by all other classes
    public static Player_Stats stats;

    private NameAndScore[] scores;

    public float time = 0;

    #region stats
    public int maxHealth = 10;
    public int health = 10;
    public float verticalMoveSpeed = 10;
    public float horizontalMoveSpeed = 10;
    public int maxAmmo = 10;
    public int ammo =10;
    public int attackCooldown = 30;
    public int cooldown = 0;
    public int defence = 10;
    public int damage = 10;
    public int manna = 10;
    #endregion

    #region progress info
    public bool gameNotOver = true;
    #endregion

    void Awake()
    {
        if(stats == null)
        {
            //DontDestroyOnLoad(gameObject);
            stats = this;
        }
        else if(stats != this)
            Destroy(gameObject); 
    }

    void Update()
    {
        time += Time.deltaTime;
        // if notgamenotover lel
        if (!gameNotOver)
            gameOver();
    }

    /// <summary>
    /// code here for what happens at gameover
    /// </summary>
    void gameOver()
    {
        //this went somewhere else for now
    }

    public float getTime()
    {
        return time;
    }

    public bool getGameOver()
    {
        return !gameNotOver;
    }

    public void addHighScore(string name, float time)
    {
        int tempIndex;
        if(scores == null)
        {
            tempIndex = 0; 
        }
        else
        {
            tempIndex = scores.Length + 1;
        }
        NameAndScore[] temp = new NameAndScore[tempIndex];
        if(temp.Length == 1)
        {
            temp[0] = new NameAndScore(name, time);
        }
        else
        {
            bool shift = false;
            for(int i = 0; i < temp.Length; i++)
            {
                if (!shift) {
                    if (scores[i].time > time)
                    {
                        temp[i] = new NameAndScore(name, time);
                        shift = true;
                    }
                    else
                    {
                        temp[i] = scores[i];
                    }
                }
                else
                {
                    temp[i] = scores[i - 1];
                }
            }
        }

    }

    private string highscoreToText(NameAndScore[] toConvert)
    {
        string temp = "Highscore:\n";
        if (toConvert == null)
        {
            toConvert = new NameAndScore[0];
            toConvert[0].playerName = "Placeholder";
            toConvert[0].time = 0.0f;
        }
        for (int i = 0; i < toConvert.Length; i++)
        {
            temp += toConvert[i].playerName + "\t" + toConvert[i].time.ToString() + "\n";
        }
        return temp;

    }

    private NameAndScore[] textToHighscore(string toConvert)
    {
        string[] fLines = Regex.Split(toConvert, "\n");
        NameAndScore[] temp = new NameAndScore[fLines.Length];
        //starts at 1 to ignore the Highscore line
        for(int i = 1; i < fLines.Length; i++)
        {
            string[] line = Regex.Split(fLines[i], "\t");
            temp[i].playerName = line[0];
            temp[i].time = float.Parse(line[1]);
        }
        return temp;
    }

    public void displayHighScore(GameObject toDisplayOn)
    {
        toDisplayOn.GetComponent<Text>().text = highscoreToText(scores);
    }

}

class NameAndScore
{
    public NameAndScore(string name, float newTime)
    {
        playerName = name;
        time = newTime;
    }

    public string playerName;
    public float time;
}
