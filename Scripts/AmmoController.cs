using UnityEngine;
using System.Collections;

public class AmmoController : MonoBehaviour {

	public int ammo1 = 1000;
	public int ammo2 = 1000;
	public GUIText ammoText1;
	public GUIText ammoText2;

	void Awake(){
		PrintAmmo1 ();
		PrintAmmo2 ();
	}

	public void PrintAmmo1(){
		//ammoText1.text = "ammo: " + ammo1.ToString (); 
	}

	public void PrintAmmo2(){
		//ammoText2.text = "ammo: " + ammo2.ToString (); 
	}

	public void IncreaseAmmo1(int amount){
		ammo1 = ammo1 + amount;
		PrintAmmo1 ();
	}

	public void IncreaseAmmo2(int amount){
		ammo2 = ammo2 + amount;
		PrintAmmo2 ();
	}

	public void DecreaseAmmo1(int amount){
		ammo1 = ammo1 - amount;
		PrintAmmo1 ();
	}
	
	public void DecreaseAmmo2(int amount){
		ammo2 = ammo2 - amount;
		PrintAmmo2 ();
	}


}
