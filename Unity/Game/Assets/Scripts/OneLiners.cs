using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class OneLiners : MonoBehaviour {

	AudioSource oneLinersSrc;
	public AudioClip[] lines;
	int random;
	float randomTime;
	float timer;

	// Use this for initialization
	void Awake () {
		oneLinersSrc = GetComponent<AudioSource> ();
		randomTime = Random.Range(30, 60);
	}
	
	// Update is called once per frame
	void Update () {
		if(timer >= randomTime){
			// Reset timer
			timer = 0;
			// Random time
			randomTime = Random.Range(30, 60);
			// Choose a random number based on the 
			// length of the audio clip array
			random = Random.Range(0, lines.Length);

			OneLiner ();
		}
		timer+= Time.deltaTime;
	}

	void OneLiner(){
		oneLinersSrc.PlayOneShot (lines [random]);
	}
}
