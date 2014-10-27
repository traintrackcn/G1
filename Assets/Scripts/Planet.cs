using UnityEngine;
using System.Collections;

public class Planet : G1MonoBehaviour {


	public float r = 22.4f;
	protected float c = 2 * Mathf.PI *22.4f;


	new void Awake(){
		base.Awake ();
			Debug.Log ("r->" + r);
		c = 2 * Mathf.PI * r;
	}



	// Use this for initialization
	void Start () {
		AssembleSolidSurface ();
	}

/** main ops **/
	public void Set (GameObject obj, float angle){
		Set (obj, angle, 0);
	}

	public void Set (GameObject obj, float angle, float landToCenter){
		float radian = RadianOfAngle(angle);
		float cos = Mathf.Cos (radian);
		float sin = Mathf.Sin (radian);
		float offsetX = landToCenter * cos;
		float offsetY = landToCenter * sin;

		Vector2 pos = GetPositionAtAngle(angle);
		obj.transform.position = new Vector2(pos.x+ offsetX,pos.y+ offsetY);
		obj.transform.localRotation = Quaternion.Euler(0, 0, angle-90);
	}

	public float GetAngleByDistance(float angleRef, float distance){
//		Debug.Log ("c:" + c);
		float angleOffset = (distance/c )*360 ;
		return angleRef + angleOffset;
	}


/** interal functions **/

	//for test purpose
	void AssembleSolidSurface(){
		for (int angle =0; angle<=359; angle++) {
			GameObject obj = resourceM.Create("Planet/PlanetLand");
			BoxCollider2D collider = obj.GetComponent<BoxCollider2D> ();

			float wActual = c / 360.0f;
			float hActural = .2f;
			
			float xScale = wActual / collider.size.x;
			float yScale = hActural / collider.size.y;

//			Debug.Log("xScale:"+xScale+" yScale:"+yScale);
			
			float heightFromBottomToCenter = -hActural/2.0f;

			obj.transform.localScale = new Vector2 (xScale, yScale);

			this.Set(obj,(float)angle,heightFromBottomToCenter);

			
			PhysicsMaterial2D m = new PhysicsMaterial2D ();
			m.friction = .9f;
			collider.sharedMaterial = m;





			obj.name = "Land-"+angle;
			obj.transform.parent = transform;
		}
	}

//	public GameObject AssemblePlateAtAngle(float angle){
//		GameObject obj = resourceM.Create("Land"); 
//		BoxCollider2D collider = obj.GetComponent<BoxCollider2D> ();
//
//		PhysicsMaterial2D m = new PhysicsMaterial2D ();
//		m.friction = .9f;
//		collider.sharedMaterial = m;
//
//		float wActual = (2.0f * Mathf.PI * r) / 360.0f;
//		float hActural = .1f;
//
//		float xScale = wActual / collider.size.x;
//		float yScale = hActural / collider.size.y;
//
//		float heightFromBottomToCenter = hActural/2.0f;
//
//		float radian = RadianOfAngle(angle);
//		float cos = Mathf.Cos (radian);
//		float sin = Mathf.Sin (radian);
//		float offsetX = heightFromBottomToCenter * cos;
//		float offsetY = heightFromBottomToCenter * sin;
//
//
//		obj.transform.localScale = new Vector2 (xScale, yScale);
//		heightFromBottomToCenter = 0;
//		Debug.Log ("heightFromBottomToCenter:" + heightFromBottomToCenter+"offsetX:"+offsetX+" offsetY:"+offsetY);
//
//		Vector2 pos = GetPositionAtAngle(angle);
//		obj.transform.position = new Vector2(pos.x + offsetX,pos.y + offsetY);
//
////		float angle = i;
//		angle -= 90;
//
////		Debug.Log("angle -> "+angle);
//		obj.transform.localRotation = Quaternion.Euler(0, 0, angle);
//
//		return obj;
//	}
	
	// Update is called once per frame
	void Update () {
//		Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 20, 0), Color.blue);
	}

	public Vector2 GetPositionAtAngle (float angle){

		float radian = RadianOfAngle (angle);

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
