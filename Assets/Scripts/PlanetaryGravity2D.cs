using UnityEngine;
using System.Collections;

public class PlanetaryGravity2D : MonoBehaviour {

	private float absG = .98f;
	private GameObject planet;

	// Use this for initialization
	void Start () {
		planet = GameObject.Find ("Planet");
	}

	void FixedUpdate(){

		float distanceYToPlanetCenter =  planet.transform.position.y - transform.position.y;
		float distanceXToPlanetCenter = planet.transform.position.x - transform.position.x;
		float distanceToPlanetCenter = Vector3.Distance (transform.position, planet.transform.position);

		Debug.DrawLine (transform.position, planet.transform.position);

//		float sin = Mathf.Sin (distanceYToPlanetCenter / distanceToPlanetCenter);
//		float cos = Mathf.Cos (distanceXToPlanetCenter / distanceToPlanetCenter);

		float gravityX = (distanceXToPlanetCenter / distanceToPlanetCenter) * absG;
		float gravityY = (distanceYToPlanetCenter / distanceToPlanetCenter) * absG;

		float d = Mathf.Pow (gravityX, 2) + Mathf.Pow (gravityY, 2);
		d = Mathf.Sqrt (d);

//		Debug.Log ("gx:"+gravityX + "gy:"+gravityY+"d:"+d);

		Vector2 gravity = new Vector2 ();
		gravity.x = gravityX;
		gravity.y = gravityY;
		
		rigidbody2D.AddForce (gravity);


		float sin = gravityY/absG;
		float sinAngle = (Mathf.Asin(sin) * 180) / Mathf.PI;
		sinAngle += 90;
		Debug.Log ("sinAngle:" + sinAngle);

		if (gravityX < 0) {
			sinAngle = -sinAngle;
		}

		transform.localRotation = Quaternion.Euler(0, 0, sinAngle);

		//adapted stand angle

//		transform.localRotation = Quaternion.Euler (gravityX, gravityY, 0);

	}

	// Update is called once per frame
	void Update () {
	
	}
}
