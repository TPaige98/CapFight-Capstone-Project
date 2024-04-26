using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestSuite
{

    /*
    var asyncLoad = SceneManager.LoadSceneAsync("GameState");
    yield return asyncLoad;
    */

    /*
    var asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
    yield return asyncLoad;
    */

    [UnityTest]
    public IEnumerator CheckPlayerMovement()
    {
        var asyncLoad = SceneManager.LoadSceneAsync("GameState");
        yield return asyncLoad;

        var player = GameObject.Find("Player");
        var playerMovement = player.GetComponent<PlayerMovement>();

        yield return new WaitForSeconds(4.5f);

        playerMovement.horizontalInput = 1.0f;

        yield return new WaitForSeconds(1.5f);

        Assert.Greater(player.transform.position.x, 0f);
    }

    [UnityTest]
    public IEnumerator CheckEnemyMovement()
    {
        var asyncLoad = SceneManager.LoadSceneAsync("GameState");
        yield return asyncLoad;

        var enemy = GameObject.Find("Enemy");
        var enemyMovement = enemy.GetComponent<Enemy>();

        yield return new WaitForSeconds(4.5f);

        enemyMovement.walkSpeed = 1.0f;

        yield return new WaitForSeconds(1.5f);

        Assert.Greater(enemy.transform.position.x, 0f);
    }

    [UnityTest]
    public IEnumerator CheckCountdownTimer()
    {
        var asyncLoad = SceneManager.LoadSceneAsync("GameState");
        yield return asyncLoad;

        var timerObject = GameObject.Find("Timer");
        var timer = timerObject.GetComponent<Timer>();

        timer.countdown = 3.0f;

        yield return new WaitForSeconds(2.5f);

        Assert.Less(timer.countdown, 3.0f);
    }

    [UnityTest]
    public IEnumerator CheckGameTimer()
    {
        var asyncLoad = SceneManager.LoadSceneAsync("GameState");
        yield return asyncLoad;

        var timerObject = GameObject.Find("Timer");
        var timer = timerObject.GetComponent<Timer>();

        timer.gameTime = 60;

        yield return new WaitForSeconds(5.5f);

        Assert.Less(timer.gameTime, 60);
    }

    //[UnityTest]
    //public IEnumerator CheckPlayerAttack()
    //{
    //    var asyncLoad = SceneManager.LoadSceneAsync("GameState");
    //    yield return asyncLoad;

    //    var player = GameObject.Find("Player");
    //    var playerAttack = player.GetComponent<PlayerMovement>();

    //    yield return new WaitForSeconds(4.5f);

    //    var attack = playerAttack.GetComponent<PlayerInput>().Jab;

    //    Assert.IsTrue(playerAttack.isAttacking());
    //}
}
