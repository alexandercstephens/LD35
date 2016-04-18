using UnityEngine;
using System.Collections;

public class TestExplode : MonoBehaviour
{
	private FracturedObject fo;

	// Use this for initialization
	void Awake ()
	{
		fo = GetComponent<FracturedObject> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if (Input.GetKeyDown(KeyCode.M))
		//{
		//    fo.Explode(Vector3.zero, 10f);
		//}
	}
}
