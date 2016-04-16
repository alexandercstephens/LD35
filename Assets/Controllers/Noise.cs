using UnityEngine;
using System.Collections;

public class Noise : MonoBehaviour {
    private Renderer r;

    void Awake ()
    {
        r = GetComponent<Renderer>();
    }

	void Update () {
        r.material.SetTextureOffset("_MainTex", new Vector2(Random.value, Random.value));
    }
}
