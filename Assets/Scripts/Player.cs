using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private int STATE_IDLE = 0;
	private int STATE_WAIT = 1;
	private int STATE_WALK = 2;
	private int STATE_JET = 3;
	private string keyOfState = "AnimState";
	private Animator animator;

	public float f = 10.0f;
	public Vector2 maxV = new Vector2 (3.0f, 3.0f);
	private Planetary2D planetary2d;


//	private Vector2 forceCache;
	private Vector2 planetaryV;

	// Use this for initialization
	void Start () {
		Debug.Log("Player is created");
		animator = GetComponent<Animator> ();
		planetary2d = GetComponent<Planetary2D>();
//		Physics2D.gravity = gravity;
	}


	
	// Update is called once per frame
	void Update () {

		planetaryV = planetary2d.velocity;

		if (Input.GetKey ("right")) {
			OnPressRight();
		} else if (Input.GetKey ("left")) {
			OnPressLeft();
		} else if (Input.GetKey ("up")) {
			OnPressUp();
		}else{
			animator.SetInteger(keyOfState, STATE_IDLE);
		}



	}


	void OnPressLeft (){

//		Debug.Log ("Mathf.Abs(planetaryV.x:"+Mathf.Abs(planetaryV.x)+ " maxV.x:"+maxV.x);

			if (Mathf.Abs(planetaryV.x) < maxV.x) {
//				forceCache.x = -force;
				planetary2d.AddForce(new Vector2(-f,0));
				animator.SetInteger(keyOfState, STATE_WALK);
			}
			
			
			if (Input.GetKey ("up")) {
				OnPressUp();
			}
			
			transform.localScale = new Vector3 (-1, 1, 1);



	}

	void OnPressRight (){

			if (Mathf.Abs(planetaryV.x) < maxV.x) {
			planetary2d.AddForce(new Vector2(f,0));
			animator.SetInteger(keyOfState, STATE_WALK);
			}
			
			if (Input.GetKey ("up")) {
				OnPressUp();
			}
			
			transform.localScale = new Vector3 (1, 1, 1);


	}

	void OnPressUp (){
		if(Mathf.Abs(planetaryV.y) < maxV.y){
			planetary2d.AddForce(new Vector2(0,f));
			animator.SetInteger(keyOfState, STATE_JET);
		}
	}

}
