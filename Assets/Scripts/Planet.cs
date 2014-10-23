using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {


	protected float r;
	// Use this for initialization
	void Start () {
		CircleCollider2D collider2d = GetComponent<CircleCollider2D> ();
		r = collider2d.radius * transform.localScale.x;

		Debug.Log ("r -> " + r);
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 20, 0), Color.blue);
	}

	public Vector2 GetPositionAtAngle (float angle){
		Vector2 pos = new Vector2 (0, 0);
		pos.x = r * Mathf.Cos (angle) + transform.position.x;
		pos.y = r * Mathf.Sin (angle) + transform.position.y;
		return pos;
	}

	public void Add (GameObject obj, float angle){
//		GameObject obj = new cls();

	}
}
