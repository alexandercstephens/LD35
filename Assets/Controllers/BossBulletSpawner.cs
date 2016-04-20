using UnityEngine;
using System.Collections;

public class BossBulletSpawner : MonoBehaviour {
    public GameObject bossShot;
    private BeatController beatController;
    private MeshRenderer meshRenderer;

	// Use this for initialization
	void Start () {
        beatController = GameObject.Find("BeatController").GetComponent<BeatController>();
        meshRenderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (beatController.IsOnBeat())
        {
            if (Random.value < 0.25f)
            {
                return;
            }

            meshRenderer.material.color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0f, 1f, 1f, 1f);

            var shot = (GameObject)Instantiate(bossShot, transform.position, transform.rotation);
            shot.transform.parent = transform.parent.parent;

            var shotComponent = shot.GetComponent<MiniBossShot>();

            //if (i == 0)
            //    shotComponent.MoveTo(new Vector3(10f, 10f, 10f), 0.444444444f);
            //else if (i == 1)
            //    shotComponent.MoveTo(new Vector3(5f, 5f, 10f), 0.444444444f);
            //else if (i == 2)
            //    shotComponent.MoveTo(new Vector3(-5f, -5f, 10f), 0.444444444f);
            //else if (i == 3)
            //    shotComponent.MoveTo(new Vector3(-10f, -10f, 10f), 0.444444444f);
            //
            //if (i == randomAtPlayer)
            //    shotComponent.FireAtPlayer();

            if (Random.value < 0.2f)
            {
                shotComponent.FireAtPlayer();
            }

            shotComponent.fireRate = 7.11111111f;
            shotComponent.Fire(Random.Range(0, 4) * 0.444444444f);
            //yield return new WaitForSeconds(0.444444444f);
        }
	}
}
