using UnityEngine;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour {

	public int health = 50;
	
	void Update () {
		if(health <= 0){
			Destroy(gameObject);
		}
	}
	
	public void Damage(int dmg){
		health -= dmg;
	}
}
