using UnityEngine;
using System.Collections;

public class ParallaxSpeedController : MonoBehaviour {

	public float desiredParallaxSpeed = 100.0f;
	public float desiredSceneSpeed = 15.0f;
	public float time=1.0f;

	private float startTime=0.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter( Collider collider ) {

		if (collider.tag == "Player") {
			startTime = Time.time;
		}
	}

	float GetNewSpeed( float spd, float dspd ) {
		float timeElapsed = ( Time.time - startTime ) / time;
		return Mathf.Lerp ( spd, dspd, timeElapsed);
	}

	void OnTriggerStay( Collider collider ) {
		

		if (collider.tag == "Player") {

			GameObject obj = ( GameObject )collider.gameObject;
			GameObject scroller = (GameObject)GameObject.Find ("RainbowSideScroller");
			GameObject movingScene = (GameObject)GameObject.Find ("MovingScene");

			scroller.GetComponent<ParallaxController> ().speed = GetNewSpeed (scroller.GetComponent<ParallaxController> ().speed, desiredParallaxSpeed);
			movingScene.GetComponent<MovingSceneController> ().movementSpeed = GetNewSpeed (movingScene.GetComponent<MovingSceneController>().movementSpeed, desiredSceneSpeed);

		}

	}
}
