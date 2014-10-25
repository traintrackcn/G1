using UnityEngine;
using System.Collections;

public class RailroadCar : G1MonoBehaviour {

	public BoxCollider2D mainCollider;
	public Rigidbody2D mainRigidbody;
	public DistanceJoint2D couplerJoint;
	public Vector2 couplerHole;
	public float landToCenter;

	public bool headRight;

	new void Awake(){
		base.Awake ();
		couplerHole  = new Vector2(mainCollider.size.x/2.0f, -mainCollider.size.y/2.0f);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject[] wheels{
		get{
			return null;
		}
	}


	public void Brake(){
	}

	public float angle{
		get{
			return transform.rotation.eulerAngles.z+90;
		}
	}

	public RailroadCar Connect(string carName){

		Planet planet = planetM.defaultPlanet;

		float angleRef = angle;

//		Debug.Log ("angleRef -> " + angleRef +"   transform.rotation.eulerAngles.z:"+ transform.rotation.eulerAngles.z);

		//next car
		GameObject nextCarGO = resourceM.Create (carName);
		RailroadCar nextCar = nextCarGO.GetComponent<RailroadCar> ();
		nextCar.headRight = headRight;

		float distanceBetweenCars = mainCollider.size.x/2 + nextCar.mainCollider.size.x/2 + couplerJoint.distance;

		//angle
		float nextCarAngle = planet.GetAngleByDistance (angleRef, distanceBetweenCars);

		if (headRight) {
			nextCarAngle = planet.GetAngleByDistance (angleRef, distanceBetweenCars);
		}else{
			nextCarAngle = planet.GetAngleByDistance (angleRef, -distanceBetweenCars);
			nextCarGO.transform.localScale= new Vector3(-1,1,1);
		}
		
		Debug.Log ("nextCarAngle:" + nextCarAngle);

//		planet.Set (couplerGO, couplerAngle, .1f);
		planet.Set ( nextCarGO , nextCarAngle, landToCenter);

//		couplerGO.transform.parent = transform.parent;
		nextCarGO.transform.parent = transform.parent;

		if (headRight) {
			//connet couplers
			couplerJoint.enabled = true;
			couplerJoint.connectedBody = nextCar.mainRigidbody;
			couplerJoint.connectedAnchor = couplerHole;
		}else{
			couplerJoint.enabled = true;
			couplerJoint.connectedBody = nextCar.mainRigidbody;
			couplerJoint.connectedAnchor = couplerHole;
		}

		return nextCar;
	}

}
