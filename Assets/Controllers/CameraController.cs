using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public float slerpSpeed = 0.1f;

    private Camera cam;
    private Transform topDownPosition;
    private Transform sideViewPosition;

    private bool isTopDown;

	public bool checkIsTopDown() { 
		return isTopDown;
	}

	public bool checkIsSideways() {
		return !isTopDown;
	}

    private float shake = 0f;

    void Awake () {
        cam = GetComponentInChildren<Camera>();
        topDownPosition = transform.Find("TopDownPosition");
        sideViewPosition = transform.Find("SideViewPosition");
    }

    void Start ()
    {
        cam.transform.LookAt(Vector3.zero, Vector3.up);
    }

    void Update ()
    {
        if (isTopDown)
        {
            cam.transform.localPosition = Vector3.Slerp(cam.transform.localPosition, topDownPosition.localPosition, slerpSpeed);
            cam.transform.LookAt(new Vector3(0f, 0f, cam.transform.position.z), Vector3.Slerp(cam.transform.up, Vector3.forward, slerpSpeed));
        } else
        {
            cam.transform.localPosition = Vector3.Slerp(cam.transform.localPosition, sideViewPosition.localPosition, slerpSpeed);
            cam.transform.LookAt(new Vector3(0f, 0f, cam.transform.position.z), Vector3.Slerp(cam.transform.up, Vector3.up, slerpSpeed));
        }

        if (shake > 0)
        {
            cam.transform.localPosition += Random.insideUnitSphere * shake;
            shake -= Time.deltaTime;
        }
    }

    public void addShake (float amount)
    {
        shake += amount;
    }

    public void killShake ()
    {
        shake = 0;
    }
	
	public void SetTopDownView ()
    {
        isTopDown = true;
    }

    public void SetSideView ()
    {
        isTopDown = false;
    }
}
