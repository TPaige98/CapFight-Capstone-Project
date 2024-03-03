using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] music = GameObject.FindGameObjectsWithTag("xMusic");
        if (music.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        Destroy(this.gameObject);
    }
}
