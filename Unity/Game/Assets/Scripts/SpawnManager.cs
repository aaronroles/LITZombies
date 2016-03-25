using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public GameObject enemy;
	public Transform[] spawnPoints;
	public static int spawnCounter = 3;
	private int spawnCount = 3;

	void Start () {
		// On Start, call spawn
		Spawn ();
	}

	void Update(){
		if (spawnCounter == 0) {
			spawnCount += 2;
			spawnCounter = spawnCount;
			RoundManager.round++;
			Spawn ();
		}
	}

	void Spawn(){
		for (int i=0; i<spawnCounter; i++) {
			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			Instantiate (enemy, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
		}
	}
}
