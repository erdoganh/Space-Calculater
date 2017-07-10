using UnityEngine;
using System.Collections;

public class VocableList : MonoBehaviour {
	
	public string[] myVocableList;
	private int sizeOfList;

	void Start () {
		sizeOfList = 6;
		myVocableList = new string[sizeOfList];
		myVocableList [0] = "AB";
		myVocableList [1] = "BCD";
		myVocableList [2] = "CDE";
		myVocableList [3] = "DEF";
		myVocableList [4] = "EFG";
		myVocableList [5] = "FG";
	}

	public bool IsInList(string _string){
		for(int i=0 ; i<6 ; i++){
			if(myVocableList[i] == _string){
				return true;
			}
		}
		return false;
	}

	public string GetWordWithIndex(int index){
		return myVocableList [index];
	}
}
