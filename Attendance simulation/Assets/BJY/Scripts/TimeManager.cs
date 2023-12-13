using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : Singleton<TimeManager>
{
    [SerializeField]
    private GameObject timerBg;
    [SerializeField]
    private TMP_Text time_text;

    private Image timerBg_image;
    private Color default_color = Color.HSVToRGB(207f / 360f, 1f, 1f);

    private float time;
    private bool isTime = false;

    private void Start()
    {
        timerBg_image = timerBg.GetComponent<Image>();
        timerBg_image.color = default_color;
        timerBg.SetActive(false);
    }

    private void Update()
    {
        if (isTime)
        {
            time -= Time.deltaTime;
            SetTimeCanvas();
            if (time <= 40f && time > 20f)
            {
                timerBg_image.color = Color.yellow;
            }
            else if (time <= 20f && time > 0f)
            {
                timerBg_image.color = Color.red;
            }
            else if (time <= 0f)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    public void StartTime(float sec)
    {
        time = sec;
        isTime = true; 
        timerBg_image.color = default_color;
    }

    public void EndTime()
    {
        isTime = false;
        timerBg.SetActive(false);
    }

    public void ActiveTime()
    {
        timerBg.SetActive(true);
    }

    private void SetTimeCanvas()
    {
        int minute = (int)time / 60;
        int second = (int)time % 60;
        string minute_str = minute.ToString();
        string second_str = second.ToString();

        if (minute < 10)
            minute_str = "0" + minute.ToString();
        if (second < 10)
            second_str = "0" + second.ToString();

        string tmp_text = minute_str + ":" + second_str;
        time_text.text = tmp_text;
    }
}
