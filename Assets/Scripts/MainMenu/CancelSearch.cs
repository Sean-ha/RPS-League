using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelSearch : MonoBehaviour
{
    // Called upon clicking the cancel button.
    public void StopSearching()
    {
        PhotonNetwork.Disconnect();
    }
}
