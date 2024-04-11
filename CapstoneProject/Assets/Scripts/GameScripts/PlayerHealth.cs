using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int max = 100;
    private int health;

    public HealthBars healthBar;
    public Timer timerScript;

    void Start()
    {
        health = max;
        healthBar.setMaxHealth(max);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.setHealth(health);

        if (health <= 0)
        {
            Debug.Log("death");
        }
    }
}
