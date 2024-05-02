using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public static WindManager instance;

    public Vector2 windDirection = Vector2.right; // Начальное направление ветра
    public float windStrength = 2f; // Начальная сила ветра

    public float windChangeInterval = 10f; // Интервал изменения параметров ветра
    public float windMinStrength = 50f; // Минимальная сила ветра
    public float windMaxStrength = 150f; // Максимальная сила ветра

    public bool isWindActive = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        Invoke("ToggleWindWithDelay", Random.Range(5f, 15f));
    }

    private void StartWind()
    {
        isWindActive = true;
        ChangeWindParams();
        ApplyWind();
        Debug.Log("Ветер пошел");
    }

    private void StopWind()
    {
        isWindActive = false;
        ApplyWind();
        Debug.Log("Ветер не пошел");
    }

    private void ChangeWindParams()
    {
        // Изменение параметров ветра (сила и направление)
        windStrength = Random.Range(windMinStrength, windMaxStrength);
        windDirection = new Vector2(Random.Range(-1f, 1f), 0).normalized;
        ApplyWind();

    }

    private void ApplyWind()
    {
        // Применение ветра к объектам
        if (isWindActive)
        {
            GameManager.SetWindDirection(windDirection);
            GameManager.SetWindStrength(windStrength);
        }
        else
        {
            GameManager.SetWindDirection(windDirection);
            GameManager.SetWindStrength(0);
        }
    }

    private void ToggleWindWithDelay()
    {
        // Включаем ветер
        StartWind();

        // Выключаем ветер через время
        Invoke("StopWind", Random.Range(5, 15));

        // Перезапускаем цикл через время
        Invoke("ToggleWindWithDelay", Random.Range(5, 15));

    }
}