using UnityEngine;
using System.Collections;

[RequireComponent(typeof(KDNav2DAgent))]
public class ZombieAI_Pathfind : MonoBehaviour {

	private KDNav2DAgent m_agent;
	private Vector2 noPlayer;
	private Vector2 playerDirection;
	private float playerX;
	private float playerY;
	private Animator animator;
	private Vector2 lastPosition;
	private Vector2 currentPosition;

	void Start(){
		animator = GetComponent<Animator> ();
	}

	public KDNav2DAgent Zombie
	{
		get { return m_agent ?? (m_agent = GetComponent<KDNav2DAgent>()); }
	}
	
	protected void Update () {
		// If the game is paused
		if (Time.timeScale == 0) {
			// Game is paused, do nothing
		}
		// If the game is not paused
		else if(Time.timeScale == 1){
			currentPosition = gameObject.transform.position;
			// If the player object exists
			if (GameObject.FindWithTag ("Player")) {
				// Find it's position and track it
				playerX = GameObject.FindWithTag ("Player").transform.position.x;
				playerY = GameObject.FindWithTag ("Player").transform.position.y;
				playerDirection = new Vector2 (playerX, playerY);

				// If current x position is greater than last x position
				// Going to the right
				if(currentPosition.x > lastPosition.x){
					animator.SetBool("ZombieStill", false);
					animator.SetBool("ZombieSideWalk", true);
					animator.SetBool("ZombieFrontWalk", false);
					animator.SetBool("ZombieBackWalk", false);
					transform.localScale = new Vector3(1,1,1); // Flips zombie to the right
				}
				
				// If current x position is less than last x position
				// Going to the left
				if(currentPosition.x < lastPosition.x){
					animator.SetBool("ZombieStill", false);
					animator.SetBool("ZombieSideWalk", true);
					animator.SetBool("ZombieFrontWalk", false);
					animator.SetBool("ZombieBackWalk", false);
					transform.localScale = new Vector3(-1,1,1); // Flips zombie to the left
				}
				
				// If current y position is greater than last y position
				// Going to the right
				if(currentPosition.y < lastPosition.y){
					animator.SetBool("ZombieStill", false);
					animator.SetBool("ZombieSideWalk", false);
					animator.SetBool("ZombieFrontWalk", true);
					animator.SetBool("ZombieBackWalk", false);
				}
				
				// If current y position is less than last y position
				// Going to the right
				if(currentPosition.y > lastPosition.y){
					animator.SetBool("ZombieStill", false);
					animator.SetBool("ZombieSideWalk", false);
					animator.SetBool("ZombieFrontWalk", false);
					animator.SetBool("ZombieBackWalk", true);
				}
				
				if(currentPosition == lastPosition){
					animator.SetBool("ZombieStill", true);
					animator.SetBool("ZombieSideWalk", false);
					animator.SetBool("ZombieFrontWalk", false);
					animator.SetBool("ZombieBackWalk", false);
				}
			}
			Zombie.SetDestination(playerDirection);
			lastPosition = currentPosition;
		} 
		// Otherwise, no player position
		else{	
			noPlayer.x = noPlayer.y = 0;
			Zombie.SetDestination(noPlayer);
		}
	}
}
