using UnityEngine;
using System.Collections;

public class Lightsaber_Enemy : MonoBehaviour {

	public int dmgToGive;
	private int hitCount;

	// Use this for initialization
	void Start () {
		hitCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			print("Obi Wan hit: " + ++hitCount);
			other.GetComponent<PlayerHealthManager>().damage(dmgToGive);
			//Destroy (other.gameObject);
		}
	}
}
