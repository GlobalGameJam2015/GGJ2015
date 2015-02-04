using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum MusicMode {Title, Start, Mid, End};
public class MusicManager : MonoBehaviour {

	//PUBLIC VARIABLES FOR RUN-TIME ADJUSTMENT
	[Range(0f,1f)]
	public float volume = 0.7f;
	public MusicMode musicMode = MusicMode.Title;
	
	//PLAYBACK MANAGEMENT
	private int currentSample = 0;
	private int currentAudioClip = 0;
	private int nextAudioClip = -1;
	private int sampleIndex = 0;
	private bool audioReady = false;
	private bool finished = false;

	//PLAYBACK DATA
	private List<float[]> sampleData = new List<float[]>();
	private int sampleLength; //Length of currently playing clip
	
	public int intensity = 0;
	public void SetIntensity(int intensity){
		this.intensity = intensity;
	}
	
	public static MusicManager controller;
	
	MusicManager[] musicManagers;
	void Awake () {
		if (controller == null){
			controller = this;
			Application.runInBackground = true;
			AudioSettings.outputSampleRate = 44100;
			LoadAudioFromResources();
			Debug.Log(currentAudioClip);
			sampleLength = sampleData[currentAudioClip].Length;
			DontDestroyOnLoad(gameObject);
		}else if (controller != this){
			Destroy(gameObject);
		}
	}
	
	void LoadAudioFromResources(){
		AudioClip[] samples = Resources.LoadAll<AudioClip>("Audio/Music");
		if(samples==null||samples.Length==0)return;
		for(var i = 0; i < samples.Length; i++){
			sampleLength = samples[i].samples*2;
			sampleData.Add(new float[sampleLength]);

			samples[i].GetData (sampleData[i], 0);
		}
		audioReady = true;
	}
	
	float[] PlayFromFloat (int channels, int currentAudioClip){
		if (sampleIndex >= sampleLength) {
			return new float[2]{0,0};
		}
		float[] retVal = new float[channels];
		for (var c = 0; c<channels; c++) {
			retVal [c] = sampleData[currentAudioClip] [sampleIndex+c];
		}
		sampleIndex += channels;
		if(sampleIndex >= sampleLength || sampleIndex == sampleLength/2 ){
			if(nextAudioClip>=0){
				sampleIndex = 0;
				this.currentAudioClip = nextAudioClip;
				sampleLength = sampleData[this.currentAudioClip].Length;
				nextAudioClip = -1;
			}
		}
		return retVal;
	}

	[HideInInspector]
	public float[] audioFilterData;
	void OnAudioFilterRead (float[] data, int channels){


		if(!audioReady)return;

		for (var i = 0; i < data.Length; i+=channels) {
			float[] audioFloat = PlayFromFloat (channels, currentAudioClip);
			for (var c = 0; c<channels; c++) {
				data [i + c] += audioFloat [c] * volume;
			}
			currentSample = (currentSample + 1) % sampleLength;
		}

		if(audioFilterData.Length == null){
			audioFilterData = new float[data.Length];
		}
		audioFilterData = (float[])data.Clone();
		Array.Clear(data,0,data.Length);

	}

	int[] mainArray = {0,1};
	int[] tempArray;
	void Update(){		
		if(sampleIndex > sampleLength*0.8f && nextAudioClip == -1){
			Debug.Log ("New Sound");
			tempArray = mainArray;
			
			do{
				nextAudioClip = tempArray[UnityEngine.Random.Range(0,tempArray.Length)];
			}while(nextAudioClip == currentAudioClip);
			
		}
	}
	
}
