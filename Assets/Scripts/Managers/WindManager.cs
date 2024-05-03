using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class WindManager : MonoBehaviour
{
    public static WindManager instance;

    public Vector2 windDirection = Vector2.right; // ��������� ����������� �����
    public float windStrength = 2f; // ��������� ���� �����

    public Image windImage1;
    public Image windImage2;

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
        Debug.Log("����� �����");
    }

    private void StopWind()
    {
        isWindActive = false;
        ApplyWind();
        windImage1.enabled = false;
        windImage2.enabled = false;
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
            yield return new WaitForSeconds(Random.Range(5f,20f)); // �������� 1 �������
            StartWind();
            yield return new WaitForSeconds(Random.Range(5f, 20f)); // �������� 2 �������
            StopWind();
        }
    }
}