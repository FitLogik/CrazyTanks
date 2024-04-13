using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2 : MonoBehaviour
{
    public int damage;
    public GameObject effectGround;
    public GameObject effectTank;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, столкнулась ли пуля с объектом с тегом "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Удаляем объект, с которым пуля столкнулась
            Destroy(gameObject);
            Instantiate(effectGround, transform.position, Quaternion.identity);
        }
        if (collision.gameObject.CompareTag("player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
            Destroy(gameObject);
            Instantiate(effectTank, transform.position, Quaternion.identity);
        }
    }
}