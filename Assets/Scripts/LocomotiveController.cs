using UnityEngine;
using System.Collections;

public class LocomotiveController : MonoBehaviour {
	protected Planetary2D planetary2d;
	protected Vector2 planetaryV;
	public float f = 1000;
	public float maxVelocity = 6.0f;

	public bool brake = false;

	// Use this for initialization
	void Start(){
		planetary2d = GetComponent<Planetary2D>();
		planetary2d.fixedAngle = true;
	}

	void FixedUpdate (){

		if (brake) {

			Debug.Log("planetaryV.x -> "+planetaryV.x);

			if (Mathf.Abs(planetaryV.x)> .1){
				planetary2d.AddForce (new Vector2 (-f*.5f, 0));


			}else{
				rigidbody2D.velocity = new Vector2(0,0);
			}


		} else {

				if (Mathf.Abs (planetaryV.x) < maxVelocity) {
						planetary2d.AddForce (new Vector2 (f, 0));
						transform.localScale = new Vector3 (1, 1, 1);
				}
		}

	}

	// Update is called once per frame
	void Update () {
		planetaryV = planetary2d.velocity;
	}
}
