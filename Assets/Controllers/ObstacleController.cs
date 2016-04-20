using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour
{

	public bool Active = true;
	public bool BackAndForth = false;

	public Vector3 DestinationOffset = new Vector3 (0, 0, 0);

	public float BackAndForthPause = 0.0f;
	public float RotateXSpeed = 0.0f;
	public float RotateYSpeed = 0.0f;
	public float RotateZSpeed = 0.0f;

	void Start ()
	{
		if (BackAndForth) {
			InvokeRepeating ("MoveBackAndForth", 0f, BackAndForthPause);
		}
	}

	private void MoveBackAndForth ()
	{
		this.transform.localPosition += DestinationOffset;
		DestinationOffset *= -1;
	}

	void Update ()
	{
		transform.Rotate (new Vector3 (RotateXSpeed, RotateYSpeed, RotateZSpeed) * Time.deltaTime);
	}

}
