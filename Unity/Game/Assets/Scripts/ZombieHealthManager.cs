using UnityEngine;
using System.Collections;

public class ZombieHealthManager : MonoBehaviour {

	public int health = 30;
	
	void Update () {
		if(health <= 0){
			gameObject.GetComponent<Explode>().OnExplode();
			//Destroy(gameObject);
		}
	}

	public void Damage(int dmg){
		health -= dmg;
	}
}
