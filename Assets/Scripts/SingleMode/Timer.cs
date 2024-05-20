using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text timerText; 
    public float totalTime = 60f;
    public float score3star = 0f;
    public float score2star = 0f;
    public float score1star = 0f;
    public int idLevel;
    public int result;
    private float currentTime;
    [SerializeField] GameObject playerScore;
    private Score score;
    public GameObject star0;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;


    void Start()
    {
        score = FindObjectOfType<Score>();
        currentTime = totalTime;
        UpdateTimerText();
        InvokeRepeating("UpdateTimer", 1f, 1f); // Запускаем метод UpdateTimer() каждую секунду
    }

    void UpdateTimer()
    {
        if (currentTime > 0)
        {
            currentTime--;
            UpdateTimerText();
        }
        else
        {
            CancelInvoke("UpdateTimer"); // Останавливаем таймер, если он достиг нуля
            EndGame();
        }
    }

    void UpdateTimerText()
    {
        timerText.text = currentTime.ToString();
    }

    private void EndGame()
    {
        playerScore.SetActive(true);
        if (Convert.ToUInt32(score.scoreText.text) > score3star)
        {
            star3.SetActive(true);
            result = 3;
        }
        else if (Convert.ToUInt32(score.scoreText.text) > score2star)
        {
            star2.SetActive(true);
            result = 2;
        }
        else if (Convert.ToUInt32(score.scoreText.text) > score1star)
        {
            star1.SetActive(true);  
            result = 1;
        }
        else
        {
            star0.SetActive(true);
            result = 0;
        }

        playerScore.SetActive(true);

        if (idLevel == 1)
        {
            int curResult = PrefsManager.GetLevel1();
            if (curResult < result)
            {
                PrefsManager.SetLevel1(result);
            }
        }
        else if (idLevel == 2)
        {
            int curResult = PrefsManager.GetLevel2();
            if (curResult < result)
            {
                PrefsManager.SetLevel1(result);
            }
        }
    }
}
