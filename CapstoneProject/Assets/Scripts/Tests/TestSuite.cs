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
    [UnityTest]
    public IEnumerator CheckPlayerMovement()
    {
        yield return new WaitForSeconds(0);
    }

    [UnityTest]
    public IEnumerator CheckEnemyMovement()
    {
        yield return new WaitForSeconds(0);
    }

    [UnityTest]
    public IEnumerator CheckPlayerAttack()
    {
        yield return new WaitForSeconds(0);
    }

    [UnityTest]
    public IEnumerator CheckEnemyAttack()
    {
        yield return new WaitForSeconds(0);
    }

    [UnityTest]
    public IEnumerator CheckTimer()
    {
        yield return new WaitForSeconds(0);
    }

    [UnityTest]
    public IEnumerator CheckPauseMenuActive()
    {
        yield return new WaitForSeconds(0);
    }

    [UnityTest]
    public IEnumerator CheckRestartMenuActive()
    {
        yield return new WaitForSeconds(0);
    }

    [UnityTest]
    public IEnumerator CheckWinCounter()
    {
        yield return new WaitForSeconds(0);
    }
}
