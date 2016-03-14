using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour {

	private Vector3 player;
	private Vector2 playerDirection;
	private float xDiff;
	private float yDiff;
	public float speed;
	private float distance;
	private Animator anim;

	void Start () {
		speed = 3;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindWithTag ("Player").transform.position;
		xDiff = player.x - transform.position.x;
		yDiff = player.y - transform.position.y;
		playerDirection = new Vector2 (xDiff, yDiff);
		distance = Vector2.Distance (player, transform.position);

		if (distance < 1.5) {
			rigidbody2D.AddForce (playerDirection.normalized * speed);
		}


	}
}
