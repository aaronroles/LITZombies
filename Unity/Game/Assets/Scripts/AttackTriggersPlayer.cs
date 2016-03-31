using UnityEngine;
using System.Collections;

public class AttackTriggersPlayer : MonoBehaviour {

	public int playerAttack = 2;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Enemy") {
			other.GetComponent<ZombieHealthManager>().Damage(playerAttack);
		}
	}
}
