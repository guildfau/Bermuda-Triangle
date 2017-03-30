using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Exit script
	public void DoExit(){
		Application.Quit ();
	}

	public void Play(){
		SceneManager.LoadScene ("Test");
	}
}
