using UnityEngine;
using System.Collections;

public class RandomColorAudioVisualizer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var rend = this.GetComponent<Renderer>();
        rend.material.color = new Color(Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), Random.Range(0.0f, 1f));
        InvokeRepeating("ColorChange", 0, 5);
    }

    void ColorChange()
    {
        var rend = this.GetComponent<Renderer>();
        rend.material.color = new Color(Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), Random.Range(0.0f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
