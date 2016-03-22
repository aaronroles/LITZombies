using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static int score;
	Text text;

	void Awake () {
		score = 0;
		text = GetComponent<Text> ();
	}
	
	void Update () {
		text.text = "Score: " + score;
	}
}
