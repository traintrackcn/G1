using UnityEngine;
using System.Collections;

public class PlanetSun : MonoBehaviour {


	// early morning, morning , afternoon , night
	public Color[] colors = new Color[4];

	public Color color;
	public int targetIndex;

	void Awake(){

		colors[0] = new Color (139.0f/255.0f,175.0f/255.0f,225.0f/255.0f);
		colors[1] = new Color (202.0f/255.0f, 225.0f/255.0f, 255.0f/255.0f);
		colors[2] = new Color (201.0f/255.0f,98.0f/255.0f,43.0f/255.0f);
		colors[3] = new Color (7.0f/255.0f,28.0f/255.0f,50.0f/255.0f);

		color = colors [0];
	}

	void FixedUpdate(){
	
		Color targetColor = colors [targetIndex];
//		Color colorRounded = new Color(Mathf.Round(color.r),Mathf.Round(color.g), Mathf.Round(color.b),color.a);
		if (color == targetColor) {
			Next ();
		} else {
			color = Color.Lerp(color, targetColor, Time.deltaTime);
//			color = Color.
		}

//		Debug.Log ("color -> " + color+" targetColor:"+targetColor+" targetIndex:"+targetIndex);
//		Debug.Log ("targetIndex -> "+targetIndex);

	}

	public void Next(){
		if (targetIndex+1 == colors.Length){
			targetIndex = 0;
		}else{
			targetIndex ++;
		}
	
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
