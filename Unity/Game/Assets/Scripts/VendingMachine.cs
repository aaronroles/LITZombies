using UnityEngine;
using System.Collections;

public class VendingMachine : MonoBehaviour {

	// When there is a collision
	void OnCollisionStay2D(Collision2D other){
		// If 'E' is pressed down
		if(Input.GetKeyDown(KeyCode.E)){
			// If the colliding object is the Player
			if(other.gameObject.tag == "Player"){
				// Do vending machine stuff
				print ("Use vending machine");
			}
		}
	}
}
