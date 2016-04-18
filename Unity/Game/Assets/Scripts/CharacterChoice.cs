using UnityEngine;
using System.Collections;

public class CharacterChoice : MonoBehaviour {
	public void Mike(){
		Application.LoadLevel ("MikeGame");
	}

	public void Bernie(){
		Application.LoadLevel ("BernieGame");
	}

	public void Richie(){
		Application.LoadLevel ("RichieGame");
	}
}
