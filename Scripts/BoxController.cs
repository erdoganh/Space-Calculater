using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour {

	private bool isMouseClicked;
	private Vector3 clickedPosition;

	void Awake(){
		isMouseClicked = false;
	}

	void FixedUpdate(){

		if(isMouseClicked && Input.GetMouseButtonUp(0) == false){
			//Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			float angle = GetAngle(clickedPosition, transform.position);
			transform.rotation = Quaternion.Euler(0f, 0f, angle);
		}
		else{
			isMouseClicked = false;
		}
	}

	float GetAngle(Vector3 position1, Vector3 position2){
		float x = position1.x - position2.x;
		float y = position1.y - position2.y;
		float angle = 0;
		if( x>0 && y>0){
			angle = ( Mathf.Atan(y/x) )* 180 /Mathf.PI;
		}
		else if( x<0 && y>0){
			angle = ( Mathf.Atan(-x/y) )* 180 /Mathf.PI + 90;
		}
		else if( x>0 && y<0){
			angle = ( Mathf.Atan(y/x) )* 180 /Mathf.PI;
		}
		else if( x<0 && y<0){
			angle = ( Mathf.Atan(-y/-x) )* 180 /Mathf.PI + 180;
		}
		return angle;
	}

	void ChangeRotataion(float angle1, float angle2){
		float angle = angle1 - angle2 + transform.rotation.z;
		transform.rotation = Quaternion.Euler(0f, 0f, angle);
	}

	void OnMouseDown(){
		if(Input.GetMouseButtonDown(0)){
			isMouseClicked = true;
		}
	}

}
