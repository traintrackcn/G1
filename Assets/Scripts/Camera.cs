using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () {
//		locomotiveTransfrom = GameObject.Find ("Locomotive").transform.GetChild (1);

	}
	
	// Update is called once per frame
	void Update () {
//		transform.LookAt (locomotive.transform.position);
		transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
	}
}
