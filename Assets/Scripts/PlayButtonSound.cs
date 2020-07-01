using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    // Ensures sound fx continuously plays throughout scenes without stopping, and that multiple soundfx objects will not exist
    // in the same scene.
    private void Awake()
    {
        GameObject[] sfx = GameObject.FindGameObjectsWithTag("sfx");
        if (sfx.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
