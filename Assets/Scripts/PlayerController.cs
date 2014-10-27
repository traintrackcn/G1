using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	protected int STATE_IDLE = 0;
	protected int STATE_WAIT = 1;
	protected int STATE_WALK = 2;
	protected int STATE_JET = 3;

	private string keyOfState = "AnimState";

	protected Planetary2D planetary2d;
	protected Animator animator;
	protected Vector2 planetaryV;
	protected int currentState;

	public float f = 10.0f;
	public Vector2 maxV = new Vector2 (3.0f, 3.0f);



	// Use this for initialization
	public void Start () {
		animator = GetComponent<Animator> ();
		planetary2d = gameObject.AddComponent<Planetary2D>();
		planetary2d.fixedAngle = true;
	}
	
	// Update is called once per frame
	public void Update () {
		planetaryV = planetary2d.velocity;
	}



/** ops **/

	public void SetState(int state){
		animator.SetInteger (keyOfState, state);
		currentState = state;
	}


/** actions **/

	public void ActionLeft(){
		if (Mathf.Abs(planetaryV.x) < maxV.x) {
			planetary2d.AddForce(new Vector2(-f,0));
			SetState(STATE_WALK);
			transform.localScale = new Vector3 (-1, 1, 1);
		}
	}

	public void ActionRight(){
		if (Mathf.Abs(planetaryV.x) < maxV.x) {
			planetary2d.AddForce(new Vector2(f,0));
			SetState(STATE_WALK);
			transform.localScale = new Vector3 (1, 1, 1);
		}
	}

	public void ActionUp(){
//		if(Mathf.Abs(planetaryV.y) < maxV.y){
//			Debug.Log("planetary2d:"+planetary2d);
			planetary2d.AddForce(new Vector2(0,f));
			SetState(STATE_JET);
//		}
	}

	public void ActionIdle(){
		SetState(STATE_IDLE);
	}

}
