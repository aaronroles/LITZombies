using UnityEngine;
using System.Collections;

public class ZombieHealthManager : MonoBehaviour {

	public int health = 30;
	public int killScore = 10;
	
	void Update () {
		if(health <= 0){
			gameObject.GetComponent<Explode>().OnExplode();
			SpawnManager.spawnCounter -= 1;
			ScoreManager.score += killScore;
			//Destroy(gameObject);
		}
	}

	public void Damage(int dmg){
		health -= dmg;
	}
}
