using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float maxHealth = 100;
    public float health;
    public float maxRotationAngle = 70f;
    public Image bar;
    public GameObject panel;
    public GameObject textWin;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        health = maxHealth;
        //healthBar.SetHealth(health, maxHealth);
    }


    private void FixedUpdate()
    {
        // �������� ������� �������� �����
        float currentRotation = transform.eulerAngles.z;

        // ������������ �������� �� ��� Z
        if (currentRotation > 180)
        {
            currentRotation -= 360;
        }

        currentRotation = Mathf.Clamp(currentRotation, -maxRotationAngle, maxRotationAngle);

        // ��������� ������������ �������� � �����
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, currentRotation);
        
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y); // �������� ����� ��� ������� �� ������ "A"
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y); // �������� ������ ��� ������� �� ������ "D"
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
        bar.fillAmount = health / 100;
    }

}
