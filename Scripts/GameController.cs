using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{	
	public GameObject[] numberBoxes;
	public GameObject[] operationBoxes;
	public GameObject[] specialBoxes;

	public int numberCount = 9;
	public int operationCount = 7;
	public int specialCount = 5;

	private GameObject[] boxGameObjects;

	public UILabel scoreText;
	private int score;
	public int scoreMultiplier = 1000;

	public UILabel memoryText;
	private int memoryNumber;

	public UILabel aimText;
	private int aimNumber;
	public UILabel currentText;
	private int currentNumber1;
	private int currentNumber2;

	public Vector3 spawnValues;
	public int waveTotalBoxCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public int minForAimRange = 20;
	public int maxForAimRange = 30;

	private string operation;
	private int numberOfOperation;
	
	void Start ()
	{
		score = 0;
		UpdateScore ();
		aimNumber = Random.Range (minForAimRange, maxForAimRange);
		aimText.text = aimNumber.ToString ();
		UpdateCurrentNumberText ();

		memoryText.text = ":" + 0;

		numberOfOperation = 0;
		currentNumber1 = 0;
		currentNumber2 = 0;
		memoryNumber = 0;
		operation = "";

		StartCoroutine (SpawnWaves ());
	}
/*-----------------------------------------------------------------------------------*/
	public void AddScore(int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}
/*-----------------------------------------------------------------------------------*/
	public void UpdateScore(){
		scoreText.text = ":" + score.ToString ();
	}
/*-----------------------------------------------------------------------------------*/
	public void UpdateCurrentNumberText(){
		if(currentNumber1 == 0){
			currentText.text = currentNumber1.ToString();
		}
		else if (currentNumber1 != 0 && currentNumber2 == 0) {
			currentText.text = currentNumber1.ToString () + operation;
		}
		else{
			currentText.text = currentNumber1.ToString () + operation + currentNumber2.ToString () ;
		}
	}
/*-----------------------------------------------------------------------------------*/
	public void FreezeAllBoxes(){
		boxGameObjects = GameObject.FindGameObjectsWithTag("Box");
		for (int i=0; i<boxGameObjects.Length; i++) {
			boxGameObjects[i].rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
			boxGameObjects[i].rigidbody.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
		}
	}
/*-----------------------------------------------------------------------------------*/
public void MakeSlowAllBoxes(){
	boxGameObjects = GameObject.FindGameObjectsWithTag("Box");
	for (int i=0; i<boxGameObjects.Length; i++) {
		boxGameObjects[i].rigidbody.velocity = new Vector3(0.0f, 0.0f, -2.0f);
	}
}
/*-----------------------------------------------------------------------------------*/
public void MakeFastAllBoxes(){
	boxGameObjects = GameObject.FindGameObjectsWithTag("Box");
	for (int i=0; i<boxGameObjects.Length; i++) {
		boxGameObjects[i].rigidbody.velocity = new Vector3(0.0f, 0.0f, -8.0f);
	}
}
/*-----------------------------------------------------------------------------------*/
	public void changeCurrentNumber(string nameOfBox){
		numberOfOperation++;
		if( nameOfBox[0] == 'o' && currentNumber1 !=0 ){
			//Toplama +
			if(nameOfBox[2] == '1'){
				operation = "+";
			}
			//Çıkarma -
			else if(nameOfBox[2] == '2'){
				operation = "-";
			}
			//Çarpma x
			else if(nameOfBox[2] == '3'){
				operation = "x";
			}
			//Bölme ÷
			else if(nameOfBox[2] == '4'){
				operation = "÷";
			}
			//kare alma
			else if(nameOfBox[2] == '5'){
				currentNumber1 = (int)Mathf.Pow( (float)currentNumber1,2.0f );
				currentNumber2 = 0;
				operation = "";
			}
			//küp alma
			else if(nameOfBox[2] == '6'){
				currentNumber1 = (int)Mathf.Pow( (float)currentNumber1,3.0f );
				currentNumber2 = 0;
				operation = "";
			}
			//kök alma
			else if(nameOfBox[2] == '7'){
				currentNumber1 = (int)Mathf.Pow( (float)currentNumber1,0.5f );
				currentNumber2 = 0;
				operation = "";
			}
		}
		else if (nameOfBox [0] == 'o' && currentNumber1 == 0) {
			operation = "";
		}
		else if(nameOfBox[0] == 's'){
			print("In memory:" + nameOfBox[7]);
			if( nameOfBox[7] == '1' ){
				if(memoryNumber == 0){
					memoryNumber = currentNumber1;
					memoryText.text = ":" + memoryNumber;
					currentNumber1 = 0;
					operation = "";
				}
				else{
					if(operation == ""){
						currentNumber1 = memoryNumber;
						operation = "";
					}
					else{ 
						currentNumber2 = memoryNumber; 
						makeOperation();
					}
					memoryNumber = 0;
					memoryText.text = ":" + memoryNumber;
				}
			}
			else if( nameOfBox[7] == '2' ){
				FreezeAllBoxes();
			}
			else if( nameOfBox[7] == '3' ){
				MakeSlowAllBoxes();
			}
			else if( nameOfBox[7] == '4' ){
				MakeFastAllBoxes();
			}
			else if( nameOfBox[7] == '5' ){
				if(GameObject.FindWithTag("Player").GetComponent<PlayerController>().isWineBottleTaken == false){
					GameObject.FindWithTag("Player").GetComponent<PlayerController>().isWineBottleTaken = true;
				}
				else{
					GameObject.FindWithTag("Player").GetComponent<PlayerController>().isWineBottleTaken = false;
				}
			}
		}
		else{
			if( currentNumber1 == 0 ){
				currentNumber1 = nameOfBox[0] - 48;
			}
			else if(currentNumber1 != 0 && operation ==""){
				currentNumber1 =  nameOfBox[0] - 48;
			}
			else{
				currentNumber2 = nameOfBox[0] - 48;
				makeOperation();
			}
		}
		controlAimAndCurrent ();
		UpdateCurrentNumberText ();
	}
/*-----------------------------------------------------------------------------------*/
	public void makeOperation(){
		if(operation == "+"){
			currentNumber1 = (currentNumber1)+(currentNumber2);
		}
		else if(operation == "-"){
			currentNumber1 = (currentNumber1)-(currentNumber2);
		}
		else if(operation == "x"){
			currentNumber1 = (currentNumber1)*(currentNumber2);
		}
		else if(operation == "÷"){
			currentNumber1 = (currentNumber1)/(currentNumber2);
		}
		currentNumber2 = 0;
		operation = "";
	}
/*-----------------------------------------------------------------------------------*/
	public void controlAimAndCurrent(){
		if(currentNumber1 == aimNumber){
			currentNumber1 = 0;
			aimNumber = Random.Range (minForAimRange, maxForAimRange);
			aimText.text = aimNumber.ToString ();
			AddScore( scoreMultiplier/numberOfOperation );
			numberOfOperation = 0;
		}
	}
/*-----------------------------------------------------------------------------------*/	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			int memoryBoxCount = 0;
			int boxType1Count = 0;
			int boxType2Count = 0;
			int boxType3Count = 0;
			for (int i = 0; i < waveTotalBoxCount; i++)
			{
				int boxType = Random.Range (0, 3);
				bool boxNumberControl = false;
				while( boxNumberControl == false ){
					if(boxType == 0 && boxType1Count < waveTotalBoxCount*0.5){ 
						boxType1Count++;
						boxNumberControl = true;
					}
					else if(boxType == 1 && boxType2Count < waveTotalBoxCount*0.25){ 
						boxType2Count++;
						boxNumberControl = true; 
					}
					else if(boxType == 2 && boxType3Count < waveTotalBoxCount*0.25){ 
						boxType3Count++;
						boxNumberControl = true; 
					}
					else{
						boxType = Random.Range (0, 3);
					}
				}
				if(boxType == 0 ){
					int selection1 = Random.Range (0, numberCount);
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (numberBoxes[selection1], spawnPosition, spawnRotation);
				}
				else if(boxType == 1 ){
					int selection2 = Random.Range (0, operationCount);
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (operationBoxes[selection2], spawnPosition, spawnRotation);	
				}
				else if(boxType == 2 ){
					int selection3 = Random.Range (0, specialCount);
					Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					Instantiate (specialBoxes[selection3], spawnPosition, spawnRotation);	
			}
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	 }
/*-----------------------------------------------------------------------------------*/	
}