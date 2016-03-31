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
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0) {
			// Game is paused, do nothing
		}
		else if(Time.timeScale == 1){
			if (GameObject.FindWithTag ("Player")) {
				player = GameObject.FindWithTag ("Player").transform.position;
				xDiff = player.x - transform.position.x;
				yDiff = player.y - transform.position.y;
				playerDirection = new Vector2 (xDiff, yDiff);
				distance = Vector2.Distance (player, transform.position);
			}
			
			if(distance < 10) {
				rigidbody2D.AddForce (playerDirection.normalized * speed);
			}
		} 

		else{	
			player.x = player.y = player.z = 0;
		}
	}
}
