using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardReference : MonoBehaviour {

	public CardTypes Type;
	public string Name;
	public string Description;
	public string Gender;
	public Texture2D Image;
	public int Resource;
	public int People;
	public int Entertainment;

	// Use this for initialization
	void Start () {
		transform.GetChild(0).GetComponent<RawImage>().texture = Image;
		transform.GetChild(1).GetComponent<Text>().text = Name;
		transform.GetChild(2).GetComponent<Text>().text = Description;

		if(People != -1)
			transform.GetChild(3).GetComponent<Text>().text = People.ToString();
		if(Resource != -1)
			transform.GetChild(4).GetComponent<Text>().text = Resource.ToString();
		if(Entertainment != -1)
			transform.GetChild(5).GetComponent<Text>().text = Entertainment.ToString();
	}
}
