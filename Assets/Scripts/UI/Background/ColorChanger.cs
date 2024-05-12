using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundColorChanger : MonoBehaviour
{
    [SerializeField] RawImage image;
    [SerializeField] Color[] colors = { Color.red, Color.green, Color.blue };
    [SerializeField] float speed = 0.1f;
    [SerializeField] float colorBorder = 0.05f;


    int _currentIndex = 0;
    Color _targetColor;

    private void Start()
    {
        image.color = colors[_currentIndex];  // ������������� ������ ����
        _currentIndex = (_currentIndex + 1) % colors.Length;
        _targetColor = colors[_currentIndex];   // ������������� ������ ����
    }

    void Update()
    {
        // ������������� ����� ������� ������ � ��������� ������
        image.color = Color.Lerp(image.color, _targetColor, Time.deltaTime * speed);

        if (Vector4.Distance(image.color, _targetColor) < colorBorder)
        {
            _currentIndex = (_currentIndex + 1) % colors.Length;
            _targetColor = colors[_currentIndex];
        }
    }
}