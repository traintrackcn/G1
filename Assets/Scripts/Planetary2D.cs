using UnityEngine;
using System.Collections;

public class Planetary2D : MonoBehaviour {

	private float absG = 9.8f;
	private GameObject planet;
	private Vector2 gravity;
	private float gravityAngle;
	private float gravitySin;
	private float gravityCos;
	private float gravityTan;

	// Use this for initialization
	void Start () {
		planet = GameObject.Find ("Planet");
	}

	void FixedUpdate(){

		float distanceYToPlanetCenter =  planet.transform.position.y - transform.position.y;
		float distanceXToPlanetCenter = planet.transform.position.x - transform.position.x;
		float distanceToPlanetCenter = Vector3.Distance (transform.position, planet.transform.position);

//		Debug.DrawLine (transform.position, planet.transform.position, Color.red);

		gravityCos = distanceXToPlanetCenter / distanceToPlanetCenter;
		gravitySin = distanceYToPlanetCenter / distanceToPlanetCenter;

		float gravityX = gravityCos * absG;
		float gravityY = gravitySin * absG;


		Debug.DrawLine (transform.position, new Vector3(transform.position.x+gravityX, transform.position.y+gravityY,0), Color.red);
//		float d = Mathf.Pow (gravityX, 2) + Mathf.Pow (gravityY, 2);
//		d = Mathf.Sqrt (d);

//		Debug.Log ("gx:"+gravityX + "gy:"+gravityY+"d:"+d);

		gravity = new Vector2 ();
		gravity.x = gravityX;
		gravity.y = gravityY;
		
		rigidbody2D.AddForce (gravity);

		gravityAngle = (Mathf.Asin( gravitySin ) * 180) / Mathf.PI;
		gravityAngle += 90;


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
		float forceXInVerticalG = force.x;
		f.x = forceXInVerticalG * -gravitySin;
		f.y = forceXInVerticalG * gravityCos;

//		Debug.DrawRay (transform.position, new Vector3(force.x,0,0));
//		Planet p = GetComponent<Player> ();


		float d = Mathf.Pow (f.x, 2) + Mathf.Pow (f.y, 2);
		d = Mathf.Sqrt (d);



		if (d != 0) {
//			Debug.DrawRay (transform.position, new Vector3(transform.position.x+f.x,0,0));
			Debug.DrawLine (transform.position, new Vector3(transform.position.x+f.x,transform.position.y,0));
			Debug.DrawLine (transform.position, new Vector3(transform.position.x,transform.position.y+f.y,0));
			Debug.DrawLine (transform.position, new Vector3(transform.position.x+f.x,transform.position.y+f.y,0), Color.green);
			Debug.Log ("forceXInVerticalG:"+forceXInVerticalG+" force value -> "+ d);
		}


		rigidbody2D.AddForce (f);
	}
}
