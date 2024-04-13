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
        // Получаем текущее вращение танка
        float currentRotation = transform.eulerAngles.z;

        // Ограничиваем вращение по оси Z
        if (currentRotation > 180)
        {
            currentRotation -= 360;
        }

        currentRotation = Mathf.Clamp(currentRotation, -maxRotationAngle, maxRotationAngle);

        // Применяем ограниченное вращение к танку
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, currentRotation);
        
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y); // Движение влево при нажатии на кнопку "A"
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y); // Движение вправо при нажатии на кнопку "D"
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y); // Остановка движения при отпускании кнопок
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
