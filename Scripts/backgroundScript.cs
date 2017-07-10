using UnityEngine;
using System.Collections;

public class backgroundScript : MonoBehaviour {
	public float speedOfBackground = 0.05f;
	public GameObject background1;
	public GameObject background2;
	private Vector3 startPositionB1;
	private Vector3 startPositionB2;
	// Use this for initialization
	void Start () {
		startPositionB1 = background2.transform.position;
		startPositionB2 = background1.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		background2.transform.position += new Vector3 (0f,0f,-speedOfBackground);
		background1.transform.position += new Vector3 (0f,0f,-speedOfBackground);
		if(background1.transform.position.z <= startPositionB1.z){
			background2.transform.position = startPositionB2;
			GameObject temp = background2;
			background2 = background1;
			background1 = temp;
		}
	}
}
