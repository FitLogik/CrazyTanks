using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.GraphicsBuffer;

public class EnemyControll : MonoBehaviour
{
    public float horizontalSpeed = 0.5f;
    public float health = 1.0f;
   
    private float minHeight = 0f;
    private float maxHeight = 2f;
    private float targetXPosition = 10.1f; // Задаем целевую позицию по оси X

    private Score sp;

    public bool ComboScore = false;


    private void Start()
    {
        sp = FindObjectOfType<Score>();
    }
    void Update()
    {
        transform.Translate(Vector3.right * -horizontalSpeed * Time.deltaTime);
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

}
