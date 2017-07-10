using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {
	public GUIContent myContent;
	public GUIStyle myStyle;
	public bool isPaused = false;

	public GameObject resumeController; 
	
	public void OnPauseButtonPressed(){
		if(isPaused == false){
			Time.timeScale = 0;
			isPaused = true;
			resumeController.SetActive(true);
		}
		else{
			Time.timeScale = 1;
			isPaused = false;
			resumeController.SetActive(false);
		}
	}

	public void OnResumeButtonPressed(){
		Time.timeScale = 1;
		isPaused = false;
		resumeController.SetActive(false);
	}

	public void OnMainMenuButtonPressed(){
		Application.LoadLevel("Menu_Scene");
	}

	public void OnQuitButtonPressed(){
		Application.Quit();
	}

/*
	void OnGUI(){
		if(isPaused){
			GUI.Box(new Rect (300,300, 500, 500), GUIContent.none,myStyle);
			if (GUI.Button(new Rect (350,350, 50,50), "BACK")){
				Time.timeScale = 1;
			    isPaused = false;
			}
		}
	}
*/

}
