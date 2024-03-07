using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    private int currentHealth;

    public HealthBars healthBar;
    public Timer timerScript;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    void Update()
    {
        if (timerScript.countdown <= 0)
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("death");
        }
    }
}
