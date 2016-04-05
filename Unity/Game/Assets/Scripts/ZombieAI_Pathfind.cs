using UnityEngine;
using System.Collections;

[RequireComponent(typeof(KDNav2DAgent))]
public class ZombieAI_Pathfind : MonoBehaviour {

	private KDNav2DAgent m_agent;
	private Vector3 player;
	private Vector2 playerDirection;
	private float playerX;
	private float playerY;
	private float speed;
	private float distance;

	void Start () {
		//speed = (Random.Range (3, 7));
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
			// If the player object exists
			if (GameObject.FindWithTag ("Player")) {
				// Find it's position and track it
				playerX = GameObject.FindWithTag ("Player").transform.position.x;
				playerY = GameObject.FindWithTag ("Player").transform.position.y;
				playerDirection = new Vector2 (playerX, playerY);
				//rigidbody2D.AddForce (playerDirection.normalized * speed);
				Zombie.SetDestination(playerDirection);
				//print (m_agent + "" + playerDirection);
			}
		} 
		// Otherwise, no player position
		else{	
			player.x = player.y = player.z = 0;
		}
	}
}
