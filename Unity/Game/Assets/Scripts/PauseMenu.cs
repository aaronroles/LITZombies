using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	private bool paused;
	public GameObject pauseMenuCanvas;
	public GameObject theHud;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (paused) {
			pauseMenuCanvas.SetActive(true);
			theHud.SetActive(false);
			Time.timeScale = 0;
		} 
		else if(!paused){
			pauseMenuCanvas.SetActive(false);
			theHud.SetActive(true);
			Time.timeScale = 1;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			paused = !paused;
		}
	}

	public void Resume(){
		paused = !paused;
		pauseMenuCanvas.SetActive(false);
		theHud.SetActive(true);
		Time.timeScale = 1;
	}

	void Settings(){
		// Pop up settings
	}

	void GameHelp(){
		// Pop up game help
	}

	void Quit(){
		// Bring to main menu
	}
}
