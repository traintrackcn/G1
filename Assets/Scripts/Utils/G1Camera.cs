using UnityEngine;
using System.Collections;

public class G1Camera : G1MonoBehaviour {

	public GameObject target;
	protected float targetCameraOrthographicSize = 6;
	int focusedTrainGOIndex = 0;

	float fromAngle;
	float toAngle;

	// Use this for initialization
	void Start () {
//		locomotiveTransfrom = GameObject.Find ("Locomotive").transform.GetChild (1);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		transform.LookAt (locomotive.transform.position);



		if (target != null) {
			Focus();
		}


		if(Input.GetKeyUp ("z")) {
			ZoomOut();
		}
		
		if(Input.GetKeyUp ("x")) {
			ZoomIn();
		}
		
		if (Input.GetKeyUp ("f")) {
			FocusNext();
		}
		
		if (cameraM.target == null) {
			FocusNext();
		}
		
		//		Debug.Log ("camera -> " + camera.orthographicSize);
		
		if (camera.orthographicSize != targetCameraOrthographicSize) {
			camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetCameraOrthographicSize, Time.deltaTime*4);
		}


	}

	void Focus(){

		Planet planet = planetM.defaultPlanet;

		float targetNormalAngle = planet.GetNormalAngleAtPosition(target.transform.position);
		float cameraNormalAngle = planet.GetNormalAngleAtPosition (transform.position);

		float from = cameraNormalAngle;
		float to = targetNormalAngle;

		if ((to < 90 && from > 270)) {
//			Debug.Log ("from -> " + from + " to:" + to);
			to += 360;
		}else if ((from < 90 && to > 270)) {
//			Debug.Log ("from -> " + from + " to:" + to);
			from += 360;
		}

//		Debug.Log ("cameraNormalAngle -> "+ cameraNormalAngle+" targetNormalAngle ->"+targetNormalAngle);
//		Debug.Log ("from -> " + from + " to:" + to);
	
		float normalAngle = Mathf.Lerp(from, to, Time.deltaTime*3);
		
		transform.localRotation = Quaternion.Euler(0,0,normalAngle-90);
		
		Vector3 targetPos = planet.GetPositionAtNormalAngle(normalAngle);
		targetPos = new Vector3(targetPos.x, targetPos.y, -10);
		transform.position = targetPos;
		
	}

	void ZoomIn(){
		if (targetCameraOrthographicSize - 3 < .1f)
						return;
		targetCameraOrthographicSize -= 3;
	}
	
	void ZoomOut(){
		if (targetCameraOrthographicSize + 3 > 30)
						return;

		targetCameraOrthographicSize += 3;
		
	}
	
	void FocusNext(){
		GameObject go = trainC.GetItemAtIndex (focusedTrainGOIndex);
//		Debug.Log ("go:" + go);
		target = go.transform.GetChild(0).gameObject;

		
		if (focusedTrainGOIndex + 1 == trainC.GetCount ()) {
			focusedTrainGOIndex = 0;
		} else {
			focusedTrainGOIndex ++;
		}
	}

}
