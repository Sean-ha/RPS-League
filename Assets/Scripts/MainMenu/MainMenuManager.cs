using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject findOpponentPanel = null;
    [SerializeField] private GameObject waitingStatusPanel = null;
    [SerializeField] private TextMeshProUGUI waitingStatusText = null;
    [SerializeField] private GameObject errorMessagePanel = null;
    [SerializeField] private TextMeshProUGUI errorMessageText = null;
    [SerializeField] private GameObject characterSelectPanel = null;
    [SerializeField] private TextMeshProUGUI playerNameTag = null;
    [SerializeField] private TextMeshProUGUI winLossText = null;

    [SerializeField] private GameObject logo = null;
    [SerializeField] private GameObject findOpponentButton = null;
    [SerializeField] private GameObject characterSelectButton = null;
    [SerializeField] private GameObject changeNameButton = null;
    [SerializeField] private GameObject changeNameText = null;
    [SerializeField] private GameObject winLoss = null;
    [SerializeField] private GameObject winLossBackground = null;

    private bool isConnecting = false;

    public static int winCount, loseCount;

    // Change this value if/when the game is updated. Players on different versions cannot play w/ each other.
    private const string gameVersion = "1.1";
    private const int maxPlayers = 2;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        SetUpName();
        SetUpWinLoss();
    }

    // Set up name upon launching game (automatically sets player's name to previously set name)
    private void SetUpName()
    {
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            string defaultName = "Player";
            PhotonNetwork.NickName = defaultName;
            playerNameTag.text = defaultName;
        }
        else
        {
            string storedName = PlayerPrefs.GetString("PlayerName");
            PhotonNetwork.NickName = storedName;
            playerNameTag.text = storedName;
        }
    }

    // Retrieves # of games won/lost/drawn from PlayerPrefs
    private void SetUpWinLoss()
    {
        winCount = 0;
        loseCount = 0;

        if(PlayerPrefs.HasKey("Wins"))
        {
            winCount = PlayerPrefs.GetInt("Wins");
        }
        if(PlayerPrefs.HasKey("Losses"))
        {
            loseCount = PlayerPrefs.GetInt("Losses");
        }

        winLossText.text = "<color=#1FBF00>Wins: " + winCount + "</color>\n<color=red>Losses: " + loseCount + "</color>\n";
    }

    // Called when the Find Opponent Button is clicked. Uses tweens to remove all UI elements and then begins searching
    // for an opponent to battle.
    public void OnClickFindOpponent()
    {
        UnityEvent onCompleted = new UnityEvent();
        onCompleted.AddListener(FindOpponent);

        CloseAllUI(onCompleted);
    }

    // Search for and join a random room (which may or may not contain another player)
    public void FindOpponent()
    {
        // If the player has not yet selected a character, do not let them find a match.
        if(CharacterManager.playerCharacter == null)
        {
            errorMessageText.text = "Please select a character first";
            errorMessagePanel.SetActive(true);
            return;
        }
        isConnecting = true;

        // The main menu buttons becomes invisible while the status message becomes visible.
        logo.SetActive(false);
        findOpponentPanel.SetActive(false);
        characterSelectPanel.SetActive(false);
        waitingStatusPanel.SetActive(true);

        waitingStatusText.text = "Searching...";

        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // Called upon successfully connecting to Photon
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");

        if(isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    // Called upon being disconnected from Photon
    public override void OnDisconnected(DisconnectCause cause)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        waitingStatusPanel.SetActive(false);
        logo.SetActive(true);
        findOpponentPanel.SetActive(true);
        characterSelectPanel.SetActive(true);

        Debug.Log($"Disconnected due to: {cause}");
    }

    // Called when there are no random rooms to join. This method creates a room.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No clients are waiting for an opponent. Creating a new room.");

        // Creates a room with a null name and maximum player limit = 2
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayers });
    }

    // Called when the player joins a room.
    public override void OnJoinedRoom()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        // If the room does not have 2 players, then display the waiting message. 
        // Otherwise, display the opponent found message
        if(playerCount != maxPlayers)
        {
            waitingStatusText.text = "Waiting For Opponent";
        }
        else
        {
            waitingStatusText.text = "Opponent Found";
        }
    }

    // Called when someone else joins your room (your opponent)
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // If there are now 2 players in the room, close the room
        if(PhotonNetwork.CurrentRoom.PlayerCount == maxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            waitingStatusText.text = "Opponent Found";

            // Both players enter the BattleScene scene
            PhotonNetwork.LoadLevel("BattleScene");
        }
    }

    // Closes all UI elements on Main Menu (with tweens)
    public void CloseAllUI(UnityEvent onCompleted)
    {
        logo.GetComponent<TweenUI>().CloseElement(null);
        findOpponentButton.GetComponent<TweenUI>().CloseElement(null);
        characterSelectButton.GetComponent<TweenUI>().CloseElement(null);
        changeNameButton.GetComponent<TweenUI>().CloseElement(null);
        changeNameText.GetComponent<TweenUI>().CloseElement(null);
        errorMessagePanel.GetComponent<TweenUI>().CloseElement(null);
        winLoss.GetComponent<TweenUI>().CloseElement(null);
        winLossBackground.GetComponent<TweenUI>().CloseElement(onCompleted);
    }
}
