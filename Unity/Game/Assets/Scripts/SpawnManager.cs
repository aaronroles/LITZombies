using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	// The enemy to spawn (zombie)
	public GameObject enemy;
	// An array of the points zombies can spawn from
	public Transform[] spawnPoints;
	// The current no. of zombies that can spawn
	public static int numZombies = 3;
	// Another variable to keep track of zombies spawned
	private int spawnCount = 3;

	void Start () {
		// On Start, call spawn
		Spawn ();
	}

	void Update(){
		// If there are no zombies
		if (numZombies == 0) {
			// Add 2 to spawn count
			spawnCount += 2;
			// Set numZombies equal to spawnCount
			numZombies = spawnCount;
			// Increment the round number
			RoundManager.round++;
			// Call spawn with the new no. of zombies
			Spawn ();
		}
	}

	// Handles the spawning of zombies into
	// the level
	void Spawn(){
		// Loop through numZombies
		for (int i=0; i<numZombies; i++) {
			// Choose a spawnpoint
			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			// Create a zombie at that spawn point
			Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
		}
	}
}
