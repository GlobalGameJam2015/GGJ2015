﻿using UnityEngine;
using System.Collections;

public class Jock : MonoBehaviour {

	private GameObject Target;
	private GameManager Manager;

	private Ray ray;
	private RaycastHit hit;

	void Start(){
		Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void Update(){
		Ray ray;

	}

	void Played () {
		if(!Target){

		}
	}
}
