﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static int score;
	Text scoreText;
	int currentHighScore;

	void Awake () {
		score = 0;
		scoreText = GetComponent<Text> ();
		// If there is a key for HighScore
		if(PlayerPrefs.HasKey("HighScore")){
			currentHighScore = PlayerPrefs.GetInt("HighScore");
		}
		else{
			// Otherwise, no high score
			PlayerPrefs.SetInt("HighScore", 0);
		}
	}
	
	void Update () {
		// Update the points on screen
		// Score incremented from ZombieHealthManager
		scoreText.text = "Coins: " + score;
		// If the score is bigger than the current high score
		if (score > currentHighScore) {
			// The current high score is now score
			currentHighScore = score;
			// Set the score to the high score key
			PlayerPrefs.SetInt("HighScore", score);
			print ("new high score: " + currentHighScore);
		}
	}
}
