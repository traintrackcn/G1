﻿using UnityEngine;
using System.Collections;

public class Planet : G1MonoBehaviour {


	public float r = 22.4f;
	// Use this for initialization
	new void Start () {
		base.Start ();
		AssembleSolidSurface ();
	}
	

	public void Set (GameObject obj, float angle, float landToCenter){
		float radian = RadianOfAngle(angle);

//		Transform anchorTransform = obj.transform.GetChild (0);
//		heightToCenter = Mathf.Abs (anchorTransform.localPosition.y);
//		Debug.Log ("heightToCenter->"+heightToCenter);

		float cos = Mathf.Cos (radian);
		float sin = Mathf.Sin (radian);
		float offsetX = landToCenter * cos;
		float offsetY = landToCenter * sin;

		Vector2 pos = GetPositionAtAngle(angle);
		obj.transform.position = new Vector2(pos.x+ offsetX,pos.y+ offsetY);
		obj.transform.localRotation = Quaternion.Euler(0, 0, angle-90);
	}

	//for test purpose
	void AssembleSolidSurface(){
		for (int angle =0; angle<=359; angle++) {
			GameObject obj = AssemblePlateAtAngle (angle);
			obj.name = "Land-"+angle;
			obj.transform.parent = transform;
		}
	}

	public GameObject AssemblePlateAtAngle(float angle){
		GameObject obj = resourceM.Create("Land"); 
		BoxCollider2D collider = obj.GetComponent<BoxCollider2D> ();

		float wActual = (2.0f * Mathf.PI * r) / 360.0f;
		float hActural = .1f;

		float xScale = wActual / collider.size.x;
		float yScale = hActural / collider.size.y;

		float heightFromBottomToCenter = hActural/2.0f;
		obj.transform.localScale = new Vector2 (xScale, yScale);

		Vector2 pos = GetPositionAtAngle(angle);
		obj.transform.position = new Vector2(pos.x ,pos.y-heightFromBottomToCenter);

//		float angle = i;
		angle -= 90;

//		Debug.Log("angle -> "+angle);
		obj.transform.localRotation = Quaternion.Euler(0, 0, angle);

		return obj;
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 20, 0), Color.blue);
	}

	public Vector2 GetPositionAtAngle (float angle){

		float radian = RadianOfAngle (angle);

		Vector2 pos = new Vector2 (0, 0);
		float cos = Mathf.Cos (radian);
		float sin = Mathf.Sin (radian);
		float x = r * cos;
		float y = r * sin;

//		Debug.Log ("sin -> " + sin + "cos -> " + cos);

		pos.x = x + transform.position.x;
		pos.y = y + transform.position.y;
		return pos;
	}


}
