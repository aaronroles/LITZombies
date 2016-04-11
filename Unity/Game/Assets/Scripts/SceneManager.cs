using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public void MainMenu(){
		Application.LoadLevel ("MainMenu");
	}

	public void CharacterChoice(){
		Application.LoadLevel ("CharacterChoice");
	}

	public void HowToPlay(){
		Application.LoadLevel ("HowToPlay");
	}

	public void Richie(){
		Application.LoadLevel ("RichieLevel");
	}

	public void Mike(){
		Application.LoadLevel ("MikeLevel");
	}

	public void Bernie(){
		Application.LoadLevel ("BernieLevel");
	}

	public void QuitGame(){
		PlayerPrefs.Save ();
		Application.Quit ();
	}
}
