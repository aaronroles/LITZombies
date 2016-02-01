using UnityEngine;
using System.Collections;

public class MyFirstScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var testArray = new string[2];
		testArray [0] = "Aaron";
		testArray [1] = "Roles";
		var myName = testArray [0] + " " + testArray [1];
		Debug.Log (myName);
		print (myName);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
