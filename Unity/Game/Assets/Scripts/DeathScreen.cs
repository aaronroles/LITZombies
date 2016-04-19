using UnityEngine;
using System.Collections;

public class DeathScreen : MonoBehaviour {

	public void CharChoice(){
		Application.LoadLevel ("CharacterChoice");
	}

	public void StartMenu(){
		Application.LoadLevel ("StartMenu");
	}

	public void QuitGame(){
		PlayerPrefs.Save();
		Application.Quit ();
	}
}
