using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControlSM : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public float maxAngle = 45f;
    public float minAngle = -15f;

    public GameObject projectilePrefab;  // ������ �� ������ �������
    public Transform firePoint;
    public float maxPower = 40f; // ������������ ���� ��������
    public float powerIncreaseRate = 15f; // �������� ���������� ���� ��������
    public float currentPower; // ������� ���� ��������
    private bool isChargingPower; // ���� ������� ���� ��������

    public float currentDamage = 5;
    public float damageIncreaseRate = 100;
    public float maxDamage = 99;

    public GameObject shotSound;


    void Update()
    {
        float rotationAmount = 0f;
        if (Input.GetKey(KeyCode.S))
        {
            rotationAmount = -rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rotationAmount = rotationSpeed * Time.deltaTime;
        }

        // ��������� �������
        transform.Rotate(0f, 0f, rotationAmount, Space.Self);

        // ���������� ������� � ������� ������
        if (transform.localRotation.eulerAngles.z > maxAngle && transform.localRotation.eulerAngles.z < 180f)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, maxAngle);
        }

        // ���������� ������� � ������ ������
        if (transform.localRotation.eulerAngles.z > 180f && transform.localRotation.eulerAngles.z < (360f + minAngle))
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, (360f + minAngle));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isChargingPower = true;
            currentPower = 3f;
            currentDamage = 3f;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isChargingPower = false;
            FireProjectile();
        }

        if (isChargingPower)
        {
            currentPower += powerIncreaseRate * Time.deltaTime;
            currentDamage += damageIncreaseRate * Time.deltaTime;
            currentPower = Mathf.Clamp(currentPower, 0f, maxPower);
            currentDamage = Mathf.Clamp(currentDamage, 0f, maxDamage);
        }
    }

    private void FireProjectile()
    {
        BulletEnemy projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation).GetComponent<BulletEnemy>();
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (projectileRb != null)
        {
            projectileRb.velocity = projectile.transform.right * currentPower;
            projectile.damageBullet = Convert.ToInt32(currentDamage);
            Instantiate(shotSound, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("������ ������� �� �������� Rigidbody2D ���������");
        }
    }
}
