using UnityEngine;
using System.Collections;

public class Main : G1MonoBehaviour {
	
	// Use this for initialization
	new void Start () {
		base.Start ();
//		Debug.Log ("planetM:"+planetM);
		int num = 40;
		float offset = 360 / num;
		float angle = 0;
		for (int i =0; i<num; i++) {
			GameObject carGO = resourceM.Create ("Car");
			planetM.defaultPlanet.Set (carGO, angle, .25f);
			camera.target = carGO;
			angle += offset;

//			carGO.transform.GetChild(0).gameObject.rigidbody2D.velocity = new Vector2(0,0);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
