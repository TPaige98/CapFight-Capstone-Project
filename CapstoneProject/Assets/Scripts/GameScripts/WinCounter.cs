using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCounter : MonoBehaviour
{
    public Text playerWinCount;
    public Text enemyWinCount;

    private int pWins;
    private int eWins;

    public GameObject restartMenu;

    void Start()
    {
        LoadWinCount();
        UpdateDisplay();
        restartMenu.SetActive(false);
    }

    public void LoadWinCount()
    {
        pWins = PlayerPrefs.GetInt("PlayerWins", 0);
        eWins = PlayerPrefs.GetInt("EnemyWins", 0);
    }

    public void SaveWinCount()
    {
        PlayerPrefs.SetInt("PlayerWins", pWins);
        PlayerPrefs.SetInt("EnemyWins", eWins);
        PlayerPrefs.Save();
    }

    public void UpdateDisplay()
    {
        playerWinCount.text = "W: " + pWins;
        enemyWinCount.text = "W: " + eWins;
    }

    public void UpdatePlayerWins()
    {
        pWins++;
        SaveWinCount();
        UpdateDisplay();
    }

    public void UpdateEnemyWins()
    {
       eWins++;
       SaveWinCount();
       UpdateDisplay();
    }
}
