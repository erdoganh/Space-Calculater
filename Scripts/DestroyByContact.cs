using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	private int scoreValue = 10;
	private GameController gameController;
	private PlayerController playerController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		GameObject playerObject = GameObject.FindWithTag ("Player");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
			playerController = playerObject.GetComponent<PlayerController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Boundary")
		{
			return;
		}
		else if (other.tag == "Player")
		{
			Destroy(gameObject);
			playerController.decreasePlayerHealth();
			Instantiate(playerExplosion	, other.transform.position, other.transform.rotation);
			Instantiate(explosion, transform.position, transform.rotation);
			if(playerController.playerHealth == 0){
				//Destroy(other.gameObject); yada GameOver
			}
		}
		else if (other.gameObject.tag == "Bolt1") {
			Destroy(gameObject);	
			Destroy(other.gameObject);	
			Instantiate(explosion, transform.position, transform.rotation);
			gameController.AddScore(1);
		}
		else if (other.gameObject.tag == "Bolt2") {
			Destroy(gameObject);
			Destroy(other.gameObject);
			Instantiate(explosion, transform.position, transform.rotation);
			gameController.changeCurrentNumber(gameObject.name);
		}
		else if(other.gameObject.tag == "Box"){
			Destroy(gameObject);	
			Destroy(other.gameObject);
			Instantiate(explosion, transform.position, transform.rotation);
		}
	}
}