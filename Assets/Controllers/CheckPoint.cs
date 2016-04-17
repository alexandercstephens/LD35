using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour
{
    public GameObject[] visualizers;
    // Use this for initialization
    void Start()
    {
        visualizers = GameObject.FindGameObjectsWithTag("AudioCube");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetCheckPoint()
    {
        foreach(var v in visualizers)
        {
            v.GetComponent<RandomColorAudioVisualizer>().CheckPointChange(this.name);
        }
    }
}
