using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D target){
		if(Input.GetKey(KeyCode.Space))
			if (target.gameObject.tag == "Player"){
				print ("Hit the switch");
				Destroy(gameObject);
		}
	}
}