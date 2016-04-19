using System.Collections;
using UnityEngine;

public class MovingSceneController : MonoBehaviour {
    public float movementSpeed = 1f;
    public float parallaxSpeed = 10f;
    public float parallaxSpeedUpTime = 1f;
    public string audioSource;
    public bool canPlayerAttack = true;
    public bool canPlayerShift = true;

    private GameObject[] visualizers;
    public float audioMinRed = 0f;
    public float audioMaxRed = 1f;
    public float audioMinBlue = 0f;
    public float audioMaxBlue = 1f;
    public float audioMinGreen = 0f;
    public float audioMaxGreen = 1f;

    public float cameraSize = 12f;

    private BeatController beatController;
    private PlayerController playerController;
    private ParallaxController scroller;
    private ParallaxController bScroller;
    private float startParallaxSpeed;
    private float startTime;

    float GetNewSpeed(float spd, float dspd)
    {
        float timeElapsed = (Time.time - startTime) / parallaxSpeedUpTime;
        return Mathf.Lerp(spd, dspd, timeElapsed);
    }

    void Start() {
        beatController = GameObject.Find("BeatController").GetComponent<BeatController>();
        beatController.SetCanAttack(canPlayerAttack);

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.SetCanShift(canPlayerShift);

        scroller = GameObject.Find("RainbowSideScroller").GetComponent<ParallaxController>();
        bScroller = GameObject.Find("RainbowBottomScroller").GetComponent<ParallaxController>();
        startParallaxSpeed = scroller.GetComponent<ParallaxController>().speed;
        startTime = Time.time;

        visualizers = GameObject.FindGameObjectsWithTag("AudioCube");
        foreach (var v in visualizers) 
        {
            v.GetComponent<RandomColorAudioVisualizer>().SetColorRange(audioMinRed, audioMaxRed, audioMinGreen, audioMaxGreen, audioMinBlue, audioMaxBlue);
        }

        //Camera.main.orthographicSize = cameraSize;
        if (cameraSize != 12f) //TODO not this
        {
            StartCoroutine("ZoomOut");
            GameObject.Find("BoundaryBox").transform.localScale = new Vector3(46f, 35f, 26f);
            GameObject.Find("Player").transform.Find("Spearhead").GetComponent<SpearheadController>().screenSize = 20f;
        }
    }

    IEnumerator ZoomOut()
    {
        for (var i = 0; i < 100; i++)
        {
            Camera.main.orthographicSize = 12f + .06f * i;
            yield return new WaitForFixedUpdate();
        }
    }

    // Update is called once per frame
    void Update () {
        this.transform.Translate(0f, 0f, -Time.deltaTime * movementSpeed);

        scroller.speed = GetNewSpeed(startParallaxSpeed, parallaxSpeed);
        bScroller.speed = GetNewSpeed(startParallaxSpeed, parallaxSpeed);
    }
}
