using UnityEngine;
using System.Collections;

public class Main : G1MonoBehaviour {
	
	// Use this for initialization
	void Start () {
//		base.Start ();
//		Debug.Log ("planetM:"+planetM);
//		int num = 40;
//		float offset = 360 / num;
//		float angle = 0;
//		for (int i =0; i<num; i++) {
//			GameObject carGO = resourceM.Create ("Car");
//			planetM.defaultPlanet.Set (carGO, angle, .25f);
//			camera.target = carGO;
//			angle += offset;
//		}

		//creating a train
		GameObject trainGO = resourceM.Create ("Train");
//		Train train = trainGO.GetComponent<Train>();
		planetM.defaultPlanet.Set (  trainGO , 90);


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
