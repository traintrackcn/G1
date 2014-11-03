using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

//	protected Planetary2D planetary2d;


	// Use this for initialization
	public void Start () {
		CreateRigidbody2D ();
	}


	public void CreateRigidbody2D(){
		gameObject.AddComponent<Rigidbody2D>(); // Add the rigidbody.
		rigidbody2D.gravityScale = 0; 
		rigidbody2D.drag = .3f;
		rigidbody2D.fixedAngle = true;

	}

	// Update is called once per frame
	public void Update () {

	}


	void OnCollisionEnter2D(Collision2D collission) {
//		GameObject targetGO = collission.gameObject;
//		if (targetGO.tag == "Vehicle") {
////			coll.gameObject.SendMessage("ApplyDamage", 10);
//			Debug.Log("Collide vehicle");
//			Debug.Log("targetGO.collider2D -> "+targetGO.collider2D+" gameObject.collider2D -> "+gameObject.collider2D);
//			Physics2D.IgnoreCollision (targetGO.collider2D, gameObject.collider2D);
//		}
	}

}
