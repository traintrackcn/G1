using UnityEngine;
using System.Collections;

public class Planet : G1MonoBehaviour {


	public float defaultR = 22.4f;
	protected float defaultC = 2 * Mathf.PI *22.4f;
//	public GameObject skin;
	public GameObject atomsphereGO;
	public SpriteRenderer atomsphereRenderer;

	protected PlanetSun sun;




	new void Awake(){
		base.Awake ();
		defaultC = 2 * Mathf.PI * defaultR;

		sun = GetComponent<PlanetSun> ();

		//update radius of planet collider
		CircleCollider2D bodyCollider = GetComponent<CircleCollider2D>();
		bodyCollider.radius = defaultR;
		//set skin scale

//		Debug.Log ("defaulR:" + defaultR);
//		Transform skinTransform = transform.GetChild (0);
//		skinTransform.localScale = new Vector2 (defaultR*2, defaultR*2);

		//adjust atomsphere 

		float atomsphereR = 10.24f;
//		float offsetR = 10.0f; //white part of atomsphere
		float scale = (defaultR + defaultR) / atomsphereR;
		atomsphereGO.transform.localScale = new Vector2 (scale * 2, scale * 2);

		atomsphereRenderer = atomsphereGO.GetComponent<SpriteRenderer> ();
	}



	// Use this for initialization
	void Start () {
		AssembleSolidSurface ();
	}


	void Update(){
		atomsphereRenderer.color = sun.color;
//		Debug.Log ("atomsphereRenderer.color -> " + atomsphereRenderer.color);
	}

/** interal functions **/
	//for test purpose
	void AssembleSolidSurface(){

		float targetR = defaultR;
		float unitSideLength;
		for (int i=0; i<1; i++) {
			unitSideLength = AssembleBlocks(0,360, targetR, "Planet/PlanetGrassBlock",0.5f);
			targetR -= unitSideLength;
//			Debug.Log("targetR -> "+targetR);
//			if (targetR < 50) break;
		}

		AssembleTrees (0,360,defaultR,"Plants/Tree");
	}

	public void AssembleTrees(int fromNormalAngle, int toNormalAngle, float r, string prefabName){
		for (float angle = fromNormalAngle; angle < toNormalAngle; angle=angle+.5f) {
			if (Random.value > .5f){
				AssembleUnit(angle, r, prefabName);
			}
		}
	}

	public void AssembleUnit(float normalAngle,float r,string prefabName){
		GameObject obj = resourceM.Create(prefabName);
		BoxCollider2D collider = obj.GetComponent<BoxCollider2D> ();

		float xScale = 1.0f;
		float yScale = 1.0f + Random.value;

//		Debug.Log ("Random.value -> "+Random.value);

		float landToCenter = (collider.size.y*yScale)/2.0f;
		obj.transform.localScale = new Vector2 (xScale, yScale);
		Set(obj,(float)normalAngle,r,landToCenter);
		obj.transform.parent = transform;
	}


	public float AssembleBlocks(float fromNormalAngle, float toNormalAngle, float r, string prefabName, int blockNum){
		float c = 2 * Mathf.PI * r;
		float sideLen = c / blockNum;
		AssembleBlocks (fromNormalAngle, toNormalAngle, r, prefabName, sideLen);
		return sideLen;
	}
	
	public float AssembleBlocks(float fromNormalAngle, float toNormalAngle, float r, string prefabName, float sideLen){
//		float c = 2 * Mathf.PI * r;
		float angleOffset = GetNormalAngleByDistance (0, sideLen);

		for (float angle = fromNormalAngle; angle < toNormalAngle; angle+=angleOffset) {
			AssembleBlock(angle, prefabName, r, sideLen);
		}

		return sideLen;
	}

	public void AssembleBlock(float normalAngle,string prefabName, float r, float sideLen){
		GameObject obj = resourceM.Create(prefabName);
		BoxCollider2D collider = obj.GetComponent<BoxCollider2D> ();
		float xScale = sideLen / collider.size.x;
		float yScale = sideLen / collider.size.y;
		float landToCenter = -sideLen/2.0f;
		obj.transform.localScale = new Vector2 (xScale, yScale);
		Set(obj,normalAngle,r,landToCenter);
		obj.name = "r"+r+"-"+normalAngle;
		obj.transform.parent = transform;
	}


//	void FixedUpdate(){
//		float targetAtomspereScale = atomsphereScales [targetAtomsphereScaleIndex];
//		float scale = Mathf.Lerp (atomsphere.transform.localScale.x,targetAtomspereScale, Time.deltaTime);
//		atomsphere.transform.localScale = new Vector2 (scale, scale);
//
//		Debug.Log ("Mathf.FloorToInt(scale) -> " + Mathf.FloorToInt(scale));
//		if (Mathf.FloorToInt(scale) == 30) {
//			targetAtomsphereScaleIndex = 2;
//		}
//
//		if (Mathf.FloorToInt (scale) == 119) {
//			targetAtomsphereScaleIndex = 4;
//		}
//
//	}


	// Update is called once per frame
//	void Update () {
//		Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 20, 0), Color.blue);
//	}

	
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
		
		Vector2 pos = GetPositionAtNormalAngle(angle, r);
		obj.transform.position = new Vector2(pos.x+ offsetX,pos.y+ offsetY);
		obj.transform.localRotation = Quaternion.Euler(0, 0, angle-90);
	}

/** utils **/

	public float GetNormalAngleByDistance(float normalAngleRef, float distance, float r){
		float c = 2 * Mathf.PI * r;
		float normalAngleOffset = (distance/c )*360 ;
		return normalAngleRef + normalAngleOffset;
	}
	
	public float GetNormalAngleByDistance(float normalAngleRef, float distance){
		return GetNormalAngleByDistance (normalAngleRef, distance, defaultR);
		//		Debug.Log ("c:" + c);
//		float angleOffset = (distance/defaultC )*360 ;
//		return angleRef + angleOffset;
	}

	public Vector2 GetPositionAtNormalAngle (float normalAngle, float r){
		float radian = RadianOfAngle (normalAngle);
		
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

	public Vector2 GetPositionAtNormalAngle (float normalAngle){
		return GetPositionAtNormalAngle(normalAngle, defaultR);
	}

	public float GetNormalAngleAtPosition (Vector2 pos){
		float r = Vector2.Distance (pos, transform.position);
		float offsetY = pos.y - transform.position.y;
		float offsetX = pos.x - transform.position.x;

		float sin = Mathf.Abs (offsetY / r);
//		float cos = Mathf.Abs (offsetY / r);

//		Debug.Log ("offsetX:" + offsetX + " offsetY:" + offsetY);
//		Debug.Log ("r->" + r +"pos.y -> "+pos.y+ " sin->" + sin+"planet.center ->"+transform.position);
		float normalAngle = AngleOfSin(sin);
//		Debug.Log ("normalAngle beore -> " + normalAngle);

		if (offsetX > 0) {
			if (offsetY > 0) { //quadrant1

			} else if(offsetY < 0){ //quadrant4
				normalAngle = 360 - normalAngle;
			}
		}else if (offsetX < 0){
			if (offsetY > 0){ //quadrant2
				normalAngle = 180 - normalAngle;
			}else if(offsetY < 0){
				normalAngle += 180;
			}
		}

//		Debug.Log ("normalAngle after -> " + normalAngle);

		return normalAngle;
	}


}
