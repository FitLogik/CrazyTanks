using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class WindManager : MonoBehaviour
{
    public static WindManager instance;

    public Vector2 windDirection = Vector2.right; // Начальное направление ветра
    public float windStrength = 2f; // Начальная сила ветра

    public Image windImage1;
    public Image windImage2;

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
        windImage1.enabled = false;
        windImage2.enabled = false;
        StartCoroutine(CallMethods());
    }

    private void StartWind()
    {
        isWindActive = true;
        ChangeWindParams();
        ApplyWind();
        if (windDirection.x > 0)
        {
            windImage1.enabled = true;
            windImage2.enabled = false;
        }
        else
        {
            windImage1.enabled = false;
            windImage2.enabled = true;
        }
        Debug.Log("Ветер пошел");
    }

    private void StopWind()
    {
        isWindActive = false;
        ApplyWind();
        windImage1.enabled = false;
        windImage2.enabled = false;
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
            RoundManager.SetWindDirection(windDirection);
            RoundManager.SetWindStrength(windStrength);
        }
        else
        {
            RoundManager.SetWindStrength(0);
        }
    }

    IEnumerator CallMethods()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f,20f)); // Подождем 1 секунду
            StartWind();
            yield return new WaitForSeconds(Random.Range(5f, 20f)); // Подождем 2 секунды
            StopWind();
        }
    }
}