using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Vector2 moving = new Vector2();
	
	// Update is called once per frame
	void Update () {
		moving.x = moving.y = 0;

		if(Input.GetKey(KeyCode.A)){
			print("a");
			moving.x = -1;
		}
		else if(Input.GetKey(KeyCode.D)){
			print("d");
			moving.x = 1;
		}

		if (Input.GetKey (KeyCode.W)) {
			print("w");
			moving.y = 1;
		}
		else if(Input.GetKey(KeyCode.S)){
			print("s");
			moving.y = -1;
		}
	}
}
