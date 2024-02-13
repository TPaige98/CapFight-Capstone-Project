using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class characterSelectRemember : MonoBehaviour
{
    public void characterSelect1P()
    {
        SceneManager.LoadScene("StageSelectScreen");
    }
}
