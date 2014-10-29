using UnityEngine;
using System.Collections;

public class G1Camera : G1MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () {
//		locomotiveTransfrom = GameObject.Find ("Locomotive").transform.GetChild (1);

	}
	
	// Update is called once per frame
	void Update () {
//		transform.LookAt (locomotive.transform.position);
		if (target != null) {
			transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, -10);
//			Vector3 to = new Vector3 (target.transform.position.x, target.transform.position.y, -10);
//			transform.position = Vector3.Slerp(transform.position, to , Time.deltaTime);
//			transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, Time.deltaTime);

//			RailroadCar car = target.GetComponent<RailroadCar>();
//			Debug.Log("car angle -> "+car.angle);
//			float distance = Vector3.Distance(target.transform.position, planetM.defaultPlanetGO.transform.position);

			float angle = planetM.defaultPlanet.GetAngleAtPosition(target.transform.position);

//			Debug.Log("target->"+target+" angle -> "+angle);

			transform.localRotation = Quaternion.Euler(0,0,angle);
		}
	}
}
