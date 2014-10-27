using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class G1MonoBehaviour : MonoBehaviour {

	protected PlanetManager planetM;
	protected ResourceManager resourceM;
	protected Camera cameraM;

	private GameObject m;
	private GameObject cameraGO;


	public void Awake(){

		m = GameObject.Find ("Main");

		if (m != null) {
			planetM = m.GetComponent<PlanetManager> ();
			resourceM = m.GetComponent<ResourceManager> ();
		}

		cameraGO = GameObject.Find ("Camera");
		if (m != null) {
			cameraM = cameraGO.GetComponent<Camera> ();
		}

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
	
	public GameObject ApplySkin(GameObject target, Object skinPrefab){
		GameObject skin = Instantiate (skinPrefab) as GameObject;
		skin.transform.parent = target.transform;
		skin.transform.localPosition = new Vector2 (0, 0);
		skin.transform.localScale = Vector2.one;
		skin.transform.localRotation = Quaternion.identity;
		return skin;
	}

	public float RadianOfAngle(float angle){
		return (angle *  Mathf.PI)/180.0f;
	}
}
