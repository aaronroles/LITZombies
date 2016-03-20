using UnityEngine;
using System.Collections;

public class AttackTriggers : MonoBehaviour {

	public int attackDmg = 2;
	private int count = 1;

	void OnTriggerEnter2D(Collider2D zombie){
		if (zombie.gameObject.tag == "Enemy") {
			zombie.GetComponent<ZombieHealthManager>().Damage(attackDmg);
			//print (count++);
		}
	}
}
