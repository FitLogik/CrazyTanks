using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float fireRate = 2f;
    public GameObject playerTarget;
    public GameObject projectilePrefab;
    public Transform muzzleTransform;

    private bool canFire = true;

    void Update()
    {
        MoveBot();
        if (playerTarget != null)
        {
            RotateTowardsPlayer();
            if (canFire)
            {
                Fire();
                canFire = false;
                Invoke("ResetFire", 1f / fireRate);
            }
        }
    }

    void MoveBot()
    {
        // Простое движение бота влево-вправо
        transform.Translate(Vector3.right * Mathf.Sin(Time.time) * Time.deltaTime * movementSpeed);
    }

    void RotateTowardsPlayer()
    {
        Vector3 targetDirection = playerTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Fire()
    {
        if (projectilePrefab != null && muzzleTransform != null)
        {
            Instantiate(projectilePrefab, muzzleTransform.position, muzzleTransform.rotation);
        }
    }

    void ResetFire()
    {
        canFire = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerTarget = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == playerTarget)
        {
            playerTarget = null;
        }
    }
}
