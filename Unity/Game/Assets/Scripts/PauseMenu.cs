using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	private bool paused;
	public GameObject pauseMenuCanvas;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (paused) {
			pauseMenuCanvas.SetActive(true);
			Time.timeScale = 0;
		} 
		else if(!paused){
			pauseMenuCanvas.SetActive(false);
			Time.timeScale = 1;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			paused = !paused;
		}
	}
}
