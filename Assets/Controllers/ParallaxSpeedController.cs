using UnityEngine;
using System.Collections;

public class ParallaxSpeedController : MonoBehaviour {

	public float desiredSpeed = 100.0f;
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

	void OnTriggerStay( Collider collider ) {
		

		if (collider.tag == "Player") {

			GameObject obj = ( GameObject )collider.gameObject;
			GameObject scroller = (GameObject)GameObject.Find ("RainbowSideScroller");

			float curSpeed = scroller.GetComponent<ParallaxController> ().speed;
			float timeElapsed = ( Time.time - startTime ) / time;

			float newSpeed = Mathf.Lerp (curSpeed, desiredSpeed, timeElapsed);
			scroller.GetComponent<ParallaxController> ().speed = newSpeed;
			Debug.Log (newSpeed);
		}

	}
}
