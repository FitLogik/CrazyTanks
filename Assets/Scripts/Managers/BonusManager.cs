using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusManager : MonoBehaviour
{
    public static BonusManager instance;

    public GameObject froze;
    public GameObject shield;
    public GameObject health;

    private GameObject activeBonus;

    public Image shieldImage1;
    public Image shieldImage2;

    [SerializeField] bool isSpawning = false;


    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    public static void DestroyBonus(int playerID)
    {
        if (instance.activeBonus == instance.shield)
        {
           instance.SetShieldPlayer(playerID);
        }
        else if (instance.activeBonus == instance.health)
        {
            //Я не знаю как увелчить показатель самого игрока
        }
        else if (instance.activeBonus == instance.froze)
        {
            //Как заморозить игрока тоже хз
        }
    }

    public void SetShieldPlayer(int playerID)
    {
        if (playerID == 1)
        {
            shieldImage1.enabled = true;

        }
        else
        {
            shieldImage2.enabled = true;
        }
    }


    public void DelShiledPlayer(int playerID)
    {
        if (playerID == 1)
        {
            shieldImage1.enabled = false;

        }
        else
        {
            shieldImage2.enabled = false;
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
                    froze.SetActive(true);
                    activeBonus = froze;
                    yield return new WaitForSeconds(5.0f);
                    froze.SetActive(false);
                }
                else if (rand == 1)
                {
                    shield.SetActive(true);
                    activeBonus = shield;
                    yield return new WaitForSeconds(5.0f);
                    shield.SetActive(false);
                }
                else
                {
                    health.SetActive(true);
                    activeBonus = health;
                    yield return new WaitForSeconds(5.0f);
                    health.SetActive(false);
                }
                activeBonus = null;

                isSpawning = false;
            }

            yield return null;
        }
    }





}
