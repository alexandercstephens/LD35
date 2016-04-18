using UnityEngine;
using System.Collections;

public class ParallaxSpeedController : MonoBehaviour {

	private float startParallaxSpeed = 0.0f;
	private float startSceneSpeed = 0.0f;

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
			GameObject scroller = (GameObject)GameObject.Find ("RainbowSideScroller");
			GameObject movingScene = (GameObject)GameObject.Find ("MovingScene");

			startTime = Time.time;
			startParallaxSpeed = scroller.GetComponent<ParallaxController> ().speed;
			//startSceneSpeed = movingScene.GetComponent<MovingSceneController> ().movementSpeed;
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
            GameObject bScroller = (GameObject)GameObject.Find("RainbowBottomScroller");
            GameObject movingScene = (GameObject)GameObject.FindWithTag("Checkpoint");

            bScroller.GetComponent<ParallaxController>().speed = GetNewSpeed(startParallaxSpeed, desiredParallaxSpeed);
			scroller.GetComponent<ParallaxController> ().speed = GetNewSpeed (startParallaxSpeed, desiredParallaxSpeed);
			//movingScene.GetComponent<MovingSceneController> ().movementSpeed = GetNewSpeed (startSceneSpeed, desiredSceneSpeed);

		}

	}
}
