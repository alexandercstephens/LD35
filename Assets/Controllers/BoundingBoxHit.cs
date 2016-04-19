using UnityEngine;
using System.Collections;

public class BoundingBoxHit : MonoBehaviour {

	private bool zoomOut = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (zoomOut) {
			Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, 14, Time.deltaTime * 2);
			//transform.parent.transform = new Vector3 (19f ,20f,19f);
		}
	}


	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "BossTrigger") {
			collider.GetComponent<BossTrigger>().StartBossFight ();
			transform.parent.transform.localScale = new Vector3 (19f,19.7059f,18.6f);
            transform.parent.GetComponent<BoxCollider>().center = new Vector3(0f, 0f, 0.08f);
            transform.parent.GetComponent<BoxCollider>().size = new Vector3(1f, 1.4f, 1.7f);
            //GameObject.FindGameObjectWithTag("Boss").GetComponent<BossAttackScript>().StartAttacking();
            zoomOut = true;
		}

	}
}
