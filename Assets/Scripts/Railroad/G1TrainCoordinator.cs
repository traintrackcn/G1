using UnityEngine;
using System.Collections;

public class G1TrainCoordinator : MonoBehaviour {

	GameObject[] data = new GameObject[100];
	int count = 0;
	
	public void AddItem(GameObject go){
		data [count] = go;
		count ++;
	}

	public GameObject GetItemAtIndex(int index){
		return data [index];
	}

	public int GetCount(){
		return count;
	}

}
