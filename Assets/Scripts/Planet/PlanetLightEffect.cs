using UnityEngine;
using System.Collections;

public class PlanetLightEffect : G1MonoBehaviour {


	protected SpriteRenderer spriteRenderer;
	protected PlanetSun sun;


	new void Awake(){
		base.Awake ();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		sun = planetM.defaultPlanetGO.GetComponent<PlanetSun>();

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		camera.
//		Debug.Log("HideFlags:"+HideFlags);
//		if (renderer.isVisible) {
//			for(int i=0;i<1000;i++){
		spriteRenderer.color = sun.color;
//			}
//		}
	}
}
