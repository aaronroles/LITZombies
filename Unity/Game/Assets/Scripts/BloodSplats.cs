using UnityEngine;
using System.Collections;

public class BloodSplats : MonoBehaviour {

	public Blood blood;
	public int totalBlood = 1;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			Blood();
		}
	}
	
	void Blood(){
		var t = transform;
		for (int i = 0; i < totalBlood; i++) {
			t.TransformPoint(0,-100,0);
			Blood clone = Instantiate(blood, t.position, Quaternion.identity) as Blood;
			clone.rigidbody2D.AddForce(Vector3.right * Random.Range (-50, 50));
		}
	}
}
