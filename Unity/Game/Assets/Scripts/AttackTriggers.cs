using UnityEngine;
using System.Collections;

public class AttackTriggers : MonoBehaviour {

	public int playerAttack = 2;
	public int zombieAttack = 5;
	private int count = 1;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Enemy") {
			other.GetComponent<ZombieHealthManager>().Damage(playerAttack);
			//print (count++);
		}

		if (other.gameObject.tag == "Player") {
			other.GetComponent<PlayerHealthManager>().Damage(zombieAttack);
			//print (count++);
		}
	}
}
