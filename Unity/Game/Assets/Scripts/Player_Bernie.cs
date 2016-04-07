using UnityEngine;
using System.Collections;

public class Player_Bernie : MonoBehaviour
{
	public float speed = 10f;
	public Vector2 maxVelocity = new Vector2 (3, 3);
	private PlayerController controller;
	private Animator anim;
	public Collider2D attackTriggerX; // Attack collider X-axis
	public Collider2D attackTriggerYup; // Attack collider going up Y-axis
	public Collider2D attackTriggerYdown; // Attack collider going down Y-axis
	public Projectile projectile;
	
	void Start ()
	{
		controller = GetComponent<PlayerController> ();
		anim = GetComponent<Animator> ();
		// Set all triggers to false at beginning
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
		
		// Set all values to false initially
		anim.SetBool("AttackX", false);
		anim.SetBool("AttackY", false);
		anim.SetBool("Attack0", false);
		attackTriggerX.enabled = false;
		attackTriggerYup.enabled = false;
		attackTriggerYdown.enabled = false;
		
		// If you are moving on x axis and NOT attacking
		if (controller.moving.x != 0 && controller.attack == false) {
			// Disable x attack trigger
			attackTriggerX.enabled = false;
			if (absVelX < maxVelocity.x) {
				forceX = speed * controller.moving.x;
				// If you are moving right
				if (controller.moving.x == 1) {
					// Move right animation
					anim.SetInteger ("AnimState", 1);
					transform.localScale = new Vector3 (1, 1, 1);
				} 
				// If you are moving left
				else if (controller.moving.x == -1) {
					// Move left animation
					anim.SetInteger ("AnimState", 1);
					transform.localScale = new Vector3 (-1, 1, 1);
				}
			}
		} 
		
		// If you are moving on x axis and attacking
		else if (controller.moving.x != 0 && controller.attack == true) {
			// Enable x attack trigger
			attackTriggerX.enabled = true;
			if (absVelX < maxVelocity.x) {
				forceX = speed * controller.moving.x;
				// If you are moving right
				if (controller.moving.x == 1) {
					// Move right attack animation
					anim.SetBool("AttackX", true);
					transform.localScale = new Vector3 (1, 1, 1);
				} 
				// If you are moving left
				else if (controller.moving.x == -1) {
					// Move left attack animation
					anim.SetBool("AttackX", true);
					transform.localScale = new Vector3 (-1, 1, 1);
				}
			}
		}
		
		// If you are moving on y axis and NOT attacking
		else if (controller.moving.y != 0 && controller.attack == false) {
			// Disable y attack triggers
			attackTriggerYup.enabled = false;
			attackTriggerYdown.enabled = false;
			if (absVelY < maxVelocity.y) {
				forceY = speed * controller.moving.y;
				// If you are moving down
				if (controller.moving.y == 1) {
					// Move down animation
					anim.SetInteger ("AnimState", 2);
				} 
				// If you are moving up
				else if (controller.moving.y == -1) {
					// Move up animation
					anim.SetInteger ("AnimState", 3);
				}
			}
		} 
		
		// If you are moving on y axis and attacking
		else if (controller.moving.y != 0 && controller.attack == true) {
			if (absVelY < maxVelocity.y) {
				forceY = speed * controller.moving.y;
				// If you are moving down
				if (controller.moving.y == 1) {
					// Enable y down attack trigger
					// Move down attack animation
					attackTriggerYdown.enabled = true;
					anim.SetBool("AttackY", true);
				} 
				// If you are moving up
				else if (controller.moving.y == -1) {
					// Enable y up attack trigger
					// Move up attack animation
					attackTriggerYup.enabled = true;
					anim.SetBool("AttackY", true);
				}
			}
		} 
		
		// If you are moving on neither axis and NOT attacking
		else if((controller.moving.x == 0 && controller.moving.y == 0) && !controller.attack){
			// You are still
			anim.SetInteger ("AnimState", 0);
			attackTriggerX.enabled = false;
		}
		
		// if you are moving on neither axis and attacking
		else if((controller.moving.x == 0 && controller.moving.y == 0) && controller.attack){
			// Perform still attack
			anim.SetBool("Attack0", true);
			attackTriggerX.enabled = true;
		}
		
		// Otherwise, you are still
		else {
			anim.SetInteger ("AnimState", 0);
			//transform.localScale = new Vector3 (1, 1, 1);
		}
		
		rigidbody2D.AddForce (new Vector2 (forceX, forceY));
	}

	void OnShoot(){
		if (projectile) {
			Projectile clone = Instantiate (projectile, transform.position, Quaternion.identity) as Projectile;
		}
	}
}
