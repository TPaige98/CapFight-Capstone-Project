using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;

    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void pauseGame()
    {
        Debug.Log("Game Paused");
        pauseMenu.SetActive(true);

        StartCoroutine(delayPause());
    }

    IEnumerator delayPause()
    {
        yield return null;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseKey(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }
}
