using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public static WindManager instance;

    public Vector2 windDirection = Vector2.right; // ��������� ����������� �����
    public float windStrength = 2f; // ��������� ���� �����

    public float windChangeInterval = 10f; // �������� ��������� ���������� �����
    public float windMinStrength = 50f; // ����������� ���� �����
    public float windMaxStrength = 150f; // ������������ ���� �����

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
        Debug.Log("����� �����");
    }

    private void StopWind()
    {
        isWindActive = false;
        ApplyWind();
        Debug.Log("����� �� �����");
    }

    private void ChangeWindParams()
    {
        // ��������� ���������� ����� (���� � �����������)
        windStrength = Random.Range(windMinStrength, windMaxStrength);
        windDirection = new Vector2(Random.Range(-1f, 1f), 0).normalized;
        ApplyWind();

    }

    private void ApplyWind()
    {
        // ���������� ����� � ��������
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
        // �������� �����
        StartWind();

        // ��������� ����� ����� �����
        Invoke("StopWind", Random.Range(5, 15));

        // ������������� ���� ����� �����
        Invoke("ToggleWindWithDelay", Random.Range(5, 15));

    }
}