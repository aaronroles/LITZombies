using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ZombieSound : MonoBehaviour {

	AudioSource audio;
	public AudioClip[] grunts;
	int randomGrunt;
	float randomTime;
	float timer;

	// Use this for initialization
	void Awake () {
		audio = GetComponent<AudioSource> ();
		// Set a random time delay for between grunts
		randomTime = Random.Range(1, 10);
	}
	
	// Update is called once per frame
	void Update () {
		// If the timer is equal or greater to the random time set
		if (timer >= randomTime) {
			// Reset timer
			timer = 0;
			// Reset a random amount of time
			randomTime = Random.Range(10, 15);
			// Choose a random number based on the 
			// length of the audio clip array
			randomGrunt = Random.Range(0, grunts.Length);
			// Call the Grunt function
			Grunt ();
		}
		// Update timer in real time
		timer += Time.deltaTime;
	}

	void Grunt(){
		// Play the grunt clip at the postion
		// which was randomly selected.
		audio.PlayOneShot (grunts [randomGrunt]);
	}
}
