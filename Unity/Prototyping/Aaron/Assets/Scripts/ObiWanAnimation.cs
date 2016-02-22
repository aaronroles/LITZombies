using UnityEngine;
using System.Collections;

public class ObiWanAnimation : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {	
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (rigidbody2D.velocity.x > 0.2) {
			animator.SetBool("idle", false);
			animator.SetBool("walkRight", true);
			animator.SetBool("walkLeft", false);
		}
		if (rigidbody2D.velocity.x < -0.2) {
			animator.SetBool("idle", false);
			animator.SetBool("walkRight", false);
			animator.SetBool("walkLeft", true);
		}
		if (rigidbody2D.velocity.x > -0.2 && rigidbody2D.velocity.x < 0.2) {
			animator.SetBool("idle", true);
			animator.SetBool("walkRight", false);
			animator.SetBool("walkLeft", false);
		}
	}
}
