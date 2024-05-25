using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;
    private int comboCounter = 0; // Переменная для отслеживания комбо-ударов
    public int baseScoreValue = 1;

    // Start is called before the first frame update


    // Update is called once per frame
    private void Update()
    {
        scoreText.text = score.ToString();
    }

    public void Kill()
    {
        score += baseScoreValue * (1 + comboCounter); // Увеличиваем количество очков в зависимости от комбо-счета
        comboCounter++; // Увеличиваем комбо-счет после каждого удара
    }

    public void ResetCombo()
    {
        comboCounter = 0; // Сбрасываем комбо-счет
    }
}
