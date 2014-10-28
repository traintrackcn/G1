using UnityEngine;
using System.Collections;

public class Planet : G1MonoBehaviour {


	public float defaultR = 22.4f;
	protected float defaultC = 2 * Mathf.PI *22.4f;


	new void Awake(){
		base.Awake ();
		defaultC = 2 * Mathf.PI * defaultR;

		//set skin scale

//		Debug.Log ("defaulR:" + defaultR);
//		Transform skinTransform = transform.GetChild (0);
//		skinTransform.localScale = new Vector2 (defaultR*2, defaultR*2);
	}



	// Use this for initialization
	void Start () {
		AssembleSolidSurface ();
	}




/** interal functions **/
	//for test purpose
	void AssembleSolidSurface(){
		for (int i=0; i<20; i++) {

			float targetR = defaultR - i;

			if (i == 0) {
				AssembleSurface (0, 360, targetR);
			}else{
				AssembleMud(0,360, targetR);
			}

		}
	}



	public void AssembleSurface(int from, int to, float r){
		AssembleUnits (from, to, r, "PlanetLand");
	}

	public void AssembleMud(int from, int to, float r){
		AssembleUnits (from, to, r, "PlanetMud");
	}


	public void AssembleUnits(int from, int to, float r, string prefabName){
		float landNum = to - from;
		float c = 2 * Mathf.PI * r * (landNum/360);
		float wActual = c / landNum;
		float hActural = 1.0f;

//		Debug.Log ("landNum:" + landNum + " wActural:" + wActual);

		for (int angle = from; angle < to; angle++) {

//			if (angle%10==0){
//				Vector2 pos = GetPositionAtAngle(angle, r);
//				GameObject keyPoint = resourceM.Create("Planet/PlanetKeyPoint");
//				keyPoint.transform.position = pos;
//				keyPoint.transform.parent = transform;
//			}

			GameObject obj = resourceM.Create("Planet/"+prefabName);
			BoxCollider2D collider = obj.GetComponent<BoxCollider2D> ();

			float xScale = wActual / collider.size.x;
			float yScale = hActural / collider.size.y;
			
//			Debug.Log("xScale:"+xScale+" yScale:"+yScale);
			
			float heightFromBottomToCenter = -hActural/2.0f;
			
			obj.transform.localScale = new Vector2 (xScale, yScale);
			
			Set(obj,(float)angle,r,heightFromBottomToCenter);
			
			
			PhysicsMaterial2D m = new PhysicsMaterial2D ();
			m.friction = .9f;
			collider.sharedMaterial = m;
			
			obj.name = "Land-"+angle+"-r"+r;
			obj.transform.parent = transform;
		}
	}

	

	// Update is called once per frame
	void Update () {
//		Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 20, 0), Color.blue);
	}

	
/** set ops **/

	public void Set (GameObject obj, float angle){
		Set (obj, angle, 0);
	}

	public void Set (GameObject obj, float angle, float landToCenter){
		Set (obj, angle, defaultR, landToCenter);
	}

	public void Set (GameObject obj, float angle , float r, float landToCenter){
		float radian = RadianOfAngle(angle);
		float cos = Mathf.Cos (radian);
		float sin = Mathf.Sin (radian);
		float offsetX = landToCenter * cos;
		float offsetY = landToCenter * sin;
		
		Vector2 pos = GetPositionAtAngle(angle, r);
		obj.transform.position = new Vector2(pos.x+ offsetX,pos.y+ offsetY);
		obj.transform.localRotation = Quaternion.Euler(0, 0, angle-90);
	}

/** utils **/

	public float GetAngleByDistance(float angleRef, float distance, float r){
		float c = 2 * Mathf.PI * r;
		float angleOffset = (distance/c )*360 ;
		return angleRef + angleOffset;
	}
	
	public float GetAngleByDistance(float angleRef, float distance){
		return GetAngleByDistance (angleRef, distance, defaultR);
		//		Debug.Log ("c:" + c);
//		float angleOffset = (distance/defaultC )*360 ;
//		return angleRef + angleOffset;
	}

	public Vector2 GetPositionAtAngle (float angle, float r){
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

	public Vector2 GetPositionAtAngle (float angle){
		return GetPositionAtAngle(angle, defaultR);
	}


}
