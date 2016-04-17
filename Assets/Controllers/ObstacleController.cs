using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

    public float RotateXSpeed = 0.0f;
    public float RotateYSpeed = 0.0f;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3( RotateXSpeed, RotateYSpeed, 0) * Time.deltaTime);
    }

}
