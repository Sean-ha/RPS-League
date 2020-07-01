using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthSetter : MonoBehaviourPun
{
    [SerializeField] private TextMeshProUGUI healthText = null;
    private PlayerManager myPlayerManager = null;

    private void Awake()
    {
        // Correctly places health UI.
        // Also gives BattleManager a reference to both PlayerManagers.
        myPlayerManager = gameObject.GetComponent<PlayerManager>();
        if (photonView.IsMine)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                healthText.rectTransform.anchoredPosition = new Vector3(-130, 50, 0);
            }
            else
            {
                healthText.rectTransform.anchoredPosition = new Vector3(130, 50, 0);
            }
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                healthText.rectTransform.anchoredPosition = new Vector3(130, 50, 0);
                BattleManager.otherPM = myPlayerManager;
            }
            else
            {
                healthText.rectTransform.anchoredPosition = new Vector3(-130, 50, 0);
                BattleManager.masterPM = myPlayerManager;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Displays both players' healths
        healthText.text = "HP: " + myPlayerManager.health + "/" + myPlayerManager.maxHealth;
    }
}
