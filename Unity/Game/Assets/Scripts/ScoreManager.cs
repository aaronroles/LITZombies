using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static int score;
	Text scoreText;

	void Awake () {
		score = 0;
		scoreText = GetComponent<Text> ();
	}
	
	void Update () {
		// Update the points on screen
		// Score incremented from ZombieHealthManager
		scoreText.text = "Points: " + score;
	}
}
