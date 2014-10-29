using UnityEngine;
using System.Collections;

public class G1Camera : G1MonoBehaviour {

	public GameObject target;
	protected float targetCameraOrthographicSize = 6;
	int focusedTrainGOIndex = 0;

	// Use this for initialization
	void Start () {
//		locomotiveTransfrom = GameObject.Find ("Locomotive").transform.GetChild (1);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		transform.LookAt (locomotive.transform.position);
		if (target != null) {
//			transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, -10);

			Vector3 from = transform.position;
			Vector3 to = new Vector3 (target.transform.position.x, target.transform.position.y, -10);
//			float distance = Vector3.Distance(from,to);
//			Debug.Log("distance -> "+distance);
			transform.position = Vector3.Slerp(from, to, Time.fixedDeltaTime*6);
	

			float angle = planetM.defaultPlanet.GetAngleAtPosition(transform.position);
			transform.localRotation = Quaternion.Euler(0,0,angle);


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

	void ZoomIn(){
		if (targetCameraOrthographicSize - 3 < .1f)
						return;
		targetCameraOrthographicSize -= 3;
	}
	
	void ZoomOut(){
		if (targetCameraOrthographicSize + 3 > 15)
						return;

		targetCameraOrthographicSize += 3;
		
	}
	
	void FocusNext(){
		GameObject go = trainC.GetItemAtIndex (focusedTrainGOIndex);
//		Debug.Log ("go:" + go);
		cameraM.target = go.transform.GetChild(0).gameObject;
		
		if (focusedTrainGOIndex + 1 == trainC.GetCount ()) {
			focusedTrainGOIndex = 0;
		} else {
			focusedTrainGOIndex ++;
		}
	}

}
