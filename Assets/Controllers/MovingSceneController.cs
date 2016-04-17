using UnityEngine;
using System.Collections;

public class MovingSceneController : MonoBehaviour {
    public float movementSpeed = 1f;
	public float startOffset = 0.0f;

	void Start() {
		this.transform.Translate (0.0f, 0.0f, -startOffset);
	}

	// Update is called once per frame
	void Update () {
        this.transform.Translate(0f, 0f, -Time.deltaTime * movementSpeed);
	}
}
