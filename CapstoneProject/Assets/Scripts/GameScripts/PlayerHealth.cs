using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int max = 100;
    public int health;

    public HealthBars healthBar;
    public Timer timerScript;
    public GameObject restartMenu;
    public WinCounter winCount;

    void Start()
    {
        health = max;
        healthBar.setMaxHealth(max);
        restartMenu.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.setHealth(health);

        if (health <= 0)
        {
            Debug.Log("death");
            winCount.UpdateEnemyWins();
            restartMenu.GetComponent<RestartMenu>().pauseGame();
        }
    }
}
