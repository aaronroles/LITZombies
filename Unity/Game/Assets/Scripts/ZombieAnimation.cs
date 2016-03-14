using UnityEngine;
using System.Collections;

public class ZombieAnimation : MonoBehaviour {

	private Animator animator;

	void Start () {
		animator = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		print (rigidbody2D.velocity.x);
		if(rigidbody2D.velocity.x > 0.2){
			print ("Greater than 0.2");
			animator.SetBool("ZombieStill", false);
			animator.SetBool("ZombieSideWalk", true);
			animator.SetBool("ZombieFrontWalk", false);
			animator.SetBool("ZombieBackWalk", false);
		}
		else if(rigidbody2D.velocity.x < -0.2){
			print ("Less than -0.2");
			animator.SetBool("ZombieStill", false);
			animator.SetBool("ZombieSideWalk", true);
			animator.SetBool("ZombieFrontWalk", false);
			animator.SetBool("ZombieBackWalk", false);
		}
		else if(rigidbody2D.velocity.x > -0.2 && rigidbody2D.velocity.x < 0.2){
			print ("Idle");
			animator.SetBool("ZombieStill", true);
			animator.SetBool("ZombieSideWalk", false);
			animator.SetBool("ZombieFrontWalk", false);
			animator.SetBool("ZombieBackWalk", false);
		}
		else if(rigidbody2D.velocity.y > -0.2){
			print ("up");
			animator.SetBool("ZombieStill", false);
			animator.SetBool("ZombieSideWalk", false);
			animator.SetBool("ZombieFrontWalk", false);
			animator.SetBool("ZombieBackWalk", true);
		}
		else if(rigidbody2D.velocity.y < 0.2){
			print ("down");
			animator.SetBool("ZombieStill", false);
			animator.SetBool("ZombieSideWalk", false);
			animator.SetBool("ZombieFrontWalk", true);
			animator.SetBool("ZombieBackWalk", false);
		}
		else if(rigidbody2D.velocity.y > -0.2 && rigidbody2D.velocity.y < 0.2){
			print ("Idle2");
			animator.SetBool("ZombieStill", true);
			animator.SetBool("ZombieSideWalk", false);
			animator.SetBool("ZombieFrontWalk", false);
			animator.SetBool("ZombieBackWalk", false);
		}

	}
}
