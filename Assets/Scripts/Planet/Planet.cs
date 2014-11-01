﻿using UnityEngine;
using System.Collections;

public class Planet : G1MonoBehaviour {


	public float defaultR = 22.4f;
	protected float defaultC = 2 * Mathf.PI *22.4f;
	public GameObject skin;
	public GameObject atomsphere;

	public float[] atomsphereScales = new float[5]; // early morning, morning, noon , afternoon , night
	private int targetAtomsphereScaleIndex = 4;


	new void Awake(){
		base.Awake ();
		defaultC = 2 * Mathf.PI * defaultR;

		//update radius of planet collider
		CircleCollider2D bodyCollider = GetComponent<CircleCollider2D>();
		bodyCollider.radius = defaultR;
		//set skin scale

//		Debug.Log ("defaulR:" + defaultR);
//		Transform skinTransform = transform.GetChild (0);
//		skinTransform.localScale = new Vector2 (defaultR*2, defaultR*2);

		atomsphereScales [0] = 80;
		atomsphereScales [1] = 100;
		atomsphereScales [2] = 120;
		atomsphereScales [3] = 50;
		atomsphereScales [4] = 30;

		//adjust atomsphere 

		float atomsphereR = 10.24f;
		float scale = (defaultR + 10.0f) / atomsphereR;
		atomsphere.transform.localScale = new Vector2 (scale * 2, scale * 2);
	}



	// Use this for initialization
	void Start () {
		AssembleSolidSurface ();
	}




/** interal functions **/
	//for test purpose
	void AssembleSolidSurface(){

		float targetR = defaultR;
		float unitSideLength;
		for (int i=0; i<2; i++) {

			if (i==0){
//				blockPrefabName = "PlanetGrassBlock";
				unitSideLength = AssembleUnits(0,90, targetR, "PlanetGrassBlock");
				unitSideLength = AssembleUnits(90,180, targetR, "PlanetMudBlock");
				unitSideLength = AssembleUnits(180,360, targetR, "PlanetRockBlock");
			}else{
				unitSideLength = AssembleUnits(0,360, targetR, "PlanetMudBlock");
			}

			targetR -= unitSideLength;

//			Debug.Log("targetR -> "+targetR);
//			if (targetR < 50) break;
		}

		//scale planet skin
		float skinR = 10.24f;
		float offsetH = 1.0f; //cover low ploygon mud
		float skinScale = (targetR+ offsetH) / skinR; //10.24
//		Debug.Log ("skinScale -> " + skinScale);
		skin.transform.localScale = new Vector2 (skinScale * 2, skinScale * 2);
	}

	
	public float AssembleUnits(int fromNormalAngle, int toNormalAngle, float r, string prefabName){
		float landNum = toNormalAngle - fromNormalAngle;
		float c = 2 * Mathf.PI * r * (landNum/360);

		float normalAnglesPerUnit = .5f;


		if (r != defaultR) {
			normalAnglesPerUnit = 10;
//			sideLen = 10.0f;
		}

		float sideLen = (c / landNum) * normalAnglesPerUnit;

//		float sideLen = (c / landNum) * normalAnglesPerUnit;


//		Debug.Log ("landNum:" + landNum + " wActural:" + wActual);

		for (float angle = fromNormalAngle; angle < toNormalAngle; angle=angle+.5f) {

			if (angle%normalAnglesPerUnit!=0)continue;

			

			GameObject obj = resourceM.Create("Planet/"+prefabName);
			BoxCollider2D collider = obj.GetComponent<BoxCollider2D> ();

			float xScale = sideLen / collider.size.x;
			float yScale = sideLen / collider.size.y;
			
//			Debug.Log("xScale:"+xScale+" yScale:"+yScale);
			
			float heightFromBottomToCenter = -sideLen/2.0f;
			
			obj.transform.localScale = new Vector2 (xScale, yScale);
			
			Set(obj,(float)angle,r,heightFromBottomToCenter);
			
			
			PhysicsMaterial2D m = new PhysicsMaterial2D ();
			m.friction = .3f;
			collider.sharedMaterial = m;
			
			obj.name = "r"+r+"-"+angle;
			obj.transform.parent = transform;
		}

		return sideLen;
	}


//	void FixedUpdate(){
//		float targetAtomspereScale = atomsphereScales [targetAtomsphereScaleIndex];
//		float scale = Mathf.Lerp (atomsphere.transform.localScale.x,targetAtomspereScale, Time.deltaTime);
//		atomsphere.transform.localScale = new Vector2 (scale, scale);
//
//		Debug.Log ("Mathf.FloorToInt(scale) -> " + Mathf.FloorToInt(scale));
//		if (Mathf.FloorToInt(scale) == 30) {
//			targetAtomsphereScaleIndex = 2;
//		}
//
//		if (Mathf.FloorToInt (scale) == 119) {
//			targetAtomsphereScaleIndex = 4;
//		}
//
//	}


