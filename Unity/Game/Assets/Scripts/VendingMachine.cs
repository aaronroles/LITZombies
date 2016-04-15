using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class VendingMachine : MonoBehaviour {

	private bool popUp;
	int addToHealth;
	public GameObject vendingMachineUI;
	AudioSource audio;
	public AudioClip[] machine;
	static int totalSpent;

	void Start(){
		popUp = false;
		audio = GetComponent<AudioSource> ();
	}

	void Awake(){
		if(PlayerPrefs.HasKey("TotalSpent")){
			totalSpent = PlayerPrefs.GetInt("TotalSpent");
		}
		else{
			// Otherwise, no high score
			PlayerPrefs.SetInt("TotalSpent", 0);
		}
	}

	void Update(){
		if (popUp) {
			vendingMachineUI.SetActive(true);
		} 
		else if (!popUp) {
			vendingMachineUI.SetActive(false);
		}
	}

	// When there is a collision
	void OnCollisionStay2D(Collision2D other){
		// If 'E' is pressed down
		if(Input.GetKeyDown(KeyCode.E)){
			// If the colliding object is the Player
			if(other.gameObject.tag == "Player"){
				// Toggle pop up
				popUp = !popUp;
			}
		}
	}

	// When the collision finishes
	void OnCollisionExit2D(Collision2D other){
		// If it's the player
		if(other.gameObject.tag == "Player"){
			// Deactivate the pop up - this is so the player
			// can't walk away from the machine and still have
			// the pop up active.
			popUp = false;
			vendingMachineUI.SetActive(false);
		}
	}

	public void ButtonA(){
		// If you have 120 coins and your health is less than 50
		if (ScoreManager.score >= 120 && PlayerHealthManager.health < 50) {
			// Add 10 to health+
			PlayerHealthManager.health += 10;
			//  Remove 120 from score
			ScoreManager.score -= 120;
			// Add to total spend
			totalSpent+= 120;
			// If the health goes over to 50
			if(PlayerHealthManager.health > 50){
				// set it back to 50
				PlayerHealthManager.health = 50;
			}
			// Close the pop up window
			popUp = false;
			// Play the audio clip
			audio.PlayOneShot (machine [Random.Range(0, machine.Length)]);
			// Update total spent key
			PlayerPrefs.SetInt("TotalSpent", totalSpent);
		}
	}

	public void ButtonB(){
		// If you have 40 coins and your health is less than 50
		if (ScoreManager.score >= 40 && PlayerHealthManager.health < 50) {
			// Add 10 to health+
			PlayerHealthManager.health += 6;
			//  Remove 120 from score
			ScoreManager.score -= 40;
			// Add to total spend
			totalSpent+= 40;
			// If the health goes over to 50
			if(PlayerHealthManager.health > 50){
				// set it back to 50
				PlayerHealthManager.health = 50;
			}
			// Close the pop up window
			popUp = false;
			// Play the audio clip 
			audio.PlayOneShot (machine [Random.Range(0, machine.Length)]);
			// Update total spent key
			PlayerPrefs.SetInt("TotalSpent", totalSpent);
		}
	}

	public void ButtonC(){
		// If you have 15 coins and your health is less than 50
		if (ScoreManager.score >= 15 && PlayerHealthManager.health < 50) {
			// Add 10 to health+
			PlayerHealthManager.health += 2;
			//  Remove 120 from score
			ScoreManager.score -= 15;
			// Add to total spend
			totalSpent+= 15;
			// If the health goes over to 50
			if(PlayerHealthManager.health > 50){
				// set it back to 50
				PlayerHealthManager.health = 50;
			}
			// Close the pop up window
			popUp = false;
			// Play the audio clip 
			audio.PlayOneShot (machine [Random.Range(0, machine.Length)]);
			// Update total spent key
			PlayerPrefs.SetInt("TotalSpent", totalSpent);
		}
	}

	public void ButtonD(){
		// If you have 80 coins and your health is less than 50
		if (ScoreManager.score >= 80 && PlayerHealthManager.health < 50) {
			// Add 10 to health+
			PlayerHealthManager.health += 8;
			//  Remove 120 from score
			ScoreManager.score -= 80;
			// Add to total spend
			totalSpent+= 80;
			// If the health goes over to 50
			if(PlayerHealthManager.health > 50){
				// set it back to 50
				PlayerHealthManager.health = 50;
			}
			// Close the pop up window
			popUp = false;
			// Play the audio clip 
			audio.PlayOneShot (machine [Random.Range(0, machine.Length)]);
			// Update total spent key
			PlayerPrefs.SetInt("TotalSpent", totalSpent);
		}
	}

	public void ButtonE(){
		// If you have 500 coins and your health is less than 50
		if (ScoreManager.score >= 500 && PlayerHealthManager.health < 50) {
			// Max health of 50
			PlayerHealthManager.health = 50;
			//  Remove 120 from score
			ScoreManager.score -= 500;
			// Add to total spend
			totalSpent+= 500;
			// Close the pop up window
			popUp = false;
			// Play the audio clip 
			audio.PlayOneShot (machine [Random.Range(0, machine.Length)]);
			// Update total spent key
			PlayerPrefs.SetInt("TotalSpent", totalSpent);
		}
	}

	public void ButtonF(){
		// If you have 200 coins and your health is less than 50
		if (ScoreManager.score >= 200 && PlayerHealthManager.health < 50) {
			// Add 10 to health+
			PlayerHealthManager.health += 14;
			//  Remove 120 from score
			ScoreManager.score -= 200;
			// Add to total spend
			totalSpent+= 200;
			// If the health goes over to 50
			if(PlayerHealthManager.health > 50){
				// set it back to 50
				PlayerHealthManager.health = 50;
			}
			// Close the pop up window
			popUp = false;
			// Play the audio clip 
			audio.PlayOneShot (machine [Random.Range(0, machine.Length)]);
			// Update total spent key
			PlayerPrefs.SetInt("TotalSpent", totalSpent);
		}
	}
}
