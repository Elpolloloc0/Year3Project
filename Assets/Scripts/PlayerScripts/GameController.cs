﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Text timerLabelText;
	public Text waveNumberText;
	public Text enemyCountText;
	public Text totalScoreText;
	public Text modifierText;
	public Canvas pauseCanvas;
	public bool playerNoDamageStreak = true;
	public int modifierCount;

	private float gameTime;
	private WaveController waveController;
	private int totalScore;
	private PlayerController player;

	// Use this for initialization
	void Start(){
		pauseCanvas.enabled = false;
		modifierCount = 1;
	}
		
	void Awake () {
		waveController = GameObject.FindObjectOfType<WaveController> ();
		player =  GameObject.Find ("Player").GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {

		gameTimer ();
		waveNumberText.text = waveController.getWaveNumber ().ToString ();
		enemyCountText.text = waveController.getEnemyCount ().ToString ();
		totalScoreText.text = totalScore.ToString ();
		modifierText.text = modifierCount.ToString () + "x";
		togglePause ();

		if (player.getPlayerHealth () == 0) {
			PlayerStats.setTimerText (timerLabelText.text);
			PlayerStats.setScoreText (totalScore.ToString ());
			PlayerStats.setShotsFired (player.shotsFired);
			PlayerStats.setShotsHitTarget (player.shotsHitTarget);
			SceneManager.LoadScene ("GameOverScene");
		}

	}

	public void setScoreText(int enemyScore){
		totalScore = totalScore + (enemyScore * modifierCount);
	}

	//Function used for maintaining the game's timer.
	public void gameTimer(){
		gameTime += Time.deltaTime;
		int hours = (int)(gameTime / 3600) % 24;
		int minutes = (int)(gameTime / 60) % 60;
		int seconds = (int)(gameTime % 60);
		timerLabelText.text = string.Format ("{0:0} : {1:00} : {2:00}", hours, minutes, seconds);
	}

	//Toggles the pause screen on and off.
	public void togglePause(){
	
		if (Input.GetKeyDown ("escape")) {
			if (pauseCanvas.enabled) {
				pauseCanvas.enabled = false;
				Time.timeScale = 1.0f;
			} else {
				pauseCanvas.enabled = true;
				Time.timeScale = 0f;
			}	
		}
	}

	//Returns a boolean for other objects to check if the game is paused.
	public bool checkPause(){
		if (pauseCanvas.enabled) {
			return true;
		} else {
			return false;
		}
	}
		
}
