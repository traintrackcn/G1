using UnityEngine;
using System.Collections;

public class RailroadCar : G1MonoBehaviour {

	public BoxCollider2D chasisCollider;
	public BoxCollider2D bodyCollider;

	public DistanceJoint2D couplerJoint;

	public Vector2 couplerHole;

	public Object wheelPrefab;
	public float wheelR;
	public Object bodyPrefab;
	
	public GameObject[] wheelGOs;
	public GameObject bodyGO;

	public RailroadCarBody body;

	public bool headRight;

	new void Awake(){
		base.Awake ();
		chasisCollider = GetComponent<BoxCollider2D> ();
		couplerJoint = GetComponent<DistanceJoint2D> ();
		couplerHole  = new Vector2(chasisCollider.size.x/2.0f,0);
		Assemble ();
	}

	//assemblers
	void Assemble(){
		AssembleBody ();
		AssembleWheels ();
	}

	void AssembleBody(){
		float x = 0;
		float y = 0;

		bodyGO = Instantiate(bodyPrefab) as GameObject;
		bodyCollider = bodyGO.GetComponent<BoxCollider2D> ();
		body = bodyGO.GetComponent<RailroadCarBody> ();

		y = chasisCollider.size.y / 2.0f + bodyCollider.size.y / 2.0f + body.offsetY;

		bodyGO.transform.parent = transform;
		bodyGO.transform.localPosition = new Vector2(x,y);
	}

	void AssembleWheels(){
		float chasisMaxX = chasisCollider.size.x/2;
		float chasisMinX = -chasisMaxX;
		 
		float x = 0;
		float y = 0;
		float spacing = 0;
		int num = 4;
	
		wheelGOs = new GameObject[num];
		for (int i=0; i<num; i++) {
			GameObject wheelGO = Instantiate(wheelPrefab) as GameObject;
			CircleCollider2D wheelCollider = wheelGO.GetComponent<CircleCollider2D>();
			wheelR = wheelCollider.radius;

			wheelGOs[i] = wheelGO;

			if (i== 0)  {
				x = chasisMaxX - wheelR - body.offsetX;
				spacing = wheelR*1.0f;
			}

			if (i == 2){
				x = chasisMinX + 3*wheelR + spacing + body.offsetX;
			}

			wheelGO.transform.parent = transform;
			wheelGO.transform.localPosition = new Vector2(x,y);
			wheelGO.rigidbody2D.mass = 1;

			//create wheel joint
			WheelJoint2D joint = gameObject.AddComponent<WheelJoint2D>();
			joint.anchor = new Vector2(x, y);

			JointSuspension2D suspension = new JointSuspension2D();
			suspension.frequency = 100;
			suspension.dampingRatio = .7f;
			suspension.angle = 90.0f;

			joint.suspension = suspension;
			joint.connectedBody = wheelGO.rigidbody2D;

			//next wheel parameters
			x -= spacing + wheelR*2;

		}
	}

//	public void ApplySkins(){
//		//body
//		this.ApplySkin (gameObject, bodySkinPrefab);
//
//		//wheels
//		for (int i=0; i<wheelGOs.Length; i++) {
//			GameObject wheelGO = wheelGOs[i];
//			this.ApplySkin(wheelGO, wheelSkinPrefab);
//		}
//	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
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

		float distanceBetweenCars = bodyCollider.size.x/2 + nextCar.bodyCollider.size.x/2 + couplerJoint.distance;

		//angle
		float nextCarAngle = planet.GetAngleByDistance (angleRef, distanceBetweenCars);

		if (headRight) {
			nextCarAngle = planet.GetAngleByDistance (angleRef, distanceBetweenCars);
		}else{
			nextCarAngle = planet.GetAngleByDistance (angleRef, -distanceBetweenCars);
			nextCarGO.transform.localScale= new Vector3(-1,1,1);
		}
		
//		Debug.Log ("nextCarAngle:" + nextCarAngle);

//		planet.Set (couplerGO, couplerAngle, .1f);
		planet.Set ( nextCarGO , nextCarAngle, wheelR);

//		couplerGO.transform.parent = transform.parent;
		nextCarGO.transform.parent = transform.parent;

//		if (headRight) {
//			//connet couplers
//			couplerJoint.enabled = true;
//			couplerJoint.connectedBody = nextCar.rigidbody2D;
//			couplerJoint.connectedAnchor = couplerHole;
//		}else{
//			couplerJoint.enabled = true;
//			couplerJoint.connectedBody = nextCar.rigidbody2D;
//			couplerJoint.connectedAnchor = couplerHole;
//		}

		return nextCar;
	}

}
