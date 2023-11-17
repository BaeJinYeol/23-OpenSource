using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeGauge : MonoBehaviour
{
    public float totalTime = 60f;
    private float currentTime;

    public Slider timeSlider;
    public Text timeText;
    public GameObject gameOverPanel;
    public Button restartButton;

    private bool isGameOver = false;

    void Start()
    {
        currentTime = totalTime;
        timeSlider.maxValue = totalTime;
        UpdateTimeUI();
    }

    void Update()
    {
        if (!isGameOver)
        {
            currentTime -= Time.deltaTime;
            UpdateTimeUI();

            if (currentTime <= 0f)
            {
                GameOver();
            }
        }
    }

    void UpdateTimeUI()
    {
        timeSlider.value = currentTime;
        timeText.text = Mathf.CeilToInt(currentTime).ToString();
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        isGameOver = false;
        currentTime = totalTime;
        UpdateTimeUI();

        gameOverPanel.SetActive(false);
    }
}
