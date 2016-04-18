using UnityEngine;

public class MovingSceneController : MonoBehaviour {
    public float movementSpeed = 1f;
    public float parallaxSpeed = 10f;
    public float parallaxSpeedUpTime = 1f;
    public string audioSource;

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
        scroller = GameObject.Find("RainbowSideScroller").GetComponent<ParallaxController>();
        bScroller = GameObject.Find("RainbowBottomScroller").GetComponent<ParallaxController>();
        startParallaxSpeed = scroller.GetComponent<ParallaxController>().speed;
        startTime = Time.time;
    }

	// Update is called once per frame
	void Update () {
        this.transform.Translate(0f, 0f, -Time.deltaTime * movementSpeed);

        scroller.speed = GetNewSpeed(startParallaxSpeed, parallaxSpeed);
        bScroller.speed = GetNewSpeed(startParallaxSpeed, parallaxSpeed);
    }
}
