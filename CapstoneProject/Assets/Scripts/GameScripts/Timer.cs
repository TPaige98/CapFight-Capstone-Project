using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float countdown = 3.0f;
    public Text countdownText;

    public static bool countdownPause = false;
    public GameObject countdownTimer;

    public float gameTime = 10.0f;
    public Text gameTimeText;

    private void Start()
    {
        StartCoroutine(CountdownToGameStart());
        StartCoroutine(CountdownToGameFinish());
    }

    private void Update()
    {
        if (gameTime < 0)
        {
            countdownText.text = "TIME OUT";
            countdownText.gameObject.SetActive(true);
            Pause();
        }  
    }

    IEnumerator CountdownToGameStart()
    {
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

    void Resume()
    {
        countdownTimer.SetActive(false);
        Time.timeScale = 1f;
        countdownPause = false;
    }

    void Pause()
    {
        countdownTimer.SetActive(true);
        Time.timeScale = 0f;
        countdownPause = true;
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
