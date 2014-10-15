using UnityEngine;
using System.Collections;

public class Planetary2D : MonoBehaviour {

	private float absG = 9.8f;
	private GameObject planet;
	private Vector2 gravity;
	private float gravityAngle;
	private float gravityCos;
	private float gravitySin;

	// Use this for initialization
	void Start () {
		planet = GameObject.Find ("Planet");
		Physics2D.gravity = new Vector2 ();
	}

	void FixedUpdate(){

		float distanceYToPlanetCenter =  planet.transform.position.y - transform.position.y;
		float distanceXToPlanetCenter = planet.transform.position.x - transform.position.x;
		float distanceToPlanetCenter = Vector3.Distance (transform.position, planet.transform.position);

		Debug.DrawLine (transform.position, planet.transform.position, Color.red);

		gravityCos = distanceXToPlanetCenter / distanceToPlanetCenter;
		gravitySin = distanceYToPlanetCenter / distanceToPlanetCenter;

		float gravityX = gravityCos * absG;
		float gravityY = gravitySin * absG;


//		Debug.DrawLine (transform.position, new Vector3(transform.position.x+gravityX, transform.position.y+gravityY,0), Color.red);
//		float d = Mathf.Pow (gravityX, 2) + Mathf.Pow (gravityY, 2);
//		d = Mathf.Sqrt (d);


//		Debug.Log ("gx:"+gravityX + "gy:"+gravityY+"d:"+d);

		gravity = new Vector2 ();
		gravity.x = gravityX;
		gravity.y = gravityY;

		gravity *= rigidbody2D.mass;
		
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
		float forceXInPlanetG = force.x;
		float forceYInPlanetG = force.y;

		f.x = forceXInPlanetG * -gravitySin;
		f.y = forceXInPlanetG * gravityCos;

		f.x += forceYInPlanetG * -gravityCos;
		f.y += forceYInPlanetG * -gravitySin;

		
		float d = Mathf.Pow (f.x, 2) + Mathf.Pow (f.y, 2);
		d = Mathf.Sqrt (d);
		
		
		if (d != 0) {
//			Debug.DrawLine (transform.position, new Vector3(transform.position.x+f.x,transform.position.y,0));
//			Debug.DrawLine (transform.position, new Vector3(transform.position.x,transform.position.y+f.y,0));
//			Debug.DrawLine (transform.position, new Vector3(transform.position.x+f.x,transform.position.y+f.y,0), Color.green);




			Debug.DrawLine (transform.position, new Vector3(transform.position.x+rigidbody2D.velocity.x,transform.position.y,0));
			Debug.DrawLine (transform.position, new Vector3(transform.position.x,transform.position.y+rigidbody2D.velocity.y,0));
			Debug.DrawLine (transform.position, new Vector3(transform.position.x+rigidbody2D.velocity.x,transform.position.y+rigidbody2D.velocity.y,0), Color.green);

//			Vector2 v = this.velocity;
//
//			float xOutSpace = (transform.position.x + v.x);
//			float dOutSpace = xOutSpace / gravityCos;
//			float yOutSpace = gravitySin * dOutSpace;
//
//			Debug.Log("gravitySin -> "+gravitySin);

//			Debug.Log("planet2d v.x -> "+v.x+" v.y -> "+v.y);

//
//			Debug.DrawLine (transform.position, new Vector3(transform.position.x + xOutSpace, transform.position.y+yOutSpace,0), Color.cyan);
//			Debug.DrawLine (transform.position, new Vector3(transform.position.x,transform.position.y+v.y * gravityCos,0));
//			Debug.DrawLine (transform.position, new Vector3(transform.position.x+v.x,transform.position.y+v.y,0), Color.green);

//			Debug.Log ("d:" + d +" gravitySin:"+gravitySin+" gravityCos:"+gravityCos);
		}
		
		
		rigidbody2D.AddForce (f);
	}






//	public float AbsVelocity {
//		get {
//			return Mathf.Pow(rigidbody2D.velocity.x, 2) + Mathf.Pow(rigidbody2D.velocity.y, 2);
//		}
//	}

	public Vector2 velocity{
//		get {
//
//			float absV = Mathf.Pow(rigidbody2D.velocity.x, 2) + Mathf.Pow(rigidbody2D.velocity.y, 2);
//			absV = Mathf.Sqrt(absV);
//
//			Vector2 v = new Vector2();
//			v.x = absV * -gravitySin ;
//			v.y = absV * gravityCos ;
//
//
//			v.x = Mathf.Abs(v.x);
//			if(inertia.x<0) v.x = -v.x;
//
//			v.y = Mathf.Abs(v.y);
//			if(inertia.y<0) v.y = -v.y;
//
//
//			return v;
//
//		}

		get{
			Vector2 v = new Vector2();

			v.x = rigidbody2D.velocity.x * -gravitySin;
			v.y = rigidbody2D.velocity.x * gravityCos;
			
			v.x += rigidbody2D.velocity.y * gravityCos;
			v.y += rigidbody2D.velocity.y * gravitySin;


//			Debug.Log("v.x from verticalVX:"+(rigidbody2D.velocity.x * -gravitySin)+"v.x form verticalVY"+(rigidbody2D.velocity.y * -gravityCos));
//
//			Debug.Log("v.y from verticalVX:"+(rigidbody2D.velocity.x * gravityCos)+"v.y form verticalVY"+(rigidbody2D.velocity.y * -gravitySin));

			return v;

		}
	}

	public float FirstCosmicVelocity {
		get{
			float distanceToPlanetCenter = Vector3.Distance (transform.position, planet.transform.position);
			float v = Mathf.Sqrt (absG * distanceToPlanetCenter);
			return v;
		}
	}

	public float SecondCosmicVelocity {
		get {
			return this.FirstCosmicVelocity * Mathf.Sqrt(2.0f);
		}
	}
	

	
}
