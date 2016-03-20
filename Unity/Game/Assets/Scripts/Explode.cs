using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {

	public BodyPart bodyPart;
	public int totalParts = 3;

	void OnTriggerEnter2D(Collider2D t){
		if (t.gameObject.tag == "Deadly") { 
			OnExplode();
			print ("test");
		}
	}

	public void OnExplode(){
		Destroy (gameObject);

		var t = transform;

		for (int i = 0; i < totalParts; i++) {
			t.TransformPoint(0, -100, 0);
			//quaternion changes the rotation of the bodypart
			BodyPart clone = Instantiate(bodyPart, t.position, Quaternion.identity) as BodyPart;
			clone.rigidbody2D.AddForce(Vector3.right * Random.Range(-50, 50));
		}
	}

}
