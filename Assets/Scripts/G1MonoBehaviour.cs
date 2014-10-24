using UnityEngine;
using System.Collections;

public class G1MonoBehaviour : MonoBehaviour {

	protected PlanetManager planetM;
	protected Camera camera;
	// Use this for initialization
	public void Start () {
		planetM = GameObject.Find ("Main").GetComponent<PlanetManager> ();
		camera = GameObject.Find ("Camera").GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	public GameObject CloneGameObjectFromPrefab(string name){
		//prefabs must be placed under Assets/Resources
		string path = "Prefabs/" + name;
		return Instantiate (Resources.Load (path)) as GameObject;
	}
}
