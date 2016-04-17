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
    }
}
