using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public float movementSpeed = 1f;

	// Update is called once per frame
	void Update () {
		this.transform.Translate(0f, 0f, -Time.deltaTime * movementSpeed);

		if (this.transform.position.z < -25.0f)
			Destroy (this.gameObject);

	}
}
 