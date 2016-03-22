using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour {

	public static int health;
	Text text;

	void Awake(){
		health = 50;
		text = GetComponent<Text> ();
	}

	void Update () {
		if(health <= 0){
			//Destroy(gameObject); Until the death body parts are added
			print ("You are dead");
		}
		text.text = "Health: " + health;
	}
	
	public void Damage(int dmg){
		health -= dmg;
	}
}