	// Update is called once per frame
	void Update () {
//		Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 20, 0), Color.blue);
	}

	
/** set ops **/

	public void Set (GameObject obj, float angle){
		Set (obj, angle, 0);
	}

	public void Set (GameObject obj, float angle, float landToCenter){
		Set (obj, angle, defaultR, landToCenter);
	}

	public void Set (GameObject obj, float angle , float r, float landToCenter){
		float radian = RadianOfAngle(angle);
		float cos = Mathf.Cos (radian);
		float sin = Mathf.Sin (radian);
		float offsetX = landToCenter * cos;
		float offsetY = landToCenter * sin;
		
		Vector2 pos = GetPositionAtNormalAngle(angle, r);
		obj.transform.position = new Vector2(pos.x+ offsetX,pos.y+ offsetY);
		obj.transform.localRotation = Quaternion.Euler(0, 0, angle-90);
	}

/** utils **/

	public float GetNormalAngleByDistance(float normalAngleRef, float distance, float r){
		float c = 2 * Mathf.PI * r;
		float normalAngleOffset = (distance/c )*360 ;
		return normalAngleRef + normalAngleOffset;
	}
	
	public float GetNormalAngleByDistance(float normalAngleRef, float distance){
		return GetNormalAngleByDistance (normalAngleRef, distance, defaultR);
		//		Debug.Log ("c:" + c);
//		float angleOffset = (distance/defaultC )*360 ;
//		return angleRef + angleOffset;
	}

	public Vector2 GetPositionAtNormalAngle (float normalAngle, float r){
		float radian = RadianOfAngle (normalAngle);
		
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

	public Vector2 GetPositionAtNormalAngle (float normalAngle){
		return GetPositionAtNormalAngle(normalAngle, defaultR);
	}

	public float GetNormalAngleAtPosition (Vector2 pos){
		float r = Vector2.Distance (pos, transform.position);
		float offsetY = pos.y - transform.position.y;
		float offsetX = pos.x - transform.position.x;

		float sin = Mathf.Abs (offsetY / r);
//		float cos = Mathf.Abs (offsetY / r);

//		Debug.Log ("offsetX:" + offsetX + " offsetY:" + offsetY);
//		Debug.Log ("r->" + r +"pos.y -> "+pos.y+ " sin->" + sin+"planet.center ->"+transform.position);
		float normalAngle = AngleOfSin(sin);
//		Debug.Log ("normalAngle beore -> " + normalAngle);

		if (offsetX > 0) {
			if (offsetY > 0) { //quadrant1

			} else if(offsetY < 0){ //quadrant4
				normalAngle = 360 - normalAngle;
			}
		}else if (offsetX < 0){
			if (offsetY > 0){ //quadrant2
				normalAngle = 180 - normalAngle;
			}else if(offsetY < 0){
				normalAngle += 180;
			}
		}

//		Debug.Log ("normalAngle after -> " + normalAngle);

		return normalAngle;
	}


}
