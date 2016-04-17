using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpawn(Transform position)
    {
        this.transform.position = position.localPosition;
    }
}
