using UnityEngine;
using System.Collections;

public class Lightsaber_Player : MonoBehaviour {

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
		if (other.gameObject.tag == "Enemy") {
			print("Vader hit: " + ++hitCount);
			other.GetComponent<EnemyHealthManager>().damage(dmgToGive);
			//Destroy (other.gameObject);
		}
	}
}
