using UnityEngine;
using System.Collections;

public class BossAttackScript : MonoBehaviour {

	private GameObject[] BulletSpawners;
	public GameObject Bullet;
	private GameObject Player;

	// Use this for initialization
	void Start () {
		BulletSpawners = GameObject.FindGameObjectsWithTag ("BulletSpawner");
		Player = GameObject.FindGameObjectWithTag ("Player");
	    //InvokeRepeating ("Shoot",Time.deltaTime,2);
	}
	
	// Update is called once per frame
	void Update () {
        //foreach (var b in BulletSpawners) {
        //	var obj = Instantiate (Bullet, b.transform.position, b.transform.rotation) as GameObject;
        //	obj.GetComponent<Rigidbody> ().AddForce (obj.transform.forward * 2);
        //}
        transform.Rotate(0, 20 * Time.deltaTime, 20 * Time.deltaTime);
    }

	void Shoot(){
		foreach (var b in BulletSpawners) {
			var obj = Instantiate (Bullet, new Vector3(b.transform.position.x,b.transform.position.y, b.transform.position.z),b.transform.rotation) as GameObject;
			//b.transform.LookAt (new Vector3(b.transform.parent.transform.position.x, b.transform.parent.transform.position.y, b.transform.parent.transform.position.z));
			obj.GetComponent<Rigidbody> ().AddForce ((Player.transform.position - obj.transform.position).normalized *5);
		}
	}

    public void StartAttacking()
    {
        InvokeRepeating("Shoot", Time.deltaTime, 2);
    }
}
