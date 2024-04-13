using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text timerText; 
    public float totalTime = 60f;
    private float currentTime;

    void Start()
    {
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
        }
    }

    void UpdateTimerText()
    {
        timerText.text = currentTime.ToString();
    }
}
