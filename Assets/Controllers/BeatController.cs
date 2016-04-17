using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BeatController : MonoBehaviour
{
    public GameObject audioSources;
    public AudioSource startMusic;
    public AudioSource checkpoint1Music;
    public AudioSource bossLoop;
    public AudioSource endMusic;
    public CameraController cameraController;
    public Canvas startScreen;
    public Text startText;
    public Canvas endScreen;
    public Text endText;

    public GameObject level1;

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
                    startMusic.Play();
                    currentSource = startMusic;
                    Invoke("TurnUp", 0.5f);
                    StartLevel(level1);
                }
                break;
        }
        if (Input.GetKeyDown(KeyCode.L) && bossLoop.loop)
        {
            //TODO keep player from dying at this point
            bossLoop.loop = false;
            endMusic.PlayScheduled(AudioSettings.dspTime + bossLoop.clip.length - bossLoop.time);
            Invoke("EndMusic", bossLoop.clip.length - bossLoop.time);
        }

        if (currentSource != null)
        {
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
                    spear.Spear();
                    beatNumber = 1;
                }
            }
        }        
	}

    public void StartLevel(GameObject nextScene)
    {
        newScene = nextScene;
        newSource = audioSources.transform.Find(nextScene.GetComponent<MovingSceneController>().audioSource).GetComponent<AudioSource>();
        newSource.PlayScheduled(AudioSettings.dspTime + currentSource.clip.length - currentSource.time);
        
        Invoke("SetSource", currentSource.clip.length - currentSource.time);        
    }
    private void SetSource()
    {
        currentSource = newSource;
        currentLevel = Instantiate(newScene);
        checkpointTotalTime = totalTime;
        checkpointLastBeat = lastBeat;
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

    private void EndMusic()
    {
        currentSource = endMusic;
        InvokeRepeating("EndShake", 0f, 0.4444444444f);
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
