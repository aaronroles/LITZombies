using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public GameObject enemy;
	public float spawnTime = 0.1f;
	public Transform[] spawnPoints;

	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Spawn(){
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	} 
}
