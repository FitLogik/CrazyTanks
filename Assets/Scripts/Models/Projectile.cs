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
            Debug.LogError("�� ������� ����� ������ Audio!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, ��������� �� ������� ������ ���
        if (_canCollide)
        {
            _canCollide = false; // ���� OnCollisionEnter2D ��������� ��� ���, ���� �� ����������� ������

            // ���������, ����������� �� ���� � �������� Ground
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
            // ���������, ����������� �� ���� � �������� Player
            else if (collision.gameObject.CompareTag("Player"))
            {
                TankController player = collision.gameObject.GetComponent<TankController>(); // �������� �������� ��������� TankController
                if (player != null)
                {
                    player.TakeDamage(properties.damage);
                }
                else
                {
                    Debug.LogError("������ ����������� ������!");
                }
                if (_audioManager != null)
                {
                    _audioManager.PlaySFX(_audioManager.hit);
                }
                Instantiate(properties.targetHitPrefab, transform.position, Quaternion.identity);
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>(); // �������� �������� ��������� EnemyController
                if (enemy != null)
                {
                    enemy.TakeDamage();
                }
                else
                {
                    Debug.LogError("������ ����������� ������!");
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
            // ������� ������, � ������� ���� �����������
            Destroy(gameObject);
        }
    }

    public void Fire(Vector2 bulletDirection, float bulletForce)
    {
        // �������� ���������� ������
        _rb.AddForce(bulletDirection * bulletForce, ForceMode2D.Impulse);
        // ��������� ������� ��� �������� ����������
        StartCoroutine(CollisionCooldown(properties.disableColliderTime));
    }

    // ������� ��� �������� ��������� ��������
    private IEnumerator CollisionCooldown(float cooldown)
    {
        _canCollide = false;
        yield return new WaitForSeconds(cooldown); // ��� {cooldown} ������
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