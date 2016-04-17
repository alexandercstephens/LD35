using UnityEngine;
using System.Collections;

public class TriggerFloorClose : MonoBehaviour {

	public GameObject TopFloor;
	public GameObject BottomFloor;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter( Collider collider ) {

		if (collider.tag == "Player") {
			TopFloor.GetComponent<ObstacleController> ().Active = true;
			BottomFloor.GetComponent<ObstacleController> ().Active = true;
		}

	}
}
