using UnityEngine;
using System.Collections;

public class PlatformBehaviour : MonoBehaviour {
	private Vector3 sourcePosition1;
	private Vector3 sourcePosition2;
	private bool actionLock;

	public float rotationTime = 0.2f;
	private float timer = 0.0f;
	// Use this for initialization
	void Start () {
		actionLock = false;
	}

	// Update is called once per frame
	void Update () {
		if (actionLock) {
			if(timer < rotationTime) {
				timer += Time.deltaTime;
			}
			else{ 
				sourcePosition2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				float angle1 = GetAngle(sourcePosition1, transform.position);
				float angle2 = GetAngle(sourcePosition2, transform.position);
				float angle = angle2 - angle1 + transform.rotation.z;
				print(" transform.eulerAngles.z : " + transform.eulerAngles.z + " angle:" + angle);
				transform.rotation = Quaternion.Euler(0f, 0f, angle);
				sourcePosition2 = sourcePosition1;
				timer = 0.0f; 
			}
			/*
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			float angle = Mathf.Atan2 (pos.y - transform.position.y, pos.x - transform.position.x);
			Vector3 rot = transform.eulerAngles;
			rot.z += (angle - sourceAngle) * Mathf.Rad2Deg ;
			transform.eulerAngles = rot ;
			*/
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

	void OnMouseDown () {
		sourcePosition1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		actionLock = true;
	}

	void OnMouseUp () {
		actionLock = false;
	}
}
