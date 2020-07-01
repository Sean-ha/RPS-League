using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickSound : MonoBehaviour
{
    // Plays the sound on click. This extra step is needed so sound is not cut off when switching between scenes.
    public void OnClickPlaySound()
    {
        GameObject[] sfx = GameObject.FindGameObjectsWithTag("sfx");

        if(sfx.Length == 1)
        {
            sfx[0].GetComponent<AudioSource>().Play();
        }
    }
}
