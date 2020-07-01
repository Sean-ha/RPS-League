using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NametagSetter : MonoBehaviourPun
{
    [SerializeField] private TextMeshProUGUI nameTagText = null;

    void Start()
    {
        // Ensures name tag says correct name and is in the correct location.
        nameTagText.text = photonView.Owner.NickName;
        if (photonView.IsMine)
        {
            nameTagText.text += "\n(You)";
            if(PhotonNetwork.IsMasterClient)
            {
                nameTagText.rectTransform.anchoredPosition = new Vector3(-130, 240, 0);
            }
            else
            {
                nameTagText.rectTransform.anchoredPosition = new Vector3(130, 240, 0);
            }
        }
        else
        {
            if(PhotonNetwork.IsMasterClient)
            {
                nameTagText.rectTransform.anchoredPosition = new Vector3(130, 240, 0);
            }
            else
            {
                nameTagText.rectTransform.anchoredPosition = new Vector3(-130, 240, 0);
            }
        }
    }
}
