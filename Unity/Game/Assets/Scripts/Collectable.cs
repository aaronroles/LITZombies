using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

	// If there is a triggered collision
	void OnTriggerEnter2D(Collider2D target){
		// If the other object is the player
		if (target.gameObject.tag == "Player") {
			// Destroy the current object
			Destroy(gameObject);
			// Do collectable stuff
		}
	}
}
