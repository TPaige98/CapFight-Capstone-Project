using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float countdown = 3.0f;
    public Text countdownText;

    public static bool countdownPause = false;

    public GameObject countdownTimer;
    public GameObject restartMenu;

    public PlayerHealth playerHealth;
    public EnemyHealth enemyHealth;
    public WinCounter winCount;

    public float gameTime = 60.0f;
    public Text gameTimeText;

    public static bool isPaused;

    void Start()
    {
        StartCoroutine(CountdownToGameStart());
        StartCoroutine(CountdownToGameFinish());
    }

    private void Update()
    {
        Debug.Log("Game is Paused, timer Countdown: " + countdown);
        if (gameTime <= 0)
        {
            countdownText.text = "TIME OUT";
            countdownText.gameObject.SetActive(true);
            gameTime = 60.0f;

            if (enemyHealth.health > playerHealth.health)
            {
                winCount.UpdateEnemyWins();
            }
            else if (playerHealth.health > enemyHealth.health)
            {
                winCount.UpdatePlayerWins();
            }

            restartMenu.GetComponent<RestartMenu>().pauseGame();
        }  
    }

    IEnumerator CountdownToGameStart()
    {
        Debug.Log("CountdownToGameStart coroutine started");

        yield return new WaitForSeconds(1f);

        while(countdown > 0)
        {
            countdownText.text = countdown.ToString();

            yield return new WaitForSeconds(1f);

            countdown--;
        }

        countdownText.text = "FIGHT";

        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
    }

    IEnumerator CountdownToGameFinish()
    {
        yield return new WaitForSeconds(4f);

        while(gameTime >= 0)
        {
            gameTimeText.text = gameTime.ToString();

            yield return new WaitForSeconds(1f);

            gameTime--;
        }
    }
}
