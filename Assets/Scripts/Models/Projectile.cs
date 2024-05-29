using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;

[System.Serializable]
public class Projectile : MonoBehaviour
{
    public ProjectileProperties properties;

    private Rigidbody2D _rb;
    AudioManager _audioManager;
    private bool _canCollide = true;
    private Score combo;

    private void Start()
    {
        if (GameManager.GameType == GameType.Game1Player)
        {
            combo = FindObjectOfType<Score>();
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        GameObject audioManagerGO = GameObject.FindGameObjectWithTag("Audio");
        if (audioManagerGO != null)
        {
            _audioManager = audioManagerGO.GetComponent<AudioManager>();
        }
        else
        {
            Debug.LogError("Не удалось найти объект Audio!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, произошёл ли выстрел только что
        if (_canCollide)
        {
            _canCollide = false; // если OnCollisionEnter2D вызовется ещё раз, пока не уничтожился объект

            // Проверяем, столкнулась ли пуля с объектом Ground
            if (collision.gameObject.CompareTag("Ground"))
            {
                Instantiate(properties.groundHitPrefab, transform.position, Quaternion.identity);
                if (GameManager.GameType == GameType.Game1Player)
                {
                    combo.ResetCombo();
                }
                if (_audioManager != null)
                {
                    _audioManager.PlaySFX(_audioManager.hit);
                }
            }
            // Проверяем, столкнулась ли пуля с объектом Player
            else if (collision.gameObject.CompareTag("Player"))
            {
                TankController player = collision.gameObject.GetComponent<TankController>(); // пытаемся получить компонент TankController
                if (player != null)
                {
                    player.TakeDamage(properties.damage);
                }
                else
                {
                    Debug.LogError("Ошибка обнаружения игрока!");
                }
                if (_audioManager != null)
                {
                    _audioManager.PlaySFX(_audioManager.hit);
                }
                Instantiate(properties.targetHitPrefab, transform.position, Quaternion.identity);
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>(); // пытаемся получить компонент EnemyController
                if (enemy != null)
                {
                    enemy.TakeDamage();
                }
                else
                {
                    Debug.LogError("Ошибка обнаружения игрока!");
                }
                if (_audioManager != null)
                {
                    _audioManager.PlaySFX(_audioManager.hit);
                }
                Instantiate(properties.targetHitPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                if (GameManager.GameType == GameType.Game1Player)
                {
                    combo.ResetCombo();
                }
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
    [HideInInspector] public int owner = 1;
}