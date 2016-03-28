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
		roundText.text = "ROUND " + round;
	}
}
