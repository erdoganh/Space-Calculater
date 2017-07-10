using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public GUITexture leftShot;
	public GUITexture rightShot;

	public UITexture _rightShot;
	public UITexture _leftShot;

	private bool isLeftSide, isRightSide, isLeftShot, isRightShot;

	public UILabel healthText;
	public int playerHealth = 100;
	public int damageOfBox = 40;

	public bool isWineBottleTaken = false;

	public bool canMoveVertical = false;
	public float speed;
	public float tilt;
	public Boundary boundary;
	
	public GameObject shot1;
	public GameObject shot2;
	public Transform shotSpawn;
	public float fireRate;

	public AudioSource weapenSound1;
	public AudioSource weapenSound2;

	private float nextFire;
	private AmmoController ammoController;

	void Start(){
		isLeftShot = isRightShot = isLeftSide = isRightSide = false;

		healthText.text = ":" + playerHealth; 
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		ammoController = gameControllerObject.GetComponent <AmmoController>();
	}

	public void decreasePlayerHealth(){
		if (playerHealth > damageOfBox) {
			playerHealth = playerHealth - damageOfBox;
			healthText.text = ":" + playerHealth;
		} 
		else {
			playerHealth = 0;
			healthText.text = ":" + playerHealth;
		}
	}

	void Update ()
	{
		if (Time.time > nextFire)
		{
			if( isLeftShot ){ //&& ammoController.ammo1 >0 
				nextFire = Time.time + fireRate;
				Instantiate(shot1, shotSpawn.position, shotSpawn.rotation);
				weapenSound1.Play();
				//ammoController.DecreaseAmmo1(1);
			}
			else if( isRightShot ){ // && ammoController.ammo2 >0
				nextFire = Time.time + fireRate;
				Instantiate(shot2, shotSpawn.position, shotSpawn.rotation);
				weapenSound2.Play();
				//ammoController.DecreaseAmmo2(1);
			}
		}
	}
	
	void FixedUpdate ()
	{
		ControlTouch ();
		//ResizeShotTexture ();
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = 0;
		if( canMoveVertical ){
			moveVertical = Input.GetAxis ("Vertical");
		}

		if (isLeftSide){ moveHorizontal = -1f; }
		if (isRightSide){ moveHorizontal = 1f; }


		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		if (isWineBottleTaken) {
			movement = new Vector3 (-moveHorizontal, 0.0f, moveVertical);
		}
		rigidbody.velocity = movement * speed;
		
		rigidbody.position = new Vector3 
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
				);
		
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}


	void ControlTouch(){
		Rect leftSideOfScreen = new Rect(0, 75, Screen.width/2, Screen.height - 150);
		Rect rightSideOfScreen = new Rect(Screen.width/2 , 75, Screen.width/2, Screen.height - 150);
		
		if( leftShot.HitTest (Input.mousePosition) && Input.GetMouseButton(0)){ 
			isLeftShot = true;
		}
			
		else if(rightShot.HitTest (Input.mousePosition) && Input.GetMouseButton(0)){ 
			isRightShot = true;
		}
			
		else if ( leftSideOfScreen.Contains(Input.mousePosition) && Input.GetMouseButton(0))
		{
			isLeftSide = true;
		}
			
		else if ( rightSideOfScreen.Contains(Input.mousePosition) && Input.GetMouseButton(0))
		{
			isRightSide = true;
		}

		else{
			isLeftShot = isRightShot = isLeftSide = isRightSide = false;
		}
	}
	

}