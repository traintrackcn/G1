﻿using UnityEngine;
using System.Collections;

public class Train : G1MonoBehaviour {

	RailroadCar caboose;

	// Use this for initialization
	void Start () {
		SetLocomotive ("Train/RailroadCar", 90, false);

		for (int i=0; i<10; i++) {
						SetCar ("Train/RailroadCar");
				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetLocomotive(string name, float angle, bool headRight){
		GameObject carGO = resourceM.Create (name);

		RailroadCar car = carGO.GetComponent<RailroadCar> ();
		Locomotive l = carGO.AddComponent<Locomotive> ();
		planetM.defaultPlanet.Set (carGO, angle, car.wheelR);
		car.headRight = headRight;
		carGO.name = "Locomotive";


		cameraM.target = carGO;

		if (!headRight) {
			l.f = -10;
			car.body.skin.transform.localScale = new Vector3(-1,1,1);
		} else {
			l.f = 10;
		}


		caboose = car;
		caboose.transform.parent = transform;
	}

	void SetCar(string name){
		if (caboose == null) return;
		caboose = caboose.Connect(name);
	}

}
