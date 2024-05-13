using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text timerText; 
    public float totalTime = 60f;
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
        if (Convert.ToUInt32(score.scoreText.text) > 80)
        {
            star3.SetActive(true);
        }
        else if (Convert.ToUInt32(score.scoreText.text) > 40)
        {
            star2.SetActive(true);
        }
        else if (Convert.ToUInt32(score.scoreText.text) > 20)
        {
            star1.SetActive(true);  
        }
        else
        {
            star0.SetActive(true);
        }

        playerScore.SetActive(true);
    }
}
