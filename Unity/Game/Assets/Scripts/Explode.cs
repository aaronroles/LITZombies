using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {

	public BodyPart bodyPart;
	public int totalParts = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D t){
		if (t.gameObject.tag == "Deadly") { 
			OnExplode();
			print ("test");
		}
	}

	void OnExplode(){
		Destroy (gameObject);

		var t = transform;

		for (int i = 0; i < totalParts; i++) {
			t.TransformPoint(0, -100, 0);
			Zombie_BodyPart clone = Instantiate(bodyPart, t.position, Quaternion.identity) as BodyPart;
		}
	}

}
