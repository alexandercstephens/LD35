using UnityEngine;
using System.Collections;

public class MovingSceneController : MonoBehaviour {
    public float movementSpeed = 1f;
	public float startOffset = 0.0f;
    private GameObject Spawner;

    void Awake()
    {
        Spawner = GameObject.FindGameObjectWithTag("Spawner");
       
    }

	void Start() {
        
        this.transform.position = new Vector3(0,0, Spawner.transform.position.z*-1);
        //this.transform.Translate (0.0f, 0.0f, this.transform.position.z);
        //Instantiate(MovingScene, new Vector3(0, 0, -Spawner.transform.position.z), Quaternion.identity);
	}

	// Update is called once per frame
	void Update () {
        this.transform.Translate(0f, 0f, -Time.deltaTime * movementSpeed);
	}
}
