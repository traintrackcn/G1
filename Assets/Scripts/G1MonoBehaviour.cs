using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class G1MonoBehaviour : MonoBehaviour {

	protected PlanetManager planetM;
	protected ResourceManager resourceM;
//	protected Camera camera;

	private GameObject m;
//	private GameObject c;


	public void Awake(){
		m = GameObject.Find ("Main");
		planetM = m.GetComponent<PlanetManager> ();
		resourceM = m.GetComponent<ResourceManager> ();
	}
	

	// Use this for initialization
//	public void Start () {
//		m = GameObject.Find ("Main");
//		c = GameObject.Find ("Camera");

//		planetM = m.GetComponent<PlanetManager> ();
//		resourceM = m.GetComponent<ResourceManager> ();
//		camera = c.GetComponent<Camera> ();
//	}

	// Update is called once per frame
	void Update () {
	
	}




	public float RadianOfAngle(float angle){
		return (angle *  Mathf.PI)/180.0f;
	}
}
