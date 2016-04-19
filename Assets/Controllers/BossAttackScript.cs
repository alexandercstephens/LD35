using UnityEngine;
using System.Collections;

public class BossAttackScript : MonoBehaviour {
    private Vector3 goalPosition;
    private Vector3 velocity;

    // Use this for initialization
    void Start () {
        goalPosition = transform.localPosition + new Vector3(0f, 0f, -25f);
        Move();
    }
	
	// Update is called once per frame
	void Update () {
        transform.localPosition += velocity * Time.deltaTime;
        transform.Rotate(0, 20 * Time.deltaTime, 20 * Time.deltaTime);
    }

    private void Move()
    {
        velocity = (goalPosition - transform.localPosition) / 14.222222222222222222f;
        Invoke("StopMove", 14.222222222222222222f);
    }

    private void StopMove()
    {
        velocity = Vector3.zero;
        transform.localPosition = goalPosition;
    }
}
