using UnityEngine;
using System.Collections;

public class AttackTriggers : MonoBehaviour {

	public int playerAttack = 2;
	public int zombieAttack = 5;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Enemy") {
			other.GetComponent<ZombieHealthManager>().Damage(playerAttack);
		}

		if (other.gameObject.tag == "Player") {
			other.GetComponent<PlayerHealthManager>().Damage(zombieAttack);
		}
	}
}
