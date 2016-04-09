using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float hashtagSpeed;
	Rigidbody2D rBody;

	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D> ();	

		rBody.AddForce(new Vector2(1,0)*hashtagSpeed, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag != "Player") {
			Destroy (gameObject);
		}
	}
}
