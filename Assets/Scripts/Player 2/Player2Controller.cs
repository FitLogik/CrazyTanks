using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player2Controller : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth = 100;
    public GameObject damageText;
    public Image bar;
    public GameObject panel;
    public GameObject textWin;


    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y); // �������� ����� ��� ������� �� ������ "left"
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y); // �������� ������ ��� ������� �� ������ "right"
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y); // ��������� �������� ��� ���������� ������
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            panel.SetActive(true);
            textWin.SetActive(false);
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        bar.fillAmount = health/100;
    }

    
}

