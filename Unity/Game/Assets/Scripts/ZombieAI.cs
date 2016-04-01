using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour {

	private Vector3 player;
	private Vector2 playerDirection;
	private float xDiff;
	private float yDiff;
	private float speed;
	private float distance;

	void Start () {
		speed = (Random.Range (3, 7));
	}
	
	void Update () {
		// If the game is paused
		if (Time.timeScale == 0) {
			// Game is paused, do nothing
		}
		// If the game is not paused
		else if(Time.timeScale == 1){
			// If the player object exists
			if (GameObject.FindWithTag ("Player")) {
				// Find it's position and track it
				player = GameObject.FindWithTag ("Player").transform.position;
				xDiff = player.x - transform.position.x;
				yDiff = player.y - transform.position.y;
				playerDirection = new Vector2 (xDiff, yDiff);
				distance = Vector2.Distance (player, transform.position);
			}

			// If the distance is close enough
			if(distance < 10) {
				// Apply a force to move the zombie 
				// towards the player
				rigidbody2D.AddForce (playerDirection.normalized * speed);
			}
		} 
		// Otherwise, no player position
		else{	
			player.x = player.y = player.z = 0;
		}
	}
}
