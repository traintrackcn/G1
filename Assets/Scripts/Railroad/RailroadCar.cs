using UnityEngine;
using System.Collections;

public class RailroadCar : G1MonoBehaviour {

	public BoxCollider2D chasisCollider;
	public BoxCollider2D bodyCollider;

	public DistanceJoint2D couplerJoint;

	public Object wheelPrefab;
	public float wheelR;
	public Object bodyPrefab;
	
	public GameObject[] wheelGOs;
	public GameObject bodyGO;

	public RailroadCarBody body;

	public bool headRight;

	public Vector2 chasisEdgeR;
	public Vector2 chasisEdgeL;

	new void Awake(){
		base.Awake ();
		chasisCollider = GetComponent<BoxCollider2D> ();
		couplerJoint = GetComponent<DistanceJoint2D> ();

		chasisEdgeR = new Vector2 (chasisCollider.size.x/2,0);
		chasisEdgeL = new Vector2 (-chasisEdgeR.x,0);

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
				x = chasisEdgeR.x - wheelR - body.offsetX;
				spacing = wheelR*1.0f;
			}

			if (i == 2){
				x = chasisEdgeL.x + 3*wheelR + spacing + body.offsetX;
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



	public RailroadCar Connect(string carName){

		Planet planet = planetM.defaultPlanet;

		//because transform.rotation.eulerAngles.z is perpendicular to planet normal angle
		float normalAngleRef = transform.rotation.eulerAngles.z+90;
//		float normalAngleRef = planet.GetNormalAngleAtPosition (transform.position);

		//next car
		GameObject nextCarGO = resourceM.Create (carName);
		RailroadCar nextCar = nextCarGO.GetComponent<RailroadCar> ();
		nextCar.headRight = headRight;

		float distanceBetweenCars = bodyCollider.size.x/2 + nextCar.bodyCollider.size.x/2 + couplerJoint.distance;

		//angle
		float nextCarNormalAngle = planet.GetNormalAngleByDistance (normalAngleRef, distanceBetweenCars);

		if (headRight) {
			nextCarNormalAngle = planet.GetNormalAngleByDistance (normalAngleRef, distanceBetweenCars);
		}else{
			nextCarNormalAngle = planet.GetNormalAngleByDistance (normalAngleRef, -distanceBetweenCars);
//			nextCarGO.transform.localScale= new Vector3(-1,1,1);
			//mask body skin scale.x = -1 , most situation is for lomotive

//			Debug.Log("nextCar.body.skin -> "+nextCar.body.skin);

			nextCar.body.skin.transform.localScale = new Vector3(-1,1,1);
		}
		
//		Debug.Log ("nextCarAngle:" + nextCarAngle);

//		planet.Set (couplerGO, couplerAngle, .1f);
		planet.Set ( nextCarGO , nextCarNormalAngle, wheelR);

//		couplerGO.transform.parent = transform.parent;
		nextCarGO.transform.parent = transform.parent;

		couplerJoint.enabled = true;
		couplerJoint.connectedBody = nextCar.rigidbody2D;
		if (headRight) {
			//connet coupler
			couplerJoint.anchor = new Vector2(chasisEdgeL.x,0);
			couplerJoint.connectedAnchor = new Vector2(chasisEdgeR.x,0);
		}else{
			couplerJoint.anchor = new Vector2(chasisEdgeR.x,0);
			couplerJoint.connectedAnchor = new Vector2(chasisEdgeL.x,0);
		}

		return nextCar;
	}

}


