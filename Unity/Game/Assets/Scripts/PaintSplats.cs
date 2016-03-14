using UnityEngine;
using System.Collections;

public class PaintSplats : MonoBehaviour {

	public Paint splats;
	public int totalSplats = 5;

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Enemy") {
			Splat();
		}
	}

	void Splat(){
		var t = transform;
		for (int i = 0; i < totalSplats; i++) {
			t.TransformPoint(0,-100,0);
			Paint clone = Instantiate(splats, t.position, Quaternion.identity) as Paint;
			clone.rigidbody2D.AddForce(Vector3.right * Random.Range (-50, 50));
		}
	}
}
