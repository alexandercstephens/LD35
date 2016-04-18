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
    private float lastTime;
	private float totalTime;
    private float lastBeat;
    private float checkpointTotalTime;
    private float checkpointLastBeat;
    private float clipLength;
	private int beatNumber = 1;

    private string state;
    private AudioSource newSource;
    private GameObject newScene;
    private int nextLevelNumber = 0;
    private bool waitingOnLevelStart = false;

    private bool canPlayerAttack = false;

	public SpearheadController spear;
    
	void Start ()
	{
        lastTime = 0f;
        totalTime = 0f;
        lastBeat = 0f;
        currentSource = null;
        state = "start";
    }    
	
	// Update is called once per frame
	void Update ()
	{
        switch(state)
        {
            case "start":
                if (Math.Abs(Input.GetAxis("Horizontal")) > 0.01f || Math.Abs(Input.GetAxis("Vertical")) > 0.01f)
                {
                    state = null;
                    Invoke("TurnUp", 0.5f);
                    StartLevel(levels[nextLevelNumber]);
                }
                break;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            currentSource.loop = false;
        }

        if (currentSource != null)
        {
            if (!currentSource.loop && nextLevelNumber < levels.Length && currentSource.clip.length - currentSource.time < 0.5f)
            {
                StartLevel(levels[nextLevelNumber]);
            }

            if (currentSource.time < lastTime)
            {
                totalTime += clipLength - lastTime + currentSource.time;
            }
            else
            {
                totalTime += currentSource.time - lastTime;
            }
            lastTime = currentSource.time;
            clipLength = currentSource.clip.length;

            //TODO write this in a way that it won't get off sync,
            //should suffice for one level though
            if (totalTime - lastBeat > 0.44444444444f)
            {
                lastBeat = lastBeat + 0.44444444444f;
                beatNumber += 1;
                if (beatNumber == 5)
                {
                    if (canPlayerAttack) spear.Spear();
                    beatNumber = 1;
                }
            }
        }        
	}

    public void SetCanAttack(bool b)
    {
        canPlayerAttack = b;
    }

    public void StartLevel(GameObject nextScene)
    {
        if (!waitingOnLevelStart)
        {
            waitingOnLevelStart = true;
            nextLevelNumber += 1;

            newScene = nextScene;
            newSource = audioSources.transform.Find(nextScene.GetComponent<MovingSceneController>().audioSource).GetComponent<AudioSource>();

            if (currentSource != null)
            {
                newSource.PlayScheduled(AudioSettings.dspTime + currentSource.clip.length - currentSource.time);
                Invoke("SetSource", currentSource.clip.length - currentSource.time);
                if (nextLevelNumber == levels.Length)
                {
                    InvokeRepeating("EndShake", currentSource.clip.length - currentSource.time, 0.4444444444f);
                }
            }
            else
            {
                newSource.Play();
                SetSource();
            }
        }                   
    }
    private void SetSource()
    {
        currentSource = newSource;
        Destroy(currentLevel);
        currentLevel = Instantiate(newScene);
        checkpointTotalTime = totalTime;
        checkpointLastBeat = lastBeat;
        waitingOnLevelStart = false;
    }

    public void RestartLevel()
    {
        Destroy(currentLevel);
        currentLevel = Instantiate(newScene);
        currentSource.time = 0f;
        lastTime = 0f;
        totalTime = checkpointTotalTime;
        lastBeat = checkpointLastBeat;
    }

    private int numShakes = 48;
    private void EndShake()
    {
        if (numShakes > 32)
            cameraController.addShake(1f);
        else if (numShakes > 16)
            cameraController.addShake(2f);
        else
            cameraController.addShake(4f);
        numShakes -= 1;
        if (numShakes == 0)
        {
            CancelInvoke();
            cameraController.killShake();
            endScreen.enabled = true;
            Invoke("ShowEndText", 0.8888888888889f);
        }
    }
    private void ShowEndText()
    {
        endText.enabled = true;
    }

    private void TurnUp()
    {
        startText.text = "turn up your volume";
        StartCoroutine("MakeTextTransparent");
    }

    private IEnumerator MakeTextTransparent()
    {
        for (var t = 2.5f; t >= 0f; t -= 0.1f)
        {
            startText.color = new Color(1f, 1f, 1f, t);
            yield return new WaitForSeconds(0.1f);
        }
        startScreen.enabled = false;
    }
}
