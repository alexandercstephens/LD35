using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {
    private BeatController beatController;

    void Start()
    {
        beatController = GameObject.Find("BeatController").GetComponent<BeatController>();
    }

	// Use this for initialization
	public void EndTheGame()
    {
        beatController.StartTheEnd();
    }
}
