using UnityEngine;
using System.Collections;

public class TouchControls : MonoBehaviour {

	public GUITexture leftShot;
	public GUITexture rightShot;

	public bool IsLeftShotPressed;
	public bool IsRightShotPressed;

	void Update () {
		if(leftShot.HitTest(Input.mousePosition) && Input.GetMouseButtonDown(0)){
			print("left shot");
			IsLeftShotPressed = true;
		}

		if(rightShot.HitTest(Input.mousePosition) && Input.GetMouseButtonDown(0)){
			print("right shot");
			IsRightShotPressed = true;
		}

		Rect leftSideOfScreen = new Rect(0, 0, Screen.width/2, Screen.height);
		if (Input.GetMouseButtonDown(0) && leftSideOfScreen.Contains(Input.mousePosition))
		{
			Debug.Log("Left side!");            
		}

		Rect rightSideOfScreen = new Rect(Screen.width/2 , 0, Screen.width/2, Screen.height);
		if (Input.GetMouseButtonDown(0) && rightSideOfScreen.Contains(Input.mousePosition))
		{
			Debug.Log("right side!");            
		}
	}

}
