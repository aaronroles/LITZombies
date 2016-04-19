using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public void CharChoice(){
		Application.LoadLevel ("CharacterChoice");
	}

	public void MyStats(){
		Application.LoadLevel ("Stats");
	}

	public void Instructions(){
		Application.LoadLevel ("Instructions");
	}

	public void QuitGame(){
		PlayerPrefs.Save();
		Application.Quit ();
	}
}
