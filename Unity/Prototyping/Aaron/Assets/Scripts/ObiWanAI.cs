using UnityEngine;
using System.Collections;

public class ObiWanAI : MonoBehaviour {

	private Vector3 player;
	private Vector2 playerDirection;
	private float xDiff;
	private float yDiff;
	public float speed;
	private float distance;

	// Use this for initialization
	void Start () {
		speed = 5;
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindWithTag("Player").transform.position;
		xDiff = player.x - transform.position.x;
		yDiff = player.y - transform.position.y;
		playerDirection = new Vector2 (xDiff, yDiff);
		distance = Vector2.Distance (player, transform.position);
		//Debug.Log (distance);

		if (distance < 1.5) {
			//Debug.Log("Distance is "+ distance);
			rigidbody2D.AddForce (playerDirection.normalized * speed);
		}
	}
}
