using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundManager : MonoBehaviour {


	public static int round;
	Text roundText;

	void Awake () {
		round = 1;
		roundText = GetComponent<Text> ();
	}
	
	void Update () {
		// Show the current round on screen
		// Round incremented from SpawnManager
		roundText.text = "ROUND " + round;
	}
}
