using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BeatController : MonoBehaviour
{
    public AudioSource bossLoop;
    public AudioSource endMusic;
    public CameraController cameraController;
    public Canvas endScreen;
    public Text endText;

    private AudioSource currentSource;
    private float lastTime;
	private float totalTime;
    private float lastBeat;
    private float clipLength;
	private int beatNumber = 1;

	public SpearheadController spear;
    
	void Start ()
	{
        lastTime = 0f;
        totalTime = 0f;
        lastBeat = 0f;
        currentSource = bossLoop;
    }    
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.L) && bossLoop.loop)
        {
            //TODO keep player from dying at this point
            bossLoop.loop = false;
            endMusic.PlayScheduled(AudioSettings.dspTime + bossLoop.clip.length - bossLoop.time);
            Invoke("EndMusic", bossLoop.clip.length - bossLoop.time);
        }

        if (currentSource.time < lastTime)
        {
            totalTime += clipLength - lastTime + currentSource.time;
        } else
        {
            totalTime += currentSource.time - lastTime;
        }
        lastTime = currentSource.time;
        clipLength = currentSource.clip.length;

        //TODO write this in a way that it won't get off sync,
        //should suffice for one level though
        if (totalTime - lastBeat > 0.44444444444f) {
            lastBeat = lastBeat + 0.44444444444f;
			beatNumber += 1;
			if (beatNumber == 5) {
				spear.Spear ();
				beatNumber = 1;
			}
		}
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
}
