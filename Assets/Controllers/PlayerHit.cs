using UnityEngine;
using System.Collections;

public class PlayerHit : MonoBehaviour {
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "HurtsPlayer")
        {
            Debug.Log("You're dead");
            Destroy(collider.gameObject);
        }
        if (collider.tag == "CheckPoint")
        {
            collider.GetComponent<CheckPoint>().GetCheckPoint();
        }
        //    Debug.Log("You're dead");
        //    Destroy(collider.gameObject);
        //    Application.LoadLevel(Application.loadedLevel);
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
