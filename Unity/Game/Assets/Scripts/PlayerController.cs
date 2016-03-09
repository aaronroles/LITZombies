using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Vector2 moving = new Vector2();
	public bool attack = false;
	
	// Update is called once per frame
	void Update () {
		moving.x = moving.y = 0;

		if(Input.GetKey(KeyCode.A)){
			moving.x = -1;
		}
		else if(Input.GetKey(KeyCode.D)){
			moving.x = 1;
		}

		if (Input.GetKey (KeyCode.W)) {
			moving.y = 1;
		}
		else if(Input.GetKey(KeyCode.S)){
			moving.y = -1;
		}

		if (Input.GetMouseButton (0)) {
			attack = true;
		} 
		else {
			attack = false;
		}
	}
}
