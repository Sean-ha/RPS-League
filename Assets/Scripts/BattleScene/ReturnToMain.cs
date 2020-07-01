using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMain : MonoBehaviourPunCallbacks
{

    public GameObject blackScreenPanel = null;

    public void DisconnectAndReturn()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        StartCoroutine(FadeBlack());
    }

    // Fades the screen into black as a transition screen
    IEnumerator FadeBlack()
    {
        float counter = 0f;
        CanvasGroup canvasGroup = blackScreenPanel.GetComponent<CanvasGroup>();

        while(counter < 1.0f)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = counter;

            yield return null;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
