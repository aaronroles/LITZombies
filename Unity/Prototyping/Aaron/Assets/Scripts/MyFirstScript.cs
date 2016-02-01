using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyFirstScript : MonoBehaviour {

	public List<string> list = new List<string>();

	// Use this for initialization
	void Start () {
		var testArray = new string[2];
		testArray [0] = "Aaron";
		testArray [1] = "Roles";
		var myName = testArray [0] + " " + testArray [1];
		Debug.Log (myName);
		print (myName);

		list.Add ("Aaron");
		list.Add ("Roles");
		print (list[0] + " " + list[1]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
