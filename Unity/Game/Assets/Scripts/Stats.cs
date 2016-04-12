using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stats : MonoBehaviour {

	public Text highScore;
	public Text numKilled;
	public Text dmgTaken;
	public Text totalDeaths;
	public Text totalSpent;
	int currentHighScore;
	int currentNumKilled;
	int currentDmgTaken;
	int currentDeaths;
	int currentSpent;
	
	void Awake(){

		if(PlayerPrefs.HasKey("HighScore")){
			currentHighScore = PlayerPrefs.GetInt("HighScore");
		}
		else{
			PlayerPrefs.SetInt("HighScore", 0);
		}
		
		if(PlayerPrefs.HasKey("NumZombiesKilled")){
			currentNumKilled = PlayerPrefs.GetInt("NumZombiesKilled");
		}
		else{
			PlayerPrefs.SetInt("NumZombiesKilled", 0);
		}
		
		if(PlayerPrefs.HasKey("DamageTaken")){
			currentDmgTaken = PlayerPrefs.GetInt("DamageTaken");
		}
		else{
			PlayerPrefs.SetInt("DamageTaken", 0);
		}
		
		if(PlayerPrefs.HasKey("TotalDeaths")){
			currentDeaths = PlayerPrefs.GetInt("TotalDeaths");
		}
		else{
			PlayerPrefs.SetInt("TotalDeaths", 0);
		}
		
		if(PlayerPrefs.HasKey("TotalSpent")){
			currentSpent = PlayerPrefs.GetInt("TotalSpent");
		}
		else{
			PlayerPrefs.SetInt("TotalSpent", 0);
		}
	}

	// Use this for initialization
	void Start () {
		highScore.text = currentHighScore.ToString();
		numKilled.text = currentNumKilled.ToString();
		dmgTaken.text = currentDmgTaken.ToString();
		totalDeaths.text = currentDeaths.ToString();
		totalSpent.text = currentSpent.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
