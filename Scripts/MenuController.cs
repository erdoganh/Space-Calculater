using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public void OnStartButtonClicked(){
		print ("OnStartButtonClicked");
		Application.LoadLevel("Game_Scene");
	}

	public void OnOptionButtonClicked(){
		print ("OnOptionButtonClicked");		
		Application.LoadLevel("Menu_Option_Scene");
	}

	public void OnUpgradeButtonClicked(){
		print ("OnUpgradeButtonClicked");
	}

	public void OnHowToPlayButtonClicked(){
		print ("OnCubesButtonClicked");
	}

	public void OnQuitButtonClicked(){
		print ("OnQuitButtonClicked");
		Application.Quit();
	}

	public void OnBackButtonClicked(){
		print ("OnQuitButtonClicked");
		Application.LoadLevel("Menu_Scene");
	}

}
