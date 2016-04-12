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
	private GameObject thePlayer;

	void Start(){
		animator = GetComponent<Animator> ();
		thePlayer = GameObject.FindWithTag ("Player");
		playerX = thePlayer.transform.position.x;
		playerY = thePlayer.transform.position.y;

		 if (thePlayer) {
			// Find it's position and track it
			playerX = thePlayer.transform.position.x;
			playerY = thePlayer.transform.position.y;
			playerDirection = new Vector2 (playerX, playerY);
			// Send the zombie to the player's direction
			Zombie.SetDestination (playerDirection);
			
			Debug.Log ("Player is: " + playerDirection + ". Zombie is: " + currentPosition);
		}

	}

	public KDNav2DAgent Zombie
	{
		get { return m_agent ?? (m_agent = GetComponent<KDNav2DAgent>()); }
	}
	
	void Update () {
		// If the game is paused
		if (Time.timeScale == 0) {
			// Game is paused, do nothing
		}
		// If the game is not paused
		else if(Time.timeScale == 1){
			//currentPosition = gameObject.transform.position;
			// If the player object exists
			if (thePlayer) {
				// Find it's position and track it
				float latestPlayerX, latestPlayerY;
				latestPlayerX = thePlayer.transform.position.x;
				latestPlayerY = thePlayer.transform.position.y;

				if ((playerX != latestPlayerX) || (playerY != latestPlayerY)) {
					playerX = latestPlayerX;
					playerY = latestPlayerY;
					playerDirection = new Vector2 (playerX, playerY);
					// Send the zombie to the player's direction
					Zombie.SetDestination(playerDirection);
					//Debug.Log ("Player is: " + playerDirection + ". Zombie is: " + currentPosition);
				}

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
				else if(currentPosition.x < lastPosition.x){
					animator.SetBool("ZombieStill", false);
					animator.SetBool("ZombieSideWalk", true);
					animator.SetBool("ZombieFrontWalk", false);
					animator.SetBool("ZombieBackWalk", false);
					transform.localScale = new Vector3(-1,1,1); // Flips zombie to the left
				}
				
				// If current y position is greater than last y position
				// Going down
				if(currentPosition.y < lastPosition.y){
					animator.SetBool("ZombieStill", false);
					animator.SetBool("ZombieSideWalk", false);
					animator.SetBool("ZombieFrontWalk", true);
					animator.SetBool("ZombieBackWalk", false);
				}
				
				// If current y position is less than last y position
				// Going up
				else if(currentPosition.y > lastPosition.y){
					animator.SetBool("ZombieStill", false);
					animator.SetBool("ZombieSideWalk", false);
					animator.SetBool("ZombieFrontWalk", false);
					animator.SetBool("ZombieBackWalk", true);
				}

				// If the current position is the same as last position
				// Stand still
				if(currentPosition == lastPosition){
					animator.SetBool("ZombieStill", true);
					animator.SetBool("ZombieSideWalk", false);
					animator.SetBool("ZombieFrontWalk", false);
					animator.SetBool("ZombieBackWalk", false);
				}
			}
			// Update last position
			lastPosition = currentPosition;
		} 
		// Otherwise, no player position
		else{	
			noPlayer.x = noPlayer.y = 0;
			//Zombie.SetDestination(noPlayer);
		}
	}
}
