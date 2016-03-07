using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player") {
			Destroy(gameObject);
		}
	}
}
