using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioVisualizerScaling : MonoBehaviour
{
    private CameraController cameraController;
    private GameObject[] RAV;
    private List<RandomColorAudioVisualizer> RAVScript;

    //An AudioSource object so the music can be played  
    //public AudioSource[] audioSources;
    //A float array that stores the audio samples  
    public float[] samples = new float[64];
    //A renderer that will draw a line at the screen  
    private LineRenderer lRenderer;
    //A reference to the cube prefab  
    public GameObject cube;
    //The transform attached to this game object  
    private Transform goTransform;
    //The position of the current cube. Will also be the position of each point of the line.  
    private Vector3 cubePos;
    //An array that stores the Transforms of all instantiated cubes  
    private Transform[] cubesTransform;
    //The velocity that the cubes will drop
    private Vector3 gravity = new Vector3(0.0f, 0.5f, 0.0f);

    private AudioSpectrum audioSpectrum;

    void Awake()
    {
        //Get and store a reference to the following attached components:  
        //AudioSource  
        //this.audioSources = GameObject.Find("AudioSources").GetComponentsInChildren<AudioSource>();
        //LineRenderer  
        this.lRenderer = GetComponent<LineRenderer>();
        //Transform  
        this.goTransform = GetComponent<Transform>();
        //The line should have the same number of points as the number of samples  
        lRenderer.SetVertexCount(samples.Length);
        //The cubesTransform array should be initialized with the same length as the samples array  
        cubesTransform = new Transform[samples.Length];
        //Center the audio visualization line at the X axis, according to the samples array length  
        goTransform.position = new Vector3(goTransform.position.x, goTransform.position.y, goTransform.position.z);
        // goTransform.LookAt(Camera.main.transform);

        //Create a temporary GameObject, that will serve as a reference to the most recent cloned cube  
        GameObject tempCube;

        //For each sample  
        for (int i = 0; i < samples.Length; i++)
        {
            //Instantiate a cube placing it at the right side of the previous one  
            tempCube = (GameObject)Instantiate(cube);
            //Get the recently instantiated cube Transform component  
            cubesTransform[i] = tempCube.GetComponent<Transform>();
            //Make the cube a child of this game object  
            cubesTransform[i].parent = goTransform;
            cubesTransform[i].transform.localPosition = new Vector3(i, 0f, 0f);
            cubesTransform[i].transform.localRotation = Quaternion.identity;
            cubesTransform[i].transform.localScale = Vector3.one;
        }
    }

    void Start()
    {
        audioSpectrum = GameObject.Find("AudioSpectrum").GetComponent<AudioSpectrum>();

    }

    void Update()
    {
        //foreach (var audioSource in audioSources)
        //{
        //    if (audioSource.isPlaying)
        //    {
        //        //Obtain the samples from the frequency bands of the attached AudioSource  
        //        audioSource.GetSpectrumData(this.samples, 0, FFTWindow.BlackmanHarris);
        //    }
        //}
        //For each sample  
        //this.transform.LookAt(Camera.main.transform.position);
        this.samples = audioSpectrum.GetSamples();

        for (int i = 0; i < samples.Length; i++)
        {
            /*Set the cubePos Vector3 to the same value as the position of the corresponding 
             * cube. However, set it's Y element according to the current sample.*/
            cubePos.Set(cubesTransform[i].localScale.x, Mathf.Clamp(samples[i] * (100 + i * 100), 0.1f, 500), cubesTransform[i].localScale.z);

            //If the new cubePos.y is greater than the current cube position

            if (cubePos.y >= cubesTransform[i].localScale.y)
            {
                //Set the cube to the new Y position  
                cubesTransform[i].localScale = cubePos;
            }
            else
            {
                //The spectrum line is below the cube, make it fall  
                cubesTransform[i].localScale -= gravity;
            }

            /*Set the position of each vertex of the line based on the cube position. 
             * Since this method only takes absolute World space positions, it has 
             * been subtracted by the current game object position.*/
            lRenderer.SetPosition(i, cubePos - goTransform.position);
        }
    }
}
