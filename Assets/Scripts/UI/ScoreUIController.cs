using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    TMP_Text scoreText;


    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    public void UpdateUI(string scoreText)
    {
        UpdateText(scoreText);
        SetPosition(UIManager.ScoreSettings);
    }

    public void UpdateText(string text)
    {
        scoreText.text = text;
    }

    private void SetPosition(ScoreUISettings uiSettings)
    {
        Debug.Log(uiSettings.textAlignmentOptions);
        scoreText.alignment = uiSettings.textAlignmentOptions;                  // ������������ ������ �� ������ ������

        Debug.Log(uiSettings.rectTransformAnchorMin);
        Debug.Log(uiSettings.rectTransformAnchorMax);
        scoreText.rectTransform.anchorMin = uiSettings.rectTransformAnchorMin;  // ������ ����� ������������ ����� �� ����� Canvas
        scoreText.rectTransform.anchorMax = uiSettings.rectTransformAnchorMax;

        Debug.Log(uiSettings.scorePosition);
        scoreText.rectTransform.anchoredPosition = uiSettings.scorePosition;                     // ������� ������������ �����, �������������� ����   
    }

}



[System.Serializable]
public class ScoreUISettings
{
    public Vector2 rectTransformAnchorMin = new Vector2(1, 1);
    public Vector2 rectTransformAnchorMax = new Vector2(1, 1);
    public TextAlignmentOptions textAlignmentOptions = TextAlignmentOptions.Center;
    public Vector2 scorePosition = new Vector2(0, -100f);
    
}