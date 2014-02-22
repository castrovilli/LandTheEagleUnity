﻿using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	private static GameState instance;

	public static GameState Instance {
		get {
			if (instance == null) {
				instance = new GameObject("GameState").AddComponent<GameState>();
				DontDestroyOnLoad(instance);
			}
			return instance;
		}
	}

	public static Rect GetGUIButtonRect(int position) {
		float top = (Screen.height / 2.0f) + position * (Screen.height / 6.0f);
		float left = Screen.width * 0.2f;
		float width = Screen.width - left * 2.0f;
		float height = Screen.height / 7.0f;
		return new Rect(left, top, width, height);
	}

	public int Level { get; private set; }
	public int Speed;
	public bool Crashed;
	public bool Landed;
	public int Fuel;

	public float GroundSpeed {
		get {
			return -5.0f;
		}
	}

	public bool Grounded {
		get {
			return Crashed || Landed;
		}
	}

	void Start() {
		Level = 0;
		Fuel = 100000;
	}

	void Update() {
	}

	public void RestartLevel() {
		Application.LoadLevel("LandingScene");
		Speed = 0;
		Crashed = false;
		Landed = false;
		Fuel = 100;
	}
	
	public void LoadNextLevel() {
		Level++;
		RestartLevel();
	}
}
