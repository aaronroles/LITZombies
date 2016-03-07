using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 10f;
	public Vector2 maxVelocity = new Vector2(3,3);
	private PlayerController controller;
	private Animator anim;

	void Start () {
		controller = GetComponent<PlayerController> ();
		anim = GetComponent<Animator> ();
	}
	
	void Update () {
		var forceX = 0f;
		var forceY = 0f;
		var absVelX = Mathf.Abs (rigidbody2D.velocity.x);
		var absVelY = Mathf.Abs (rigidbody2D.velocity.y);

		if (controller.moving.x != 0) {
			if (absVelX < maxVelocity.x) {
				forceX = speed * controller.moving.x;
				if (controller.moving.x == 1) {
					anim.SetInteger ("AnimState", 1);
					transform.localScale = new Vector3 (1, 1, 1);
				} 
				else if (controller.moving.x == -1) {
					anim.SetInteger ("AnimState", 1);
					transform.localScale = new Vector3 (-1, 1, 1);
				}
			}
		}

		else if (controller.moving.y != 0) {
			if (absVelY < maxVelocity.y) {
				forceY = speed * controller.moving.y;
				if (controller.moving.y == 1) {
					anim.SetInteger ("AnimState", 2);
				} 
				else if (controller.moving.y == -1) {
					anim.SetInteger ("AnimState", 3);
				}
			}
		}

		else {
			anim.SetInteger("AnimState", 0);
			transform.localScale = new Vector3 (1, 1, 1);
		}

		rigidbody2D.AddForce(new Vector2(forceX, forceY));
	}
}
