using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SFXManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[HideInInspector]
	public float[] audioFilterData;
	void OnAudioFilterRead(float[] data, int channels){
		if(audioFilterData.Length == null){
			audioFilterData = new float[data.Length];
		}
		audioFilterData = (float[])data.Clone();
		Array.Clear(data,0,data.Length);
	}
}
