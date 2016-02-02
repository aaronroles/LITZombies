using UnityEngine;
using System.Collections;

public class DarthVader : MonoBehaviour {

	public float speed = 10f;
	public Vector2 maxVelocity = new Vector2(3,3);
	private DarthVaderController controller;
	private Animator animator;

	void Start(){
		controller = GetComponent<DarthVaderController> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		var forceX = 0f;
		var forceY = 0f;
		var absVelX = Mathf.Abs(rigidbody2D.velocity.x);
		var absVelY = Mathf.Abs (rigidbody2D.velocity.y);

		if (controller.moving.x != 0) {
			if (absVelX < maxVelocity.x) {
				forceX = speed * controller.moving.x;
			}
		} 
		else if (controller.moving.y != 0) {
			if (absVelY < maxVelocity.y) {
				forceY = speed * controller.moving.y;
				if(controller.moving.y == 1){
					animator.SetInteger("AnimState", 2);
				}
				else if(controller.moving.y == -1){
					animator.SetInteger("AnimState", 1);
				}
			}
		}
		else{
			animator.SetInteger("AnimState",0);
		}

		rigidbody2D.AddForce(new Vector2(forceX, forceY));
	
	}
}

/* Original Hardcoded Player Movement
 * 
 * if(Input.GetKey("right")){
			if(absVelX < maxVelocity.x){
				forceX = speed;
			}
			transform.localScale = new Vector3(1,1,1);
		}
		else if(Input.GetKey("left")){
			if(absVelX < maxVelocity.x){
				forceX = -speed;
			}
			transform.localScale = new Vector3(-1,1,1);
		}

		if (Input.GetKey ("up")) {
			if (absVelY < maxVelocity.y) {
				forceY = speed;
			}
		} 
		else if (Input.GetKey ("down")) {
			if (absVelY < maxVelocity.y) {
				forceY = -speed;
			}
		}*/

