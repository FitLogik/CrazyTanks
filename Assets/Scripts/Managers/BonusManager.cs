using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusManager : MonoBehaviour
{
    public static BonusManager instance;

    public GameObject freeze;
    public GameObject shield;
    public GameObject health;

    private GameObject activeBonus;

    public GameObject shieldImage1;
    public GameObject shieldImage2;

    public float shieldDuration;
    public float healthDuration;
    public float freezeDuration;

    [SerializeField] bool isSpawning = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    public static void ApplyBonus(int playerID, GameObject collisionBonus)
    {
        if (collisionBonus == instance.shield)
        {
            Debug.Log($"Shield applied\nPlayer owner: {playerID}");
            instance.SetShield(playerID);
        }
        else if (collisionBonus == instance.health)
        {
            Debug.Log($"Health applied\nPlayer owner: {playerID}");
            // TODO: Увеличить здоровье игрока
        }
        else if (collisionBonus == instance.freeze)
        {
            Debug.Log($"Freeze applied\nPlayer owner: {playerID}");
            // TODO: Заморозить противника
        }
    }

    public void SetShield(int playerID)
    {
        if (playerID == 1)
        {
            shieldImage1.SetActive(true);

        }
        else
        {
            shieldImage2.SetActive(true);
        }
    }


    public void DelShiledPlayer(int playerID)
    {
        if (playerID == 1)
        {
            shieldImage1.SetActive(false);

        }
        else
        {
            shieldImage2.SetActive(false);
        }
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            if (!isSpawning)
            {
                isSpawning = true;

                float randomDelay = Random.Range(1.0f, 3.0f);

                yield return new WaitForSeconds(randomDelay);
                int rand = Random.Range(1, 3);
                if (rand == 0) // Решаем, какой объект появится
                {
                    freeze.SetActive(true);
                    activeBonus = freeze;
                    yield return new WaitForSeconds(freezeDuration);
                    freeze.SetActive(false);
                }
                else if (rand == 1)
                {
                    shield.SetActive(true);
                    activeBonus = shield;
                    yield return new WaitForSeconds(shieldDuration);
                    shield.SetActive(false);
                }
                else
                {
                    health.SetActive(true);
                    activeBonus = health;
                    yield return new WaitForSeconds(healthDuration);
                    health.SetActive(false);
                }
                activeBonus = null;

                isSpawning = false;
            }

            yield return null;
        }
    }





}
