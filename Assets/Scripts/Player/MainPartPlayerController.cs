using UnityEngine;
using System.Collections;

public class MainPartPlayerController : PlayerController {



	// Update is called once per frame
	public new void Update () {
		base.Update ();
		
		if (Input.GetKey ("right")) {
			ActionRight ();
		} else if(Input.GetKeyUp ("right")) {
			ActionIdle();
		}

		if (Input.GetKey ("left")) {
			ActionLeft();
		}else if(Input.GetKeyUp ("left")) {
			ActionIdle();
		}


		if (Input.GetKey ("up")) {
			ActionUp();
		}else if(Input.GetKeyUp ("up")) {
			ActionIdle();
		}
	}
}
