using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BeatController : MonoBehaviour
{
	public GameObject audioSources;
	public AudioSource endMusic;
	public CameraController cameraController;
	public Canvas startScreen;
	public Text startText;
	public Canvas endScreen;
	public Text endText;

	public GameObject[] levels;

	private GameObject currentLevel;

	private AudioSource currentSource;
	private float lastBeat;
	private float clipLength;
	private int beatNumber = 1;

	private string state;
	private AudioSource newSource;
	private GameObject newScene;
	public int nextLevelNumber = 0;
	private bool waitingOnLevelStart = false;

	private bool canPlayerAttack = false;
	private bool hasShownPlayerAttackMessage = false;
	public Canvas attackMessage;
	public Text attackText;

	public Canvas shiftMessage;
	public Text shiftText;

	public Canvas checkpointCanvas;
	public Text checkpointText;

	public SpearheadController spear;

	private int beatFrames = 0;

	void Start ()
	{
		lastBeat = 0f;
		currentSource = null;
		state = "start";
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if (Input.GetKeyDown(KeyCode.Alpha0))
		//{
		//    nextLevelNumber = 0;
		//    StartLevel(levels [nextLevelNumber]);
		//}
		//else if (Input.GetKeyDown(KeyCode.Alpha1))
		//{
		//    nextLevelNumber = 1;
		//    StartLevel(levels[nextLevelNumber]);
		//}
		//else if (Input.GetKeyDown(KeyCode.Alpha2))
		//{
		//    nextLevelNumber = 2;
		//    StartLevel(levels[nextLevelNumber]);
		//}
		//else if (Input.GetKeyDown(KeyCode.Alpha3))
		//{
		//    nextLevelNumber = 3;
		//    StartLevel(levels[nextLevelNumber]);
		//}
		//else if (Input.GetKeyDown(KeyCode.Alpha4))
		//{
		//    nextLevelNumber = 4;
		//    StartLevel(levels[nextLevelNumber]);
		//}
		//else if (Input.GetKeyDown(KeyCode.Alpha5))
		//{
		//    nextLevelNumber = 5;
		//    StartLevel(levels[nextLevelNumber]);
		//}
		//else if (Input.GetKeyDown(KeyCode.Alpha6))
		//{
		//    nextLevelNumber = 6;
		//    StartLevel(levels[nextLevelNumber]);
		//}
		//else if (Input.GetKeyDown(KeyCode.Alpha7))
		//{
		//    nextLevelNumber = 7;
		//    StartLevel(levels[nextLevelNumber]);
		//}
		//else if (Input.GetKeyDown(KeyCode.Alpha8))
		//{
		//    nextLevelNumber = 8;
		//    StartLevel(levels[nextLevelNumber]);
		//}
		//else if (Input.GetKeyDown(KeyCode.Alpha9))
		//{
		//    nextLevelNumber = 9;
		//    StartLevel(levels[nextLevelNumber]);
		//}

		if (beatFrames != 0) {
			beatFrames -= 1;
		}
		switch (state) {
		case "start":
			if (Math.Abs (Input.GetAxis ("Horizontal")) > 0.01f || Math.Abs (Input.GetAxis ("Vertical")) > 0.01f) {
				state = null;
				Invoke ("TurnUp", 0.5f);
				StartLevel (levels [nextLevelNumber]);
			}
			break;
		}

		if (currentSource != null) {
			if (!currentSource.loop && nextLevelNumber < levels.Length && currentSource.clip.length - currentSource.time < 0.5f) {
				StartLevel (levels [nextLevelNumber]);
			}

			if (currentSource.time < lastBeat) {
				lastBeat = 0f;
				beatNumber += 1;
				if (beatNumber == 5) {
					if (canPlayerAttack)
						spear.Spear ();
					beatNumber = 1;
					beatFrames = 1; //change to 2 if necessary
				}
			} else if (currentSource.time - lastBeat > 0.44444444444f) {
				lastBeat = lastBeat + 0.44444444444f;
				beatNumber += 1;
				if (beatNumber == 5) {
					if (canPlayerAttack)
						spear.Spear ();
					beatNumber = 1;
					beatFrames = 1; //change to 2 if necessary
				}
			}
		}        
	}

	public void StartTheEnd ()
	{
		currentSource.loop = false;
		var numBeatsLeft = 0;
		var timeLeft = currentSource.clip.length - currentSource.time;
		while (timeLeft > 0) {
			timeLeft -= 0.44444444444f;
			if (timeLeft > 0)
				numBeatsLeft += 1;
		}
		InvokeRepeating ("EndShake", currentSource.clip.length - currentSource.time - 0.44444444444f * numBeatsLeft, 0.4444444444f);
		Invoke ("EndGame", currentSource.clip.length - currentSource.time + 21.3333333333f);
	}

	public bool IsOnBeat ()
	{
		return beatFrames != 0;
	}

	public void SetCanAttack (bool b)
	{
		canPlayerAttack = b;
		if (b && !hasShownPlayerAttackMessage) {
			attackMessage.enabled = true;
			hasShownPlayerAttackMessage = true;
			StartCoroutine ("MakeAttackTextTransparent");
		}
	}

	public void ShowShiftMessage ()
	{
		shiftMessage.enabled = true;
		StartCoroutine ("MakeShiftTextTransparent");
	}

	public void StartLevel (GameObject nextScene)
	{
		if (!waitingOnLevelStart) {
			waitingOnLevelStart = true;
			nextLevelNumber += 1;

			newScene = nextScene;
			newSource = audioSources.transform.Find (nextScene.GetComponent<MovingSceneController> ().audioSource).GetComponent<AudioSource> ();

			if (currentSource != null) {
				newSource.PlayScheduled (AudioSettings.dspTime + currentSource.clip.length - currentSource.time);
				Invoke ("SetSource", currentSource.clip.length - currentSource.time);
			} else {
				newSource.Play ();
				SetSource ();
			}
		}                   
	}

	private void SetSource ()
	{
		currentSource = newSource;
		Destroy (currentLevel);
		currentLevel = Instantiate (newScene);
		waitingOnLevelStart = false;
		lastBeat = 0f;
		beatNumber = 1;

		if (nextLevelNumber > 2 && nextLevelNumber != levels.Length) {
			checkpointCanvas.enabled = true;
			StartCoroutine ("MakeCheckpointTextTransparent");
		}
	}

	public void RestartLevel ()
	{
		Destroy (currentLevel);
		currentLevel = Instantiate (newScene);
		currentSource.time = 0f;
		lastBeat = 0f;
		beatNumber = 1;
	}

	private int numShakes = 32;

	private void EndShake ()
	{
		if (numShakes > 16)
			cameraController.addShake (1f);
		else if (numShakes > 12)
			cameraController.addShake (2f);
		else if (numShakes > 8)
			cameraController.addShake (4f);
		else
			cameraController.addShake (8f);			
		numShakes -= 1;
	}

	private void EndGame ()
	{
		CancelInvoke ();
		cameraController.killShake ();
		endScreen.enabled = true;
		Invoke ("ShowEndText", 0.8888888888889f);
	}

	private void ShowEndText ()
	{
		endText.enabled = true;
	}

	private void TurnUp ()
	{
		startText.text = "turn up your volume";
		StartCoroutine ("MakeTextTransparent");
	}

	private IEnumerator MakeTextTransparent ()
	{
		for (var t = 2.5f; t >= 0f; t -= 0.1f) {
			startText.color = new Color (1f, 1f, 1f, t);
			yield return new WaitForSeconds (0.11111f);
		}
		startScreen.enabled = false;
	}

	private IEnumerator MakeAttackTextTransparent ()
	{
		for (var t = 2.5f; t >= 0f; t -= 0.1f) {
			attackText.color = new Color (1f, 1f, 1f, t);
			yield return new WaitForSeconds (0.11111f);
		}
		attackMessage.enabled = false;
	}

	private IEnumerator MakeShiftTextTransparent ()
	{
		for (var t = 2.5f; t >= 0f; t -= 0.1f) {
			shiftText.color = new Color (1f, 1f, 1f, t);
			yield return new WaitForSeconds (0.11111f);
		}
		shiftMessage.enabled = false;
	}

	private IEnumerator MakeCheckpointTextTransparent ()
	{
		for (var t = 1.5f; t >= 0f; t -= 0.1f) {
			checkpointText.color = new Color (1f, 1f, 1f, t);
			yield return new WaitForSeconds (0.11111f);
		}
		checkpointCanvas.enabled = false;
	}
}
