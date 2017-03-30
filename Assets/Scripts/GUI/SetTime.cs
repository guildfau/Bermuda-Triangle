using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTime : MonoBehaviour {


	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Text>().text = "0";
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Text>().text = Player_Stats.stats.getTime().ToString();
    }
}
