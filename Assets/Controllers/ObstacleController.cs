using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour
{
	public bool Active = true;
	public bool BackAndForth = false;

	public Vector3 DestinationOffset = new Vector3 (0, 0, 0);
    public float timeToMove = 0f;
    public float timeToStartMoving = 0f;

	public float BackAndForthPause = 0.0f;
	public float RotateXSpeed = 0.0f;
	public float RotateYSpeed = 0.0f;
	public float RotateZSpeed = 0.0f;

    private Vector3 startLocalPosition;
    private float startTime;

	void Start ()
	{
		if (BackAndForth) {
			InvokeRepeating ("MoveBackAndForth", 0f, BackAndForthPause);
		}
        startLocalPosition = transform.localPosition;
        startTime = Time.time;
    }

	private void MoveBackAndForth ()
	{
		this.transform.localPosition += DestinationOffset;
		DestinationOffset *= -1;
	}

	void Update ()
	{
		transform.Rotate (new Vector3 (RotateXSpeed, RotateYSpeed, RotateZSpeed) * Time.deltaTime);
        if (timeToMove > 0f && Active && !BackAndForth)
        {
            transform.localPosition = Vector3.Lerp(startLocalPosition, startLocalPosition + DestinationOffset, (Time.time - startTime - timeToStartMoving) / timeToMove);
        }
	}

}
