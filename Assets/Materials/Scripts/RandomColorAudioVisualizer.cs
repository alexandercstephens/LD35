using UnityEngine;
using System.Collections;

public class RandomColorAudioVisualizer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var rend = this.GetComponent<Renderer>();
        rend.material.color = new Color(Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), Random.Range(0.0f, 1f));
        //InvokeRepeating("ColorChange", 0, 5);
    }

    void ColorChange()
    {
        var rend = this.GetComponent<Renderer>();
        rend.material.color = new Color(Random.Range(0.0f, 0f), Random.Range(0.0f, 0.7f), Random.Range(0.0f, 0f));
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            var rend = this.GetComponent<Renderer>();
            rend.material.color = new Color(Random.Range(0.2f, 1f), 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            var rend = this.GetComponent<Renderer>();
            rend.material.color = new Color(0, 0, Random.Range(0.2f, 1f));
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            var rend = this.GetComponent<Renderer>();
            rend.material.color = new Color(0, Random.Range(0.2f, 1f), 0);
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            var rend = this.GetComponent<Renderer>();
            rend.material.color = new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), 0);
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            var rend = this.GetComponent<Renderer>();
            rend.material.color = new Color(Random.Range(0.2f, 1f), 0, Random.Range(0.2f, 1f));
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            var rend = this.GetComponent<Renderer>();
            rend.material.color = new Color(0, Random.Range(0.2f, 1f), Random.Range(0.2f, 1f));
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            var rend = this.GetComponent<Renderer>();
            rend.material.color = new Color(Random.Range(0.2f, 1f), Random.Range(0.2f, 1f), Random.Range(0.2f, 1f));
        }

    }
}
