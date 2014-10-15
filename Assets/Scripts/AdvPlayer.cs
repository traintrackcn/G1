using UnityEngine;
using System.Collections;

public class AdvPlayer : Player {

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
//	override void Update () {
//		base.Update ();
//	}

	void Update(){
		base.Update ();

//		Debug.Log("planetary2d.velocity"+planetary2d.velocity.x);
	}
}
