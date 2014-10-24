using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

	CarConnector connector;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject[] wheels{
		get{

		}
	}

	public void Brake(){
	}

	public void Connect (Car obj){

		if (connector) {
			return;
		}

		//create a connector instance form prefab

//		var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//		cube.AddComponent(Rigidbody);
//		Instantiate(prefab, pos, Quaternion.identity);

		connector = new CarConnector ();
		connector.Bind (this, obj);
	}

	public void Disconnect (){

		if (connector == null) {
			return;
		}

		connector.Unbind(); 
		connector = null;
	}

}
