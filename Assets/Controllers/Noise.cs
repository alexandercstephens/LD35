using UnityEngine;
using System.Collections;

public class Noise : MonoBehaviour
{
	private Renderer r;
	private Renderer[] childRenderers;

	void Awake ()
	{
		r = GetComponent<Renderer> ();
		childRenderers = GetComponentsInChildren<Renderer> ();
	}

	void Update ()
	{
		//TODO randomize the shared material to improve performance
		var randomVec2 = new Vector2 (Random.value, Random.value);
		r.material.SetTextureOffset ("_MainTex", randomVec2);
		foreach (var cr in childRenderers) {
			if (cr != null)
				cr.material.SetTextureOffset ("_MainTex", randomVec2);
		}
	}
}
