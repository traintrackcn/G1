using UnityEngine;
using System.Collections;

public class Planetary2D : MonoBehaviour {

	private float absG = 9.8f;
	private GameObject planet;
	private Vector2 gravity;
	private float gravityAngle;
	private float gravitySin;
	private float gravityCos;

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

		gravity = new Vector2 ();
		gravity.x = gravityX;
		gravity.y = gravityY;
		
		rigidbody2D.AddForce (gravity);

		gravitySin = gravityY / absG;
		gravityCos = gravityX / absG;
		gravityAngle = (Mathf.Asin( gravitySin ) * 180) / Mathf.PI;
		gravityAngle += 90;
//		Debug.Log ("gravityAngle:" + gravityAngle);

		if (gravityX < 0) {
			gravityAngle = -gravityAngle;
		}

		transform.localRotation = Quaternion.Euler(0, 0, gravityAngle);

		//adapted stand angle

//		transform.localRotation = Quaternion.Euler (gravityX, gravityY, 0);

	}

	// Update is called once per frame
	void Update () {
	
	}

	public void AddForce(Vector2 force){
		Vector2 f = new Vector2 ();
		f.x = -force.x * gravityCos;
		f.y = force.x * gravitySin;

		Debug.Log ("f.x:" + f.x+" gravityCos:"+gravityCos+" "+force.x + " f.y:" + f.y+" "+force.y);

		rigidbody2D.AddForce (f);
	}
}
