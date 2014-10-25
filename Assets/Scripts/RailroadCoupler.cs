using UnityEngine;
using System.Collections;

public class RailroadCoupler : MonoBehaviour {

	public BoxCollider2D mainCollider;
	public WheelJoint2D rightJoint;
	public WheelJoint2D leftJoint;


	// Use this for initialization
	void Start () {
//		 = new BoxCollider2D ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Bind(RailroadCar master, RailroadCar slave){
//		WheelJoint2D joint2d = new WheelJoint2D ();
//		joint2d.anchor
	}

	public void Unbind(){
		
	}
}
