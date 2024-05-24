using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header ("Level status")]
    private int statusLvl1;
    private int statusLvl2;
    private int statusLvl3;
    private int statusLvl4;
    private int statusLvl5;
    private int statusLvl6;
    private int statusLvl7;
    private int statusLvl8;
    private int statusLvl9;
    private int statusLvl10;

    [Header ("Star images")]
    public Sprite star3;
    public Sprite star2;
    public Sprite star1;

    [Header ("Level images stars")]
    public Image imageLevel1;
    public Image imageLevel2;
    public Image imageLevel3;
    public Image imageLevel4;
    public Image imageLevel5;
    public Image imageLevel6;
    public Image imageLevel7;
    public Image imageLevel8;
    public Image imageLevel9;
    public Image imageLevel10;

    [Header("Level panels")]
    public GameObject panelLevel1;
    public GameObject panelLevel2;
    public GameObject panelLevel3;
    public GameObject panelLevel4;
    public GameObject panelLevel5;
    public GameObject panelLevel6;
    public GameObject panelLevel7;
    public GameObject panelLevel8;
    public GameObject panelLevel9;
    public GameObject panelLevel10;

    [Header ("Level buttons")]
    public Button buttonLevel1;
    public Button buttonLevel2;
    public Button buttonLevel3;
    public Button buttonLevel4;
    public Button buttonLevel5;
    public Button buttonLevel6;
    public Button buttonLevel7;
    public Button buttonLevel8;
    public Button buttonLevel9;
    public Button buttonLevel10;



    private void Start()
    {
        SetLevels();
        Debug.Log($"Уровень 1 - {statusLvl1}");
        Debug.Log($"Уровень 2 - {statusLvl2}");
        Debug.Log($"Уровень 3 - {statusLvl3}");
        Debug.Log($"Уровень 4 - {statusLvl4}");
        Debug.Log($"Уровень 5 - {statusLvl5}");
        Debug.Log($"Уровень 6 - {statusLvl6}");
        Debug.Log($"Уровень 7 - {statusLvl7}");
        Debug.Log($"Уровень 8 - {statusLvl8}");
        Debug.Log($"Уровень 9 - {statusLvl9}");
        Debug.Log($"Уровень 10 - {statusLvl10}");
    }

    public void SetLevels()
    {
        statusLvl1 = PrefsManager.GetLevel1();
        statusLvl2 = PrefsManager.GetLevel2();
        statusLvl3 = PrefsManager.GetLevel3();
        statusLvl4 = PrefsManager.GetLevel4();
        statusLvl5 = PrefsManager.GetLevel5();
        statusLvl6 = PrefsManager.GetLevel6();
        statusLvl7 = PrefsManager.GetLevel7();
        statusLvl8 = PrefsManager.GetLevel8();
        statusLvl9 = PrefsManager.GetLevel9();
        statusLvl10 = PrefsManager.GetLevel10();
        #region Level 1
        if (statusLvl1 == 0)
        {
            buttonLevel1.interactable = true;
            panelLevel1.SetActive (false);
        }
        else if (statusLvl1 == 1)
        {
            panelLevel1.SetActive(true);
            imageLevel1.sprite = star1;
        }
        else if (statusLvl1 == 2)
        {
            panelLevel1.SetActive(true);
            imageLevel1.sprite = star2;
        }
        else if (statusLvl1 == 3)
        {
            panelLevel1.SetActive(true);
            imageLevel1.sprite = star3;
        }
        #endregion
        #region Level 2
        if (statusLvl2 == -1)
        {
            panelLevel2.SetActive(false);
            buttonLevel2.interactable = false;
        }
        else if (statusLvl2 == 0)
        {
            panelLevel2.SetActive(false);
            buttonLevel2.interactable = true;
        }
        else if (statusLvl2 == 1)
        {
            panelLevel2.SetActive(true);
            imageLevel2.sprite = star1;
        }
        else if (statusLvl2 == 2)
        {
            panelLevel2.SetActive(true);
            imageLevel2.sprite = star2;
        }
        else if (statusLvl2 == 3)
        {
            panelLevel2.SetActive(true);
            imageLevel2.sprite = star3;
        }
        #endregion
        #region Level 3
        if (statusLvl3 == -1)
        {
            panelLevel3.SetActive(false);
            buttonLevel3.interactable = false;
        }
        else if(statusLvl3 == 0)
        {
            panelLevel3.SetActive(false);
            buttonLevel3.interactable = true;
        }
        else if (statusLvl3 == 1)
        {
            panelLevel3.SetActive(true);
            imageLevel3.sprite = star1;
        }
        else if (statusLvl3 == 2)
        {
            panelLevel3.SetActive(true);
            imageLevel3.sprite = star2;

        }
        else if (statusLvl3 == 3)
        {
            panelLevel3.SetActive(true);
            imageLevel3.sprite = star3;

        }
        #endregion
        #region Level 4
        if (statusLvl4 == -1)
        {
            panelLevel4.SetActive(false);
            buttonLevel4.interactable = false;
        }
        else if (statusLvl4 == 0)
        {
            panelLevel4.SetActive(false);
            buttonLevel4.interactable = true;
        }
        else if (statusLvl4 == 1)
        {
            panelLevel4.SetActive(true);
            imageLevel4.sprite = star1;
        }
        else if (statusLvl4 == 2)
        {
            panelLevel4.SetActive(true);
            imageLevel4.sprite = star2;
        }
        else if (statusLvl4 == 3)
        {
            panelLevel4.SetActive(true);
            imageLevel4.sprite = star3;
        }
        #endregion
        #region Level 5
        if (statusLvl5 == -1)
        {
            panelLevel5.SetActive(false);
            buttonLevel5.interactable = false;
        }
        else if (statusLvl5 == 0)
        {
            panelLevel5.SetActive(false);
            buttonLevel5.interactable = true;
        }
        else if (statusLvl5 == 1)
        {
            panelLevel5.SetActive(true);
            imageLevel5.sprite = star1;
        }
        else if (statusLvl5 == 2)
        {
            panelLevel5.SetActive(true);
            imageLevel5.sprite = star2;
        }
        else if (statusLvl5 == 3)
        {
            panelLevel5.SetActive(true);
            imageLevel5.sprite = star3;
        }
        #endregion
        #region Level 6
        if (statusLvl6 == -1)
        {
            panelLevel6.SetActive(false);
            buttonLevel6.interactable = false;
        }
        else if (statusLvl6 == 0)
        {
            panelLevel6.SetActive(false);
            buttonLevel6.interactable = true;
        }
        else if (statusLvl6 == 1)
        {
            panelLevel6.SetActive(true);
            imageLevel6.sprite = star1;
        }
        else if (statusLvl6 == 2)
        {
            panelLevel6.SetActive(true);
            imageLevel6.sprite = star2;
        }
        else if (statusLvl6 == 3)
        {
            panelLevel6.SetActive(true);
            imageLevel6.sprite = star3;
        }
        #endregion
        #region Level 7
        if (statusLvl7 == -1)
        {
            panelLevel7.SetActive(false);
            buttonLevel7.interactable = false;
        }
        else if (statusLvl7 == 0)
        {
            panelLevel7.SetActive(false);
            buttonLevel7.interactable = true;
        }
        else if (statusLvl7 == 1)
        {
            panelLevel7.SetActive(true);
            imageLevel7.sprite = star1;
        }
        else if (statusLvl7 == 2)
        {
            panelLevel7.SetActive(true);
            imageLevel7.sprite = star2;
        }
        else if (statusLvl7 == 3)
        {
            panelLevel7.SetActive(true);
            imageLevel7.sprite = star3;
        }
        #endregion
        #region Level 8
        if (statusLvl8 == -1)
        {
            panelLevel8.SetActive(false);
            buttonLevel8.interactable = false;
        }
        else if (statusLvl8 == 0)
        {
            panelLevel8.SetActive(false);
            buttonLevel8.interactable = true;
        }
        else if (statusLvl8 == 1)
        {
            panelLevel8.SetActive(true);
            imageLevel8.sprite = star1;
        }
        else if (statusLvl8 == 2)
        {
            panelLevel8.SetActive(true);
            imageLevel8.sprite = star2;
        }
        else if (statusLvl8 == 3)
        {
            panelLevel8.SetActive(true);
            imageLevel8.sprite = star3;
        }
        #endregion
        #region Level 9
        if (statusLvl9 == -1)
        {
            panelLevel9.SetActive(false);
            buttonLevel9.interactable = false;

        }
        else if (statusLvl9 == 0)
        {
            panelLevel9.SetActive(false);
            buttonLevel9.interactable = true;
        }
        else if (statusLvl9 == 1)
        {
            panelLevel9.SetActive(true);
            imageLevel9.sprite = star1;
        }
        else if (statusLvl9 == 2)
        {
            panelLevel9.SetActive(true);
            imageLevel9.sprite = star2;
        }
        else if (statusLvl9 == 3)
        {
            panelLevel9.SetActive(true);
            imageLevel9.sprite = star3;
        }
        #endregion
        #region Level 10
        if (statusLvl10 == -1)
        {
            panelLevel10.SetActive(false);
            buttonLevel10.interactable = false;
        }
        else if (statusLvl10 == 0)
        {
            panelLevel10.SetActive(false);
            buttonLevel10.interactable = true;
        }
        else if (statusLvl10 == 1)
        {
            panelLevel10.SetActive(true);
            imageLevel10.sprite = star1;
        }
        else if (statusLvl10 == 2)
        {
            panelLevel10.SetActive(true);
            imageLevel10.sprite = star2;
        }
        else if (statusLvl10 == 3)
        {
            panelLevel10.SetActive(true);
            imageLevel10.sprite = star3;
        }
        #endregion
    }
}
