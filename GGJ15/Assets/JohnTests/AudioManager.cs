using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioListener))]
public class AudioManager : MonoBehaviour {

	MusicManager musicManager;
	SFXManager sfxManager;

	[Range(0,1)]
	public float musicVolume = 0.7f;
	[Range(0,1)]
	public float sfxVolume = 0.7f;

	private bool isDucking;
	public float duckAmount = 0.9f;

	private float Duck{
		get{
			return 1-duckAmount;
		}
	}

	// Use this for initialization
	private bool isInitialized = false;
	void Start () {
		musicManager = gameObject.GetComponentInChildren<MusicManager>();
		sfxManager = gameObject.GetComponentInChildren<SFXManager>();
		isInitialized = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			sfxManager.audio.Play();
		}
		if(sfxManager.audio.isPlaying){
			isDucking = true;
		}else{
			isDucking = false;
		}
		signalMod = Mathf.Lerp(1,Duck,duckLerp);
		Debug.Log(signalMod);
	}

	float signalMod = 1;
	float duckLerp = 0;
	float[] musicData;
	float[] sfxData;
	void OnAudioFilterRead(float[] data, int channels){

		if(!isInitialized)return;

		musicData = musicManager.audioFilterData;
		sfxData = sfxManager.audioFilterData;

		for(var i = 0; i < data.Length; i+=channels){
			if(isDucking){
				duckLerp = Mathf.Clamp01(duckLerp+0.01f);
			}else{
				duckLerp = Mathf.Clamp01(duckLerp-0.1f);
			}
			signalMod = Mathf.Lerp(1,Duck,duckLerp);
			for(var c = 0; c < channels; c++){
				data[i+c] = 0;
				if(musicData.Length>0){
					data[i+c] += musicData[i+c] * musicVolume * signalMod;
				}
				if(sfxData.Length>0){
					data[i+c] += sfxData[i+c] * sfxVolume;
				}
			}
		}
	
	}
}
