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
    public float currentTime;
    [SerializeField] GameObject playerScore;
    
    private Score score;
    public GameObject star0;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public EnemyController enemy;

    AudioManager _audioManager;

    void Start()
    {
        score = FindObjectOfType<Score>();
        currentTime = totalTime;
        UpdateTimerText();
        InvokeRepeating("UpdateTimer", 1f, 1f); // Запускаем метод UpdateTimer() каждую секунду
    }

    private void Awake()
    {
        GameObject audioManagerGO = GameObject.FindGameObjectWithTag("Audio");
        if (audioManagerGO != null)
        {
            _audioManager = audioManagerGO.GetComponent<AudioManager>();
        }
        else
        {
            Debug.LogError("Не удалось найти объект Audio!");
        }
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
            EndGame();
        }
    }

    public void UpdateTimerText()
    {
        timerText.text = currentTime.ToString();
    }

    public void EndGame()
    {
        UpdateTimerText();
        enemy.OnDestroy();
        CancelInvoke("UpdateTimer");
        if (Convert.ToUInt32(score.scoreText.text) > score3star)
        {
            star3.SetActive(true);
            result = 3;
            _audioManager.PlaySFX(_audioManager.winGame);
        }
        else if (Convert.ToUInt32(score.scoreText.text) > score2star)
        {
            star2.SetActive(true);
            result = 2;
            _audioManager.PlaySFX(_audioManager.winRound);

        }
        else if (Convert.ToUInt32(score.scoreText.text) > score1star)
        {
            star1.SetActive(true);  
            result = 1;
            _audioManager.PlaySFX(_audioManager.winRound);
        }
        else
        {
            star0.SetActive(true);
            result = 0;
            _audioManager.PlaySFX(_audioManager.loseGame);
        }

        playerScore.SetActive(true);

        if (idLevel == 1)
        {
            int curResult = PrefsManager.GetLevel1();
            if (curResult <= result && result != 0)
            {
                PrefsManager.SetLevel1(result);
                if (PrefsManager.GetLevel2() == -1)
                {
                    PrefsManager.SetLevel2(0);
                }
            }
        }
        else if (idLevel == 3)
        {
            int curResult = PrefsManager.GetLevel3();
            if (curResult <= result && result != 0)
            {
                PrefsManager.SetLevel3(result);
                if (PrefsManager.GetLevel4() == -1)
                {
                    PrefsManager.SetLevel4(0);
                }
            }
        }
        else if (idLevel == 5)
        {
            int curResult = PrefsManager.GetLevel5();
            if (curResult <= result && result != 0)
            {
                PrefsManager.SetLevel5(result);
                if (PrefsManager.GetLevel6() == -1)
                {
                    PrefsManager.SetLevel6(0);
                }
            }
        }
    }
}
