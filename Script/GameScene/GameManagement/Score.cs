using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;

    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI inGameScoreText;
    [SerializeField] private GameObject gameoverPanel;

    private int score;
    private int nextThreshold;

    public static event Action OnScoreThresholdReached;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        score = 0;
        SetNextThreshold();

        if (gameoverPanel != null)
        {
            inGameScoreText.text = score.ToString();
            highScoreText.text = PlayerPrefs.GetInt("bestdata", 0).ToString();
        }
        UpdateHighScore();
    }

    public void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt("bestdata", 0))
        {
            PlayerPrefs.SetInt("bestdata", score);
            highScoreText.text = score.ToString();
        }
    }

    public void AddScore()
    {
        score++;
        if (inGameScoreText != null)
        {
            inGameScoreText.text = score.ToString();
        }
        if (currentScoreText != null)
        {
            currentScoreText.text = score.ToString();
        }
        UpdateHighScore();

        if (score >= nextThreshold) // Check if the score has reached or surpassed the next threshold
        {
            // Debug.Log("Score Threshold Reached");
            // Debug.Log("Next Threshold: " + nextThreshold);
            OnScoreThresholdReached?.Invoke();
            SetNextThreshold(); // Set the next threshold
        }
    }

    private void SetNextThreshold()
    {
        // Set the next threshold to the current score plus a random number between 3 and 7
        nextThreshold = score + UnityEngine.Random.Range(3, 7);
    }
}
