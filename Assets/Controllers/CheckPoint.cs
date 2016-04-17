using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour
{
    public GameObject nextScene;

    private GameObject currentLevel;
    private BeatController beatController;
    //private GameObject[] visualizers;
    // Use this for initialization
    void Awake()
    {
        beatController = GameObject.Find("BeatController").GetComponent<BeatController>();
        //visualizers = GameObject.FindGameObjectsWithTag("AudioCube");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            beatController.StartLevel(nextScene);
            //Destroy(collider.gameObject);
            //Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void GetCheckPoint()
    {
        //this.gameObject.SetActive(false);
        //foreach (var v in visualizers) 
        //{
        //    v.GetComponent<RandomColorAudioVisualizer>().CheckPointChange(this.name);
        //}
    }
}
