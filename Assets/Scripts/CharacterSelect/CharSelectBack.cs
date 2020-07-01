using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectBack : MonoBehaviour
{

    public GameObject scrollArea = null;
    public GameObject characterInfoPanel = null;

    // Update is called once per frame
    public void BackButton()
    {
        if(scrollArea.activeSelf)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
        else
        {
            characterInfoPanel.SetActive(false);
            scrollArea.SetActive(true);
        }
    }
}
