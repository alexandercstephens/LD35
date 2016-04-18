using UnityEngine;
using System.Collections;

public class RandomColorAudioVisualizer : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		var rend = this.GetComponent<Renderer> ();
		rend.material.color = new Color (Random.Range (0.0f, 1f), Random.Range (0.0f, 1f), Random.Range (0.0f, 1f), 0.2f);
		//InvokeRepeating("ColorChange", 0, 5);
	}

	void ColorChange ()
	{
		var rend = this.GetComponent<Renderer> ();
		rend.material.color = new Color (Random.Range (0.0f, 0f), Random.Range (0.0f, 0.7f), Random.Range (0.0f, 0f), 0.2f);
	}

    public void SetColorRange(float audioMinRed, float audioMaxRed, float audioMinGreen, float audioMaxGreen, float audioMinBlue, float audioMaxBlue)
    {
        var rend = this.GetComponent<Renderer>();
        rend.material.color = new Color(Random.Range(audioMinRed, audioMaxRed), Random.Range(audioMinGreen, audioMaxGreen), Random.Range(audioMinBlue, audioMaxBlue), 0.2f);
    }

    public void CheckPointChange (string color)
	{
        if (color == "Cp1") {
			var rend = this.GetComponent<Renderer> ();
			rend.material.color = new Color (Random.Range (0.2f, 1f), Random.Range (0f, 0.1f), Random.Range (0f, 0.1f), 0.2f);
		}
		if (color == "Cp2") {
			var rend = this.GetComponent<Renderer> ();
			rend.material.color = new Color (Random.Range (0f, 0.1f), Random.Range (0f, 0.1f), Random.Range (0.2f, 1f), 0.2f);
		}
		if (color == "Cp3") {
			var rend = this.GetComponent<Renderer> ();
			rend.material.color = new Color (0, Random.Range (0.2f, 1f), 0, 0.2f);
		}
		if (color == "Cp4") {
			var rend = this.GetComponent<Renderer> ();
			rend.material.color = new Color (Random.Range (0.2f, 1f), Random.Range (0.2f, 1f), 0, 0.2f);
		}
		if (color == "Cp5") {
			var rend = this.GetComponent<Renderer> ();
			rend.material.color = new Color (Random.Range (0.2f, 1f), 0, Random.Range (0.2f, 1f), 0.2f);
		}
		if (color == "Cp6") {
			var rend = this.GetComponent<Renderer> ();
			rend.material.color = new Color (0, Random.Range (0.2f, 1f), Random.Range (0.2f, 1f), 0.2f);
		}
		if (color == "Cp7") {
			var rend = this.GetComponent<Renderer> ();
			rend.material.color = new Color (Random.Range (0.2f, 1f), Random.Range (0.2f, 1f), Random.Range (0.2f, 1f), 0.2f);
		}
	}
}
