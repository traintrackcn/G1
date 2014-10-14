using UnityEngine;
using System.Collections;

public class PlanetaryGravity2D : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate(){
		Vector2 gravity = new Vector2 ();
		gravity.x = 0;
		gravity.y = - 9.8f;
		
		rigidbody2D.AddForce (gravity);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
