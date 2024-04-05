using UnityEngine;
using UnityEngine.SceneManagement;

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

    //Fade to Game State Scene
    public void FadeGameStart()
    {
        animator.SetTrigger("FadeOutGameStart");
    }

    public void FadeCompleteGameStart()
    { 
        SceneManager.LoadScene("GameState");
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    //Quit Game
    public void quitGame()
    {
        Application.Quit();

        //UnityEditor.EditorApplication.isPlaying = false;
    }

    //This is code I wanted to get to, but didn't have the time to get to. I had more ambitious plans.

    //Fade to Game Options Scene
    //public void FadeGame()
    //{
    //    animator.SetTrigger("FadeOutGame");
    //}

    //public void FadeCompleteGameOptions()
    //{
    //    Time.timeScale = 1f;
    //    SceneManager.LoadScene("GameOptions");
    //}
}
