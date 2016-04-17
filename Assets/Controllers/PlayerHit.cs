using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHit : MonoBehaviour {

    public Text Score;

    private BeatController beatController;

    private GameObject Spawner;
    private GameObject ScoreValue;

    void Awake()
    {
        Spawner = GameObject.FindGameObjectWithTag("Spawner");
        beatController = GameObject.Find("BeatController").GetComponent<BeatController>();
        //ScoreValue = GameObject.FindGameObjectWithTag("Score");
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "HurtsPlayer")
        {
            //Debug.Log("You're dead");
            //beatController.RestartLevel();
            //Destroy(collider.gameObject);
            //Application.LoadLevel(Application.loadedLevel);
        }
        if (collider.tag == "CheckPoint")
        {
            collider.GetComponent<CheckPoint>().GetCheckPoint();
            Spawner.GetComponent<SpawnPoint>().SetSpawn(collider.transform);
            //var score = Score.text.Replace("Score:", "");
            //GameObject.FindGameObjectWithTag("HudCanvas").GetComponent<HUD>().SetScoreValue(float.Parse(score));
           
        }
        //    Debug.Log("You're dead");
        //    Destroy(collider.gameObject);
        //    
        //    return;
        //}
        //    if (collider.tag == "CheckPoint")
        //    {
        //        collider.GetComponent<CheckPoint>().GetCheckPoint();
        //        Spawner.GetComponent<SpawnPoint>().SetSpawn(collider.transform);
        //    }
        //}
    }
}
