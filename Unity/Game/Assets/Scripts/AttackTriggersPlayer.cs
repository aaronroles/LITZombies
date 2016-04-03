using UnityEngine;
using System.Collections;

public class AttackTriggersPlayer : MonoBehaviour {

	public int playerAttack = 2;
	
	// ***ONCOLLISION VS ONTRIGGER*** 
	void OnTriggerEnter2D(Collider2D other){
		// If Player collides with Enemy object
		if (other.gameObject.tag == "Enemy") {
			// Apply player attack damage to it's health
			other.GetComponent<ZombieHealthManager>().Damage(playerAttack);
		}
	}
}
