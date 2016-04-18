using UnityEngine;
using System.Collections;

public class MiniBossShot : MonoBehaviour
{
	public bool atPlayer = false;

	private Vector3 goalPosition;
	private Vector3 velocity = Vector3.zero;

	public void MoveTo (Vector3 pos, float time)
	{
		goalPosition = pos;
		Move (time);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position += velocity * Time.deltaTime;
	}

	public void Fire (float waitTime)
	{
		if (waitTime < 0.01f)
			ActuallyFire ();
		else
			Invoke ("ActuallyFire", waitTime);
	}

	private void ActuallyFire ()
	{
		CancelInvoke ();
		if (atPlayer) {
			var direction = GameObject.Find ("Player").transform.position - transform.position;
			MoveTo (transform.position + direction.normalized * 60f, 1.777777777777f);
		} else {
			MoveTo (transform.position + Vector3.back * 60f, 1.777777777777f);
		}
		Invoke ("Destroy", 1.77777777777f);
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