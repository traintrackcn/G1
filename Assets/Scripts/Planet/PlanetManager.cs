using UnityEngine;
using System.Collections;

public class PlanetManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public GameObject defaultPlanetGO{
		get{
			return GameObject.Find("Planet");
		}
	}

	public Planet defaultPlanet{
		get{
			return defaultPlanetGO.GetComponent<Planet>();
		}
	}
}
