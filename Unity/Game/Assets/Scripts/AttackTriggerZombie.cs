using UnityEngine;
using System.Collections;

public class AttackTriggerZombie : MonoBehaviour {

	public int zombieAttack = 5;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			other.GetComponent<PlayerHealthManager>().Damage(zombieAttack);
		}
	}
}
