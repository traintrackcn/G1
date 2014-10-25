using UnityEngine;
using System.Collections;

public class Train : G1MonoBehaviour {

	RailroadCar caboose;

	// Use this for initialization
	void Start () {
		SetLocomotive ("RailroadCar", 90, false);

		for (int i=0; i<10; i++) {
						SetCar ("RailroadCar");
				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetLocomotive(string name, float angle, bool headRight){
		GameObject carGO = resourceM.Create (name);
		RailroadCar car = carGO.GetComponent<RailroadCar> ();
		planetM.defaultPlanet.Set (carGO, angle, car.landToCenter);
		car.headRight = headRight;
		if(!headRight) car.transform.localScale = new Vector3(-1,1,1);
		caboose = car;
		caboose.transform.parent = transform;
	}

	void SetCar(string name){
		if (caboose == null) return;
		caboose = caboose.Connect(name);
	}

}
