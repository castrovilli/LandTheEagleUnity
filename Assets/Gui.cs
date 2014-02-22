﻿using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour {

	public GUISkin defaultSkin;
	public GUISkin stateSkin;

	private bool started = false;
	private bool startingToShowButtons = false;
	private bool showButtons = false;
	
	void Start() {
	}
	
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			started = true;
		}
	}
	
	private IEnumerator ShowButtons() {
		yield return new WaitForSeconds(1.5f);
		showButtons = true;
	}

	private void DrawStatus() {
		GUI.skin = stateSkin;

		GameState state = GameState.Instance;

		float x = Screen.width * 0.1f;
		float y = Screen.height * 0.05f;
		float width = Screen.width - 2.0f * x;
		float height = Screen.height * 0.05f;
		
		GUI.Label(new Rect(x, y, width, height), "Level: " + (state.Level + 1));
		if (state.Speed >= 3) {
			GUI.Label(new Rect(x, y + height, width, height),
			          "<color=#ff0000>Speed: " + state.Speed + "</color>");
		} else {
			GUI.Label(new Rect(x, y + height, width, height), "Speed: " + state.Speed);
		}
		GUI.Label(new Rect(x, y + 2 * height, width, height), "Fuel: " + state.Fuel);
	}

	void OnGUI() {
		DrawStatus();

		GUI.skin = defaultSkin;

		if (!started) {
			GUI.Label(new Rect(0, 0, Screen.width, Screen.height / 2), "Instructions");
		}

		Rect button1Rect = GameState.GetGUIButtonRect(0);
		
		GameState state = GameState.Instance;
		if (!state.Grounded) {
			return;
		}
		
		if (!startingToShowButtons) {
			startingToShowButtons = true;
			StartCoroutine(ShowButtons());
		}
		
		if (state.Crashed) {
			GUI.Label(new Rect(0, 0, Screen.width, Screen.height / 2), "You crashed!");
			if (showButtons) {
				if (GUI.Button(button1Rect, "Retry")) {
					state.RestartLevel();
				}
			}
		} else {
			GUI.Label(new Rect(0, 0, Screen.width, Screen.height / 2), "You landed!");
			if (showButtons) {
				if (GUI.Button(button1Rect, "Next Level")) {
					state.LoadNextLevel();
				}
			}
		}
	}
}