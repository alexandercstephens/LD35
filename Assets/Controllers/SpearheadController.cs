using UnityEngine;
using System.Collections;

public class SpearheadController : MonoBehaviour
{
	public AudioSource hitSound;
    public PlayerHit hitbox;

	private CameraController cameraController;
	private PlayerController playerController;
	private BoxCollider boxCollider;
	private MeshRenderer meshRenderer;
	private MeshRenderer playerRenderer;
    
	private LayerMask hittableThings;

	private Vector3 originalPosition;
	private Vector3 originalScale;

	private float timeHit = 0f;

    public float screenSize = 13f;

	// Use this for initialization
	public void Spear ()
	{
		var distanceToScreenEnd = screenSize - transform.position.z;

		RaycastHit hit;
		//TODO make max length based on the end of the screen
		if (Physics.BoxCast (transform.position, boxCollider.bounds.extents, Vector3.forward, out hit, Quaternion.identity, distanceToScreenEnd + 3f, hittableThings)) {
			var distanceToEnemy = (hit.point - (transform.position - boxCollider.bounds.extents)).z;

			transform.localPosition = new Vector3 (0f, 0f, distanceToEnemy * 0.5f + 0.5f);
			transform.localScale = new Vector3 (1f, 1f, distanceToEnemy);

			if (hit.collider.gameObject.layer == LayerMask.NameToLayer ("Enemies")) {
                var maybeEndTheGame = hit.collider.gameObject.GetComponent<EndGame>();
                if (maybeEndTheGame != null)
                {
                    maybeEndTheGame.EndTheGame();
                    hitbox.canDie = false;
                }
                hitSound.Play ();
				Destroy (hit.collider.gameObject);
				var fracturedObject = hit.collider.gameObject.GetComponent<FracturedObject> ();
				if (fracturedObject != null) {
					fracturedObject.Explode (hit.point, 15f + Random.value * 30f);
					cameraController.addShake (0.44444444444f); //the length of one beat of the song
				}
			}

			timeHit = Time.time;
		} else {
			//TODO make max length based on the end of the screen
			transform.localPosition = new Vector3 (0f, 0f, distanceToScreenEnd * 0.5f + 0.5f);
			transform.localScale = new Vector3 (1f, 1f, distanceToScreenEnd);
		}

		meshRenderer.material.color = Color.black;
		playerRenderer.material.color = Color.black;
		StartCoroutine ("ResetTexture");
	}

	void Awake ()
	{
		cameraController = GameObject.Find ("CameraManager").GetComponent<CameraController> ();
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		boxCollider = GetComponent<BoxCollider> ();
		meshRenderer = GetComponent<MeshRenderer> ();
		playerRenderer = transform.parent.GetComponent<MeshRenderer> ();

		hittableThings = LayerMask.GetMask ("Enemies");

		originalPosition = transform.localPosition;
		originalScale = transform.localScale;
	}

	void Update ()
	{
		if (Time.time - timeHit > 0.1f) { //let spear be expanded for a moment
			transform.localPosition = Vector3.Lerp (transform.localPosition, originalPosition, 0.1f);
			transform.localScale = Vector3.Lerp (transform.localScale, originalScale, 0.1f);
		}
	}

	private IEnumerator ResetTexture ()
	{
		for (var f = 0f; f <= 1f; f += 0.1f) {
			meshRenderer.material.color = new Color (f, f, f);
			playerRenderer.material.color = new Color (f, f, f);
			yield return new WaitForSeconds (0.02f);
		}        
	}
}
