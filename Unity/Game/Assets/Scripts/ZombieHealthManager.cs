using UnityEngine;
using System.Collections;

public class ZombieHealthManager : MonoBehaviour {

	public int health = 30;
	public int killScore = 10;
	
	void Update () {
		// If a zombie's health is 0 (Dead)
		if(health <= 0){
			// Call it's explode function
			gameObject.GetComponent<Explode>().OnExplode();
			// Remove 1 from numZombies
			SpawnManager.numZombies -= 1;
			// Updated the score
			ScoreManager.score += killScore;
		}
	}

	// Public function so it can be accessed from
	// elsewhere. A number gets passed in and is 
	// the damage applied to zombie health
	public void Damage(int dmg){
		health -= dmg;
	}
}
