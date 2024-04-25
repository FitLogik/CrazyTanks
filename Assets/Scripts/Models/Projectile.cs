using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Projectile : MonoBehaviour
{
    public ProjectileProperties properties;

    private Rigidbody2D _rb;
    private bool _canCollide = true;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, произошёл ли выстрел только что
        if (_canCollide)
        {
            // Проверяем, столкнулась ли пуля с объектом Ground
            if (collision.gameObject.CompareTag("Ground"))
            {
                Instantiate(properties.groundHitPrefab, transform.position, Quaternion.identity);
            }
            // Проверяем, столкнулась ли пуля с объектом Player
            else if (collision.gameObject.CompareTag("Player"))
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>(); // пытаемся получить компонент PlayerController
                if (player != null)
                {
                    player.TakeDamage(properties.damage);
                }
                else
                {
                    Debug.LogError("Ошибка обнаружения игрока!");
                }

                Instantiate(properties.targetHitPrefab, transform.position, Quaternion.identity);
                //textToDisplay = $"{damageBullet}";
                //GameObject DamageTextInstance = Instantiate(damageTextPrefab, Enemy.transform);
                //DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(textToDisplay);
                //Debug.Log(damageBullet);

                Debug.Log($"Hit!\nDamaged Player: {player.playerNumber}");
            }

            // Удаляем объект, с которым пуля столкнулась
            Destroy(gameObject);
        }
    }

    public void Fire(Vector2 bulletDirection, float bulletForce)
    {
        // Образуем импульсный толчок
        _rb.AddForce(bulletDirection * bulletForce, ForceMode2D.Impulse);
        // Запускаем корутин для задержки выполнения
        StartCoroutine(CollisionCooldown(properties.disableColliderTime));
    }

    // Корутин для задержки включения коллизии
    private IEnumerator CollisionCooldown(float cooldown)
    {
        _canCollide = false;
        yield return new WaitForSeconds(cooldown); // ждём {cooldown} секунд
        _canCollide = true;
    }
}



[System.Serializable]
public class ProjectileProperties
{
    public GameObject groundHitPrefab;
    public GameObject targetHitPrefab;
    public int damage = 50;
    public float disableColliderTime = 0.1f;
}