using UnityEngine;
using System.Collections;

public class RailroadBranch : MonoBehaviour {

	EdgeCollider2D pathCollider;

	void Awake(){
		pathCollider = gameObject.AddComponent<EdgeCollider2D> ();

		Vector2[] pts = new Vector2[10];
		pts[0] = new Vector2(16,-1);
		pts[1] = new Vector2(2,1 );
		pts[2] = new Vector2(-2,1);
		pts [3] = new Vector2 (-16,-1);


		pathCollider.points = pts;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
