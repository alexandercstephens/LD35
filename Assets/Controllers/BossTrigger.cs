using UnityEngine;
using System.Collections;

public class BossTrigger : MonoBehaviour {

	public bool slow = false;

	private float speed = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (slow) {
			var moveingSceneScript = GetComponentInParent<MovingSceneController> ();
			//if (moveingSceneScript.movementSpeed > 0) {
			//	moveingSceneScript.movementSpeed -= 0.05f;
			//} else {
				moveingSceneScript.movementSpeed = 0;
				//Camera.main.orthographicSize = 23;
			//}
			//if (moveingSceneScript.movementSpeed == 0) {
				slow = false;
			//}
		}
	}


	public void StartBossFight(){
		slow = true;
	}
}
