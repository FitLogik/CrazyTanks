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
        if (GameManager.GameType != GameTypes.Game2Players)
        {
            combo = FindObjectOfType<Score>();
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, ��������� �� ������� ������ ���
        if (_canCollide)
        {
            // ���������, ����������� �� ���� � �������� Ground
            if (collision.gameObject.CompareTag("Ground"))
            {
                Instantiate(properties.groundHitPrefab, transform.position, Quaternion.identity);
                if (GameManager.GameType != GameTypes.Game2Players)
                {
                    combo.ResetCombo();

                }
            }
    
            // ���������, ����������� �� ���� � �������� Player
            else if (collision.gameObject.CompareTag("Player"))
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>(); // �������� �������� ��������� PlayerController
                if (player != null)
                {
                    player.TakeDamage(properties.damage);
                }
                else
                {
                    Debug.LogError("������ ����������� ������!");
                }

                Instantiate(properties.targetHitPrefab, transform.position, Quaternion.identity);
                //textToDisplay = $"{damageBullet}";
                //GameObject DamageTextInstance = Instantiate(damageTextPrefab, Enemy.transform);
                //DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(textToDisplay);
                //Debug.Log(damageBullet);

                // TODO: �������� ����� ���������� �����

                Debug.Log($"Hit!\nDamaged Player: {player.playerNumber}");
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                EnemyControll enemy = collision.gameObject.GetComponent<EnemyControll>(); // �������� �������� ��������� PlayerController
                if (enemy != null)
                {
                    enemy.TakeDamage();
                }
                else
                {
                    Debug.LogError("������ ����������� ������!");
                }

                Instantiate(properties.targetHitPrefab, transform.position, Quaternion.identity);
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