﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private int STATE_IDLE = 0;
	private int STATE_WAIT = 1;
	private int STATE_WALK = 2;
	private int STATE_JET = 3;
	private string keyOfState = "AnimState";
	private Animator animator;

	public float speed = 100.0f;
	public Vector2 maxV = new Vector2 (3, 5);


	// Use this for initialization
	void Start () {
		Debug.Log("Player is created");
		animator = GetComponent<Animator> ();

		animator.SetInteger(keyOfState, STATE_WALK);



//		Physics2D.gravity = gravity;
	}


	
	// Update is called once per frame
	void Update () {

		float forceX = 0f;
		float forceY = 0f;
		float absVX = Mathf.Abs (rigidbody2D.velocity.x);

		if (Input.GetKey ("right")) {
			if(absVX<maxV.x){
				forceX = speed;
			}

			transform.localScale = new Vector3 (1,1,1);
		}else if (Input.GetKey("left")){
			if(absVX<maxV.x){
				forceX = -speed;
			}

			transform.localScale = new Vector3 (-1,1,1);
		}

		Debug.Log (forceX);

		Planetary2D planetary2d = GetComponent<Planetary2D>();
		planetary2d.AddForce(new Vector2(forceX, forceY));
	}
}
