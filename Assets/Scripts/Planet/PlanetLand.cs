using UnityEngine;
using System.Collections;

public class PlanetLand : G1MonoBehaviour {

	public Object skinPrefab;
	
	// Use this for initialization
	void Start () {
		ApplySkin (gameObject, skinPrefab);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
