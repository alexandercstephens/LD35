using UnityEngine;
using System.Collections;

public class ParallaxController : MonoBehaviour {
    [System.Serializable]
    public class Background
    {
        public GameObject background;
        public int size;
    }

    public float parallaxSpeed = 1f;
    public Background[] backgrounds;

    void Awake ()
    {
        foreach (var background in backgrounds)
        {
            var go = Instantiate(background.background);
            var go2 = (GameObject)Instantiate(background.background, background.background.transform.localPosition + new Vector3(0f, 0f, background.size), background.background.transform.localRotation);
            go.transform.parent = transform;
            go2.transform.parent = transform;
        }
    }

	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.back * parallaxSpeed * Time.deltaTime);
	}
}
