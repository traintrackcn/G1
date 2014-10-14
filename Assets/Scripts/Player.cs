using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private int STATE_IDLE = 0;
	private int STATE_WAIT = 1;
	private int STATE_WALK = 2;
	private int STATE_JET = 3;
	private string keyOfState = "AnimState";


	private Animator animator;
	// Use this for initialization
	void Start () {
		Debug.Log("Player is created");
		animator = GetComponent<Animator> ();

		animator.SetInteger(keyOfState, STATE_WAIT);



//		Physics2D.gravity = gravity;
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
