using UnityEngine;
using System.Collections;

public class ParallaxController : MonoBehaviour {
    public GameObject background;
    public float speed;
    public float size;

    private GameObject o1;
    private GameObject o2;

    void Awake ()
    {
            o1 = Instantiate(background);
            o2 = (GameObject)Instantiate(background, background.transform.localPosition + new Vector3(0f, 0f, size), background.transform.localRotation);
            o1.transform.parent = transform;
            o2.transform.parent = transform;
    }

	// Update is called once per frame
	void Update () {
        o1.transform.localPosition -= new Vector3(0f, 0f, 1f) * speed * Time.deltaTime;
        o2.transform.localPosition -= new Vector3(0f, 0f, 1f) * speed * Time.deltaTime;

        if (o2.transform.localPosition.z < 0)
        {
            o1.transform.localPosition += new Vector3(0f, 0f, size * 2);
            var t = o2;
            o2 = o1;
            o1 = t;
        }
    }

    public void ShiftHalf ()
    {
        o1.transform.localPosition -= new Vector3(0f, 0f, 1f) * size * 0.5f;
        o2.transform.localPosition -= new Vector3(0f, 0f, 1f) * size * 0.5f;

        if (o2.transform.localPosition.z < 0)
        {
            o1.transform.localPosition += new Vector3(0f, 0f, size * 2);
            var t = o2;
            o2 = o1;
            o1 = t;
        }
    }
}
