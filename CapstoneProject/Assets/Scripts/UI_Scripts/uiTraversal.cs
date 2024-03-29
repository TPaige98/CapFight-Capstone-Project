using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class uiTraversal : MonoBehaviour
{
    public Animator animator;

    //Fade to Start Scene
    public void FadeStart()
    {
        animator.SetTrigger("FadeOutStart");
    }

    public void FadeCompleteMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    //Fade to Game Options Scene
    public void FadeGame()
    {
        animator.SetTrigger("FadeOutGame");
    }

    public void FadeCompleteGameOptions()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOptions");
    }

    //Fade to Game State Scene
    public void FadeGameStart()
    {
        animator.SetTrigger("FadeOutGameStart");
    }

    public void FadeCompleteGameStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameState");
    }
    
    //Quit Game
    public void quitGame()
    {
        Application.Quit();

        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
