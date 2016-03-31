using UnityEngine;
using System.Collections;

public class ZombieAnimation : MonoBehaviour {

	private Animator animator;

	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(rigidbody2D.velocity.x > 0.1){
			animator.SetBool("ZombieStill", false);
			animator.SetBool("ZombieSideWalk", true);
			animator.SetBool("ZombieFrontWalk", false);
			animator.SetBool("ZombieBackWalk", false);
			transform.localScale = new Vector3(1,1,1); // Flips zombie to the right
		}

		if(rigidbody2D.velocity.x < -0.1){
			animator.SetBool("ZombieStill", false);
			animator.SetBool("ZombieSideWalk", true);
			animator.SetBool("ZombieFrontWalk", false);
			animator.SetBool("ZombieBackWalk", false);
			transform.localScale = new Vector3(-1,1,1); // Flips zombie to the left
		}

		if(rigidbody2D.velocity.y < -0.1){
			animator.SetBool("ZombieStill", false);
			animator.SetBool("ZombieSideWalk", false);
			animator.SetBool("ZombieFrontWalk", true);
			animator.SetBool("ZombieBackWalk", false);
		}

		if(rigidbody2D.velocity.y > 0.1){
			animator.SetBool("ZombieStill", false);
			animator.SetBool("ZombieSideWalk", false);
			animator.SetBool("ZombieFrontWalk", false);
			animator.SetBool("ZombieBackWalk", true);
		}

		if(rigidbody2D.velocity.y > -0.1 && rigidbody2D.velocity.y < 0.1 && rigidbody2D.velocity.x > -0.1 && rigidbody2D.velocity.x < 0.1){
			animator.SetBool("ZombieStill", true);
			animator.SetBool("ZombieSideWalk", false);
			animator.SetBool("ZombieFrontWalk", false);
			animator.SetBool("ZombieBackWalk", false);
		}

	}
}
