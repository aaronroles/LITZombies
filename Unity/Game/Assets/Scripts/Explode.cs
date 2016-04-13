using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {

	public BodyPart bodyPart;
	static int totalDeaths;
	int totalParts = 3;

	void Awake(){
		if(PlayerPrefs.HasKey("TotalDeaths")){
			totalDeaths = PlayerPrefs.GetInt("TotalDeaths");
		}
		else{
			// Otherwise, no high score
			PlayerPrefs.SetInt("TotalDeaths", 0);
		}
	}

	public void OnExplode(){
		Destroy (gameObject);

		if (gameObject.tag == "Player") {
			totalDeaths++;
			PlayerPrefs.SetInt("TotalDeaths", totalDeaths);
		}
		var t = transform;

		for (int i = 0; i < totalParts; i++) {
			t.TransformPoint(0, -100, 0);
			//quaternion changes the rotation of the bodypart
			BodyPart clone = Instantiate(bodyPart, t.position, Quaternion.identity) as BodyPart;
			clone.rigidbody2D.AddForce(Vector3.right * Random.Range(-50, 50));
		}
	}

}
