using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {

	private int _personCount = 1;
	private int _personMod = 0;
	public int PersonCount {
		get{
			return _personCount + _personMod;
		}
	}
	public void SetPersonCount(int count){
		_personCount = count;
	}
	public void SetPersonMod (int mod){
		_personMod = mod;
	}

	private int _resourceNeed = 1;
	private int _resourceMod = 0;
	public int ResourceNeed{
		get{
			return _resourceNeed + _resourceMod;
		}
	}
	public void SetResourceNeed(int count){
		_personCount = count;
	}
	public void SetResourceMod (int mod){
		_personMod = mod;
	}

	private int _entertainment = 0;
	private int _entertainmentMod = 0;
	public int Entertainment{
		get{
			return _entertainment + entertainmentMod;
		}
	}
	public void SetEntertainment(int count){
		_personCount = count;
	}
	public void SetEntertainmentMod (int mod){
		_personMod = mod;
	}
}
