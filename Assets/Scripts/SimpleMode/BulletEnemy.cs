using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public int damageBullet;
    public GameObject effectGround;
    public GameObject effectTank;
    public GameObject Enemy;
    private Camera mainCamera;

    private Score combo;


    private void Start()
    {
        mainCamera = Camera.main;
        combo = FindObjectOfType<Score>();

    }

    void Update()
    {
        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);

        if (viewPos.x > 1 || viewPos.y > 1)
        {
            // ���� ������ ���� �� ������� ��������� ������, ����� ������� ���
            Destroy(gameObject);
            combo.ResetCombo();

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, ����������� �� ���� � �������� � ����� "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            // ������� ������, � ������� ���� �����������
            combo.ResetCombo();
            Destroy(gameObject);
            Instantiate(effectGround, transform.position, Quaternion.identity);

        }
        if (collision.gameObject.CompareTag("player"))
        {
            Destroy(gameObject);
            Instantiate(effectTank, transform.position, Quaternion.identity);
            EnemyControll enemy = collision.gameObject.GetComponent<EnemyControll>();
            enemy.TakeDamage();
        }

    }
}