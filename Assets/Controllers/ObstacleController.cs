using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	public bool Active = true;
	public bool BackAndForth = false;
	private bool getStartPos = false;

	public Vector3 DestinationOffset = new Vector3 (0, 0, 0);
	public float time = 0.0f;

	private Vector3 StartPos;
	private float StartTime { get; set; }
	private float StartSceneZ = 0.0f;

    public float RotateXSpeed = 0.0f;
    public float RotateYSpeed = 0.0f;
	public float RotateZSpeed = 0.0f;

    // Use this for initialization
    void Awake()
    {
		StartPos = this.transform.position;
		StartTime = Time.time;
    }

	void LateUpdate() {

		if (Active) {
			if (DestinationOffset.magnitude > 0) {

				if (!getStartPos) {
					getStartPos = true;
					StartPos = this.transform.position;
					StartSceneZ = GameObject.Find ("MovingScene").transform.position.z;
					StartTime = Time.time;
				}

				Vector3 newPos = StartPos + DestinationOffset;
				newPos.z += (GameObject.Find ("MovingScene").transform.position.z - StartSceneZ);
				this.transform.position = Vector3.Lerp (StartPos, newPos, (Time.time - StartTime) / time);

				if (BackAndForth) {
					getStartPos = false;

				}

			}

		}
	}

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3( RotateXSpeed, RotateYSpeed, RotateZSpeed) * Time.deltaTime);
    }

}
