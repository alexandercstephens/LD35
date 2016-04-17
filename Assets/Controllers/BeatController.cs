using UnityEngine;
using System.Collections;

public class BeatController : MonoBehaviour
{
	private AudioSource aSource;
	private float lastTime;
	private int beatNumber = 1;

	public SpearheadController spear;

	// Use this for initialization
	void Awake ()
	{
		this.aSource = GameObject.Find ("Audio Source").GetComponent<AudioSource> ();
	}

	void Start ()
	{
		lastTime = 0f;//Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//TODO write this in a way that it won't get off sync,
		//should suffice for one level though
		if (aSource.time - lastTime > 0.44444444444f) {
			lastTime = lastTime + 0.44444444444f;
			beatNumber += 1;
			if (beatNumber == 5) {
				spear.Spear ();
				beatNumber = 1;
			}
		}
	}
}
