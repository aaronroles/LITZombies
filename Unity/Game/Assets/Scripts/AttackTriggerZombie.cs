using UnityEngine;
using System.Collections;

public class AttackTriggerZombie : MonoBehaviour {

	public int zombieAttack = 5;

	// ***ONCOLLISION VS ONTRIGGER*** 
	void OnTriggerEnter2D(Collider2D other){
		// If zombies collides with Player object
		if (other.gameObject.tag == "Player") {
			// Apply zombie attack damage to it's health
			other.GetComponent<PlayerHealthManager>().Damage(zombieAttack);
		}
	}
}
