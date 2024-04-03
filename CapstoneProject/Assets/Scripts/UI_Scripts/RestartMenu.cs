using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{

    public GameObject WinnerRestartMenu;

    public static bool isPaused;

    public void pauseGame()
    {
        WinnerRestartMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void restartGame()
    {
        SceneManager.LoadScene("GameState");
        Time.timeScale = 1f;
        isPaused = false;
    }
}
