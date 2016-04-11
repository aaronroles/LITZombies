using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour {

	public static int health;
	public Slider healthSlider;
	Text healthText;

	void Awake(){
		health = 50;
		healthText = GetComponent<Text> ();
	}

	void Update () {
		if(health <= 0){
			// If the object has the Explode script
			if(gameObject.GetComponent<Explode>()){
				// Call it's OnExplode function
				gameObject.GetComponent<Explode>().OnExplode();
			}
			// Go to death scene
			SetDead ();
		}

		// If the object has a text component
		if (gameObject.GetComponent<Text> () && health > 0) {
			// Update it
			healthText.text = "Health: " + health;
		}

		if (gameObject.GetComponent<Slider> ()) {
			healthSlider.value = health;
		}
	}

	// Public function so it can be accessed from
	// elsewhere. A number gets passed in and is 
	// the damage applied to player health
	public void Damage(int dmg){
		health -= dmg;
	}

	// Called when the player's health is zero (Dead)
	// and changes the health text to reflect that.
	void SetDead(){
		health = 0;
		healthSlider.value = 0;
		if (gameObject.GetComponent<Text> ()) {
			healthText.text = "Dead";
		}
		// PlayerPrefs.Save ();
		// Application.LoadLevel("DeadScreen");
	}
}
