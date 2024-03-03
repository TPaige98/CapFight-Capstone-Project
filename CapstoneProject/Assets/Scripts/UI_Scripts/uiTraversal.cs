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
        SceneManager.LoadScene("MainMenu");
    }

    //Fade to Game Scene
    public void FadeGame()
    {
        animator.SetTrigger("FadeOutGame");
    }

    public void FadeCompleteGameOptions()
    {
        SceneManager.LoadScene("GameOptions");
    }
    
    public void quitGame()
    {
        Application.Quit();

        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
