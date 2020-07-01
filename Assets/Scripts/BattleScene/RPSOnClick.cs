using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPSOnClick : MonoBehaviour
{
    public void RockButton()
    {
        BattleManager.playerChoice = 1;

        // Moves cursor on top of Rock button
        GameObject.Find("Selected").GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -173, 0);
    }

    public void PaperButton()
    {
        BattleManager.playerChoice = 2;

        // Moves the cursor on top of Paper button
        GameObject.Find("Selected").GetComponent<RectTransform>().anchoredPosition = new Vector3(-90, -308, 0);
    }

    public void ScissorsButton()
    {
        BattleManager.playerChoice = 3;

        // Moves the cursor on top of Scissors button
        GameObject.Find("Selected").GetComponent<RectTransform>().anchoredPosition = new Vector3(90, -308, 0);
    }
}
