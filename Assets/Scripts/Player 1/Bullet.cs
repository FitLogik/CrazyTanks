using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Bullet : MonoBehaviour
{
    public int damageBullet;
    public GameObject effectGround;
    public GameObject effectTank;

    public GameObject damageTextPrefab;
    public GameObject Enemy;
    private string textToDisplay;


    private void OnCollisionEnter2D(Collision2D collision)
    {


        // Проверяем, столкнулась ли пуля с объектом с тегом "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Удаляем объект, с которым пуля столкнулась
            Destroy(gameObject);
            Instantiate(effectGround, transform.position, Quaternion.identity);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Player2Controller player = collision.gameObject.GetComponent<Player2Controller>();
            player.TakeDamage(damageBullet);
            Destroy(gameObject);
            Instantiate(effectTank, transform.position, Quaternion.identity);
            textToDisplay = $"{damageBullet}";
            GameObject DamageTextInstance = Instantiate(damageTextPrefab, Enemy.transform);
            DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(textToDisplay);
            Debug.Log(damageBullet);
        }

    }
}