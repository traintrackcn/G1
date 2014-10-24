using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {


	public float r = 22.4f;
	// Use this for initialization
	void Start () {
		AssembleSolidSurface ();
	}
	

	public void Set (GameObject obj, float angle, float heightToCenter){
		float radian = (angle *  Mathf.PI)/180.0f;

		float cos = Mathf.Cos (radian);
		float sin = Mathf.Sin (radian);
		float offsetX = heightToCenter * cos;
		float offsetY = heightToCenter * sin;

		Vector2 pos = GetPositionAtAngle(angle);
		obj.transform.position = new Vector2(pos.x+ offsetX,pos.y+ offsetY);
		obj.transform.localRotation = Quaternion.Euler(0, 0, angle-90);
	}

	//for test purpose
	void AssembleSolidSurface(){
		for (int angle =0; angle<=359; angle++) {
			GameObject obj = AssemblePlateAtAngle (angle);
			obj.name = "Plate-"+angle;
			obj.transform.parent = transform;
		}
	}

	public GameObject AssemblePlateAtAngle(float angle){
		GameObject obj = new GameObject ();
		obj.AddComponent<BoxCollider2D>();
		BoxCollider2D boxCollider = obj.GetComponent<BoxCollider2D> ();
		float w = (2.0f * Mathf.PI * r) / 360.0f;
		boxCollider.size = new Vector2 (w, 0.1f);

		float heightFromBottomToCenter = obj.transform.localScale.y * boxCollider.size.y/2.0f;

		Vector2 pos = GetPositionAtAngle(angle);
		obj.transform.position = new Vector2(pos.x ,pos.y-heightFromBottomToCenter);

//		float angle = i;
		angle -= 90;

//		Debug.Log("angle -> "+angle);
		obj.transform.localRotation = Quaternion.Euler(0, 0, angle);

		return obj;
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 20, 0), Color.blue);
	}

	public Vector2 GetPositionAtAngle (float angle){

		float radian = (angle *  Mathf.PI)/180.0f;

		Vector2 pos = new Vector2 (0, 0);
		float cos = Mathf.Cos (radian);
		float sin = Mathf.Sin (radian);
		float x = r * cos;
		float y = r * sin;

//		Debug.Log ("sin -> " + sin + "cos -> " + cos);

		pos.x = x + transform.position.x;
		pos.y = y + transform.position.y;
		return pos;
	}


}
