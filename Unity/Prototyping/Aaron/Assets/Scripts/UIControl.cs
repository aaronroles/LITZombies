using UnityEngine;
using System.Collections;

public class UIControl : MonoBehaviour {

	public void PlayScene(string sceneName){
		Application.LoadLevel (sceneName);
	}
}
