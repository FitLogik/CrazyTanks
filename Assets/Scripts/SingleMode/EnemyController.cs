using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyController : MonoBehaviour
{
    public float horizontalSpeed = 0.5f;
    public float health = 1.0f;

    private float minHeight = 0f;
    private float maxHeight = 2f;
    private float targetXPosition = 10.1f; // ������ ������� ������� �� ��� X

    private Score sp;
    private Timer timer;

    public bool ComboScore = false;


    private void Start()
    {
        sp = FindObjectOfType<Score>();
        timer = FindFirstObjectByType<Timer>();

    }
    void Update()
    {
        transform.Translate(Vector3.right * -horizontalSpeed * Time.deltaTime);
        if (transform.position.x <= Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x)
        {
            timer.currentTime = 0;
            timer.UpdateTimerText();
        }
    }

    public void TakeDamage()
    {
        EnemyDestroyed();
        sp.Kill();
    }

    private void EnemyDestroyed()
    {
        float randomHeight = Random.Range(minHeight, maxHeight);
        Vector3 newPosition = new Vector3(targetXPosition, randomHeight, transform.position.z);
        transform.position = newPosition;
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
