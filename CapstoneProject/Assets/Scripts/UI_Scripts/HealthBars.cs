using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    public Slider slider;

    public GameObject WinnerRestartMenu;

    private void Start()
    {
        WinnerRestartMenu.SetActive(false);
    }

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void setHealth(int health)
    {
        slider.value = health;

        if (health <= 0)
        {
            WinnerRestartMenu.GetComponent<RestartMenu>().pauseGame();
        }
    }
}
