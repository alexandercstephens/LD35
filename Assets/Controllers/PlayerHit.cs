using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHit : MonoBehaviour
{

	public Text Score;
	public ParallaxController[] parallaxes;
	public Canvas screenFlash;
	public Image screenFlashPanel;
	public bool canDie = true;

	private BeatController beatController;
    
	//private GameObject ScoreValue;

	void Awake ()
	{
		beatController = GameObject.Find ("BeatController").GetComponent<BeatController> ();
		//ScoreValue = GameObject.FindGameObjectWithTag("Score");
	}

	void OnTriggerEnter (Collider collider)
	{
		if (collider.tag == "HurtsPlayer" && canDie) {
			//Debug.Log("You're dead");
			beatController.RestartLevel ();
			foreach (var parallax in parallaxes) {
				parallax.ShiftHalf ();
			}
			screenFlash.enabled = true;
			StartCoroutine ("Transparify");
			transform.parent.localPosition = new Vector3 (0f, 0f, -3f);
			//Destroy(collider.gameObject);
			//Application.LoadLevel(Application.loadedLevel);
		}
		if (collider.tag == "CheckPoint") {
			collider.GetComponent<CheckPoint> ().GetCheckPoint ();
			//var score = Score.text.Replace("Score:", "");
			//GameObject.FindGameObjectWithTag("HudCanvas").GetComponent<HUD>().SetScoreValue(float.Parse(score));
           
		}
	}

	IEnumerator Transparify ()
	{
		for (var t = 1f; t >= 0f; t -= 0.1f) {
			screenFlashPanel.color = new Color (1f, 1f, 1f, t);
			yield return new WaitForSeconds (0.05555555f);
		}
		screenFlash.enabled = false;
	}
}
