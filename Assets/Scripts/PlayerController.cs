using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float tankSpeed = 1;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float maxRotationAngle = 70f;
    [SerializeField] string inputString;
    

    private float health;
    private Rigidbody2D rb;

    private float horizontalInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        health = maxHealth;
    }


    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis(inputString);

        Vector2 wantedPosition = transform.position + (transform.forward * horizontalInput * tankSpeed * Time.deltaTime);

        rb.MovePosition(wantedPosition);

        //rb.velocity = new Vector2(horizontalInput * tankSpeed, rb.velocity.y);
        //rb.AddForce(new Vector2(horizontalInput * speed, 0), ForceMode2D.Force);


        // Ограничиваем вращение по оси Z
        float currentRotation = Mathf.Clamp(transform.rotation.z, -180, 180);

        // Применяем ограниченное вращение к танку
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, currentRotation);




        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
