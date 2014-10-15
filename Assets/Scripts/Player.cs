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




}
