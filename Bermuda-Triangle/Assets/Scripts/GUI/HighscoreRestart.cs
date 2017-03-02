using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreRestart : MonoBehaviour {

    GameObject settings1;
    GameObject submitButton;
    GameObject highScore;
    string inputField;
    bool hasloded = false;

	// Use this for initialization
	void Start () {
        settings1 = GameObject.Find("GetName");
        submitButton = GameObject.Find("Submit");
        highScore = GameObject.FindGameObjectWithTag("HighscorePanel");
        settings1.SetActive(false);
        highScore.SetActive(false);
    }
	
    public void exit()
    {
        Application.Quit();
    }

    public void restart()
    {
        SceneManager.LoadScene("Test");
        Time.timeScale = 1;
        Player_Stats.stats.gameNotOver = true;
    }

    public void display()
    {
        if (!hasloded)
        {
            highScore.SetActive(true);
        }
    }

    /// <summary>
    /// call for the function
    /// </summary>
    public void submit()
    {
        Player_Stats.stats.addHighScore(inputField , Player_Stats.stats.getTime());
        highScore.SetActive(true);
        settings1.SetActive(false);
        Player_Stats.stats.displayHighScore(highScore);
    }

    public void updateString()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Input");
        string name = temp.GetComponent<Text>().text;
        if (name.Length > 0)
        {
            inputField = name;
        }
    }

	// Update is called once per frame
	void Update () {
        if (Player_Stats.stats.getGameOver())
        {
            if(Time.timeScale != 0)
            {
                Time.timeScale = 0;
            }
            display();
        }
	}
}
