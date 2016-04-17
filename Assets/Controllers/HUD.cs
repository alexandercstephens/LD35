using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HUD : MonoBehaviour
{

    public float WeightSpeed = 1;
    public Text Score;
    public GameObject ScoreValue;

    private float CurrentScore = 0;
    private float targetScore = 0;
    private float score = 0;
    private float startingScore = 0;

    void Awake()
    {
        DontDestroyOnLoad(ScoreValue);
        if (GameObject.FindGameObjectsWithTag("Score").Length > 1)
        {
            Destroy(ScoreValue);
        }
        ScoreValue = GameObject.FindGameObjectWithTag("Score");
        Score.text = "Score: " + ScoreValue.transform.position.x;
        startingScore = ScoreValue.transform.position.x;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CurrentScore = Mathf.Lerp(CurrentScore, targetScore, Time.deltaTime * WeightSpeed);
        var nextScore = Mathf.FloorToInt(CurrentScore);

        if (score != nextScore)
        {
            score = nextScore;
            Score.text = "Score: " + score;
        }
        SetScore(startingScore += 0.1f);
    }

    public void SetScore(float score)
    {
        targetScore = score;
    }

    public void SetScoreValue(float value)
    {
        ScoreValue.transform.position = new Vector3(value, value, value); ;
    }

}