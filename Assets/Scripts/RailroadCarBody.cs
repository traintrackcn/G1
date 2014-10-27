using UnityEngine;
using System.Collections;

public class RailroadCarBody : G1MonoBehaviour {

	public float offsetY = .1f;
	public float offsetX = .4f;
	public Object skinPrefab;
	public GameObject skin;


	new void Awake(){
		base.Awake ();

		skin = ApplySkin (gameObject, skinPrefab);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
