using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float speed = 10f;
	public Vector2 maxVelocity = new Vector2 (3, 3);
	private PlayerController controller;
	private Animator anim;
	public Collider2D attackTriggerX;
	public Collider2D attackTriggerYup;
	public Collider2D attackTriggerYdown;

	void Start ()
	{
		controller = GetComponent<PlayerController> ();
		anim = GetComponent<Animator> ();
		attackTriggerX.enabled = false;
		attackTriggerYup.enabled = false;
		attackTriggerYdown.enabled = false;
	}
	
	void Update ()
	{
		var forceX = 0f;
		var forceY = 0f;
		var absVelX = Mathf.Abs (rigidbody2D.velocity.x);
		var absVelY = Mathf.Abs (rigidbody2D.velocity.y);

		anim.SetBool("AttackX", false);
		anim.SetBool("AttackY", false);
		anim.SetBool("Attack0", false);

		attackTriggerX.enabled = false;
		attackTriggerYup.enabled = false;
		attackTriggerYdown.enabled = false;

		if (controller.moving.x != 0 && controller.attack == false) {
			attackTriggerX.enabled = false;
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

		else if (controller.moving.x != 0 && controller.attack == true) {
			attackTriggerX.enabled = true;
			if (absVelX < maxVelocity.x) {
				forceX = speed * controller.moving.x;
				if (controller.moving.x == 1) {
					anim.SetBool("AttackX", true);
					transform.localScale = new Vector3 (1, 1, 1);
				} 
				else if (controller.moving.x == -1) {
					anim.SetBool("AttackX", true);
					transform.localScale = new Vector3 (-1, 1, 1);
				}
			}
		}

		else if (controller.moving.y != 0 && controller.attack == false) {
			attackTriggerYup.enabled = false;
			attackTriggerYdown.enabled = false;
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

		else if (controller.moving.y != 0 && controller.attack == true) {
			if (absVelY < maxVelocity.y) {
				forceY = speed * controller.moving.y;
				if (controller.moving.y == 1) {
					attackTriggerYdown.enabled = true;
					anim.SetBool("AttackY", true);
				} 
				else if (controller.moving.y == -1) {
					attackTriggerYup.enabled = true;
					anim.SetBool("AttackY", true);
				}
			}
		} 

		else if((controller.moving.x == 0 && controller.moving.y == 0) && !controller.attack){
			anim.SetInteger ("AnimState", 0);
			attackTriggerX.enabled = false;
		}

		else if((controller.moving.x == 0 && controller.moving.y == 0) && controller.attack){
			anim.SetBool("Attack0", true);
			attackTriggerX.enabled = true;
		}

		else {
			anim.SetInteger ("AnimState", 0);
			//transform.localScale = new Vector3 (1, 1, 1);
		}

		rigidbody2D.AddForce (new Vector2 (forceX, forceY));
	}
}
