using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	private bool paused;
	private bool settings;
	private bool help;
	public GameObject pauseMenuCanvas;
	public GameObject theHud;
	public GameObject settingsPanel;
	public GameObject helpPanel;

	void Start(){
		paused = settings = help = false;
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


	// Clicking the settings button will call
	// this function, it opens settings info
	public void Settings(){
		// Toggle settings
		settings = !settings;
		// If settings is true
		if (settings) {
			// Activate the settings panel
			settingsPanel.SetActive (true);
		} 
		// If settings is false
		else if (!settings) {
			// Deactivate the settings panel
			settingsPanel.SetActive (false);
		}
	}

	// Clicking the help button will call
	// this function, it opens help info
	public void GameHelp(){
		// Toggle help
		help = !help;
		// If help is true
		if (help) {
			// Activate help panel
			helpPanel.SetActive (true);
		} 
		// If help is false
		else if (!help) {
			// Deactivate help panel
			helpPanel.SetActive (false);
		}
	}

	public void Quit(){
		print ("quitted");
		Application.Quit (); // Should quit to main menu actually
	}
}