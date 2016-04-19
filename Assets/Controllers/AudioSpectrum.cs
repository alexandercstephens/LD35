using UnityEngine;
using System.Collections;

public class AudioSpectrum : MonoBehaviour
{
    private AudioSource[] audioSources;
    //A float array that stores the audio samples  
    public float[] samples = new float[64];

    // Use this for initialization
    void Start ()
    {
        //Get and store a reference to the following attached components:  
        //AudioSource  
        this.audioSources = GameObject.Find("AudioSources").GetComponentsInChildren<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var audioSource in audioSources)
        {
            if (audioSource.isPlaying)
            {
                //Obtain the samples from the frequency bands of the attached AudioSource  
                audioSource.GetSpectrumData(this.samples, 0, FFTWindow.BlackmanHarris);
            }
        }
    }

    public float[] GetSamples()
    {
        return samples;
    }
}
