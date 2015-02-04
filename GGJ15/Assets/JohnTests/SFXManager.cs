using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SFXManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LoadAudioFromResources();
	}

	System.Random rand = new System.Random();
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			audio.clip = effectFiles[rand.Next(0,effectFiles.Length)];
			audio.Play();
		}
	
	}

	AudioClip[] effectFiles;

	public string[] effectNames;

	bool audioReady = false;

	void LoadAudioFromResources(){
		AudioClip[] loadSamples = Resources.LoadAll<AudioClip>("Audio/SFX");
		effectFiles = new AudioClip[loadSamples.Length];
		effectNames = new string[loadSamples.Length];
		if(loadSamples==null||loadSamples.Length==0)return;
		for(var i = 0; i < loadSamples.Length; i++){
			effectFiles[i] = loadSamples[i];
			effectNames[i] = loadSamples[i].name;
			Debug.Log(i);
		}
		Debug.Log("Finished");
		audioReady = true;
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
