using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	private bool paused;
	private bool help;
	public GameObject pauseMenuCanvas;
	public GameObject theHud;

	void Start(){
		paused = help = false;
	}
	
	// Update is called once per frame
	void Update () {
		// If the game is paused
		if (paused) {
			// Activate the pause menu
			pauseMenuCanvas.SetActive(true);
			// Deactivate the HUD
			theHud.SetActive(false);
			// Stop time
			Time.timeScale = 0;
		} 
		// If the game is not paused
		else if(!paused){
			// Deactivate the pause menu
			pauseMenuCanvas.SetActive(false);
			// Activate the HUD
			theHud.SetActive(true);
			// Continue time at normal speed
			Time.timeScale = 1;
		}

		// If you press Escape
		if (Input.GetKeyDown (KeyCode.Escape)) {
			// Toggle pause
			paused = !paused;
			print ("press");
		}
	}

	// Clicking the Resume button will call
	// this function, it continues the game
	public void Resume(){
		// Toggle pause
		paused = !paused;
		// Deactivate the pause menu
		pauseMenuCanvas.SetActive(false);
		// Activate the HUD
		theHud.SetActive(true);
		// Continue time at normal speed
		Time.timeScale = 1;
	}

	// Clicking the help button will call
	// this function, it opens help info
	public void GameHelp(){
		// Toggle help
		help = !help;
		// If help is true
		if (help) {
			// Show the instructions
		} 
		// If help is false
		else if (!help) {
			// Hide the instructions
		}
	}

	public void Quit(){
		print ("quitted");
		//Application.Load the main menu; // Should quit to main menu actually
	}
}