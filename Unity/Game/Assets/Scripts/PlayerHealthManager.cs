using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour {

	public static int health;
	Text healthText;

	void Awake(){
		health = 50;
		healthText = GetComponent<Text> ();
	}

	void Update () {
		if(health <= 0){
			// If the object has the Explode script
			if(gameObject.GetComponent<Explode>()){
				gameObject.GetComponent<Explode>().OnExplode();
			}
			// Go to death scene
			SetScore ();
		}

		// If the object has a text component
		if (gameObject.GetComponent<Text> () && health > 0) {
			healthText.text = "Health: " + health;
		}
	}
	
	public void Damage(int dmg){
		health -= dmg;
	}

	void SetScore(){
		health = 0;
		if (gameObject.GetComponent<Text> ()) {
			healthText.text = "Dead";
		}
	}
}
