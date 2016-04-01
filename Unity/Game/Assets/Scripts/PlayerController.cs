using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Vector2 moving = new Vector2();
	public bool attack = false;

	// Update is called once per frame
	void Update () {
		// X and Y to zero when not affected
		moving.x = moving.y = 0;

		// If pressing A (go left)
		if(Input.GetKey(KeyCode.A)){
			// Moving to -1 on x (left)
			moving.x = -1;
		}
		// If pressing D (go right)
		else if(Input.GetKey(KeyCode.D)){
			// Moving to 1 on x (right)
			moving.x = 1;
		}

		// If pressing W (go up)
		if (Input.GetKey (KeyCode.W)) {
			// Moving to 1 on y (up)
			moving.y = 1;
		}
		// If pressing S (go down)
		else if(Input.GetKey(KeyCode.S)){
			// Moving to -1 on y (down)
			moving.y = -1;
		}

		// If pressing left mouse button
		if (Input.GetMouseButton (0)) {
			// You are attacking
			attack = true;
		} 
		// Otherwise
		else {
			// You are not attacking
			attack = false;
		}

	}
}
