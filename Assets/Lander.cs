﻿using UnityEngine;
using System.Collections;

public class Lander : MonoBehaviour {

	public AudioClip thrustSound;

	void Start() {
	}
	
	void Update() {
		GameState state = GameState.Instance;
		if (state.Grounded) {
			return;
		}
		if (Input.GetMouseButtonDown(0)) {
			if (rigidbody2D.gravityScale != 0.0f) {
				if (state.Fuel > 0) {
					audio.PlayOneShot(thrustSound);
					rigidbody2D.AddForce(new Vector2(0, 15));
					state.Fuel--;
				}
			} else {
				rigidbody2D.gravityScale = 0.1f;
			}
		}
		state.Speed = (int)Mathf.Round(rigidbody2D.velocity.y / -0.5f);
	}

	void KnockOver() {
		rigidbody2D.AddTorque(-2);
	}
}
