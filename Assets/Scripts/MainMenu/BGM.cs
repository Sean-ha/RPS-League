using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    // Ensures music continuously plays throughout scenes without stopping, and that multiple music objects will not exist
    // in the same scene.
    private void Awake()
    {
        GameObject[] music = GameObject.FindGameObjectsWithTag("music");
        if(music.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
