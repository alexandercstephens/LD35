using UnityEngine;
using System.Collections;

public class SpearheadController : MonoBehaviour
{
	private CameraController cameraController;
	private PlayerController playerController;
	private BoxCollider boxCollider;

	private LayerMask enemies;

	private bool isExpanding = false;

	private Vector3 originalPosition;
	private Vector3 originalScale;

	// Use this for initialization
	public void Spear ()
	{
		RaycastHit hit;
		//TODO make max length based on the end of the screen
		if (Physics.BoxCast (transform.position, boxCollider.bounds.extents, Vector3.forward, out hit, Quaternion.identity, 20f, enemies)) {
			var distanceToEnemy = (hit.point - (transform.position - boxCollider.bounds.extents)).z;

			transform.localPosition = new Vector3 (0f, 0f, distanceToEnemy * 0.5f + 0.5f);
			transform.localScale = new Vector3 (1f, 1f, distanceToEnemy);

			Destroy (hit.collider.gameObject);
			var fracturedObject = hit.collider.gameObject.GetComponent<FracturedObject> ();
			if (fracturedObject != null) {
				fracturedObject.Explode (hit.point, 15f + Random.value * 30f);
				cameraController.addShake (0.44444444444f); //the length of one beat of the song
			}
		} else {
			//TODO make max length based on the end of the screen
			transform.localPosition = new Vector3 (0f, 0f, 5f + 0.5f);
			transform.localScale = new Vector3 (1f, 1f, 10f);
		}
	}

	void Awake ()
	{
		cameraController = GameObject.Find ("CameraManager").GetComponent<CameraController> ();
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		boxCollider = GetComponent<BoxCollider> ();

		enemies = LayerMask.GetMask ("Enemies");

		originalPosition = transform.localPosition;
		originalScale = transform.localScale;
	}

	void Update ()
	{
		transform.localPosition = Vector3.Lerp (transform.localPosition, originalPosition, 0.1f);
		transform.localScale = Vector3.Lerp (transform.localScale, originalScale, 0.1f);
	}
}
