using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ResourceManager : MonoBehaviour {

	protected Dictionary<string,Object> resourceCache = new Dictionary<string, Object>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public GameObject Create(string name){
		//prefabs must be placed under Assets/Resources
		string path = "Prefabs/" + name;
		Object resource;
		
		if (resourceCache.ContainsKey (path)) {
			resource = resourceCache [path];
		}else{
			Debug.Log("loading path -> "+path);
			resource = Resources.Load(path);
			resourceCache.Add(path, resource);
		} 
		
		return Instantiate (resource) as GameObject;
	}
}
