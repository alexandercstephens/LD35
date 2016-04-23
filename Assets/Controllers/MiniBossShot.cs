using UnityEngine;
using System.Collections;

public class MiniBossShot : MonoBehaviour
{
	private bool atPlayer = false;
	private Vector3 goalPosition;
	private Vector3 velocity = Vector3.zero;

	public float fireRate = 1.777777777777f;
	public Material atPlayerMaterial;

	private Transform playerTransform;
	private PlayerController playerController;

	private bool isFired = false;

	public void MoveTo (Vector3 pos, float time)
	{
		goalPosition = pos;
		Move (time);
	}

	void Awake ()
	{
		var player = GameObject.Find ("Player");
		playerTransform = player.transform;
		playerController = player.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position += velocity * Time.deltaTime;
		if (atPlayer && !isFired) {
			if (playerController.GetIsTopDown ()) {
				transform.LookAt (2 * transform.position - new Vector3 (playerTransform.position.x, transform.position.y, playerTransform.position.z));
			} else {
				transform.LookAt (2 * transform.position - new Vector3 (transform.position.x, playerTransform.position.y, playerTransform.position.z));
			}
		}
	}

	public void Fire (float waitTime)
	{
		if (waitTime < 0.01f)
			ActuallyFire ();
		else
			Invoke ("ActuallyFire", waitTime);
	}

	public void FireAtPlayer ()
	{
		atPlayer = true;
		foreach (var renderer in GetComponentsInChildren<Renderer>()) {
			renderer.material = atPlayerMaterial;
		}
	}

	private void ActuallyFire ()
	{
		isFired = true;
		CancelInvoke ();
		if (atPlayer) {
			var direction = playerTransform.position - transform.position;
			MoveTo (transform.position + direction.normalized * 60f, fireRate);
		} else {
			MoveTo (transform.position + Vector3.back * 60f, fireRate);
		}
		Invoke ("Destroy", fireRate);
	}

	private void Move (float time)
	{
		velocity = (goalPosition - transform.position) / time;
		Invoke ("StopMove", time);
	}

	private void StopMove ()
	{
		velocity = Vector3.zero;
		transform.position = goalPosition;
	}

	private void Destroy ()
	{
		Destroy (this.gameObject);
	}
}