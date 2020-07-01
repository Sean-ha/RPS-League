using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{

    [SerializeField] private TMP_InputField nameInputField = null;
    [SerializeField] private Button button = null;

    private const string playerPrefsNameKey = "PlayerName";

    void Start()
    {
        SetUpInputField();

        //Adds a listener to the main input field and invokes "EnableButton" when the value changes.
        nameInputField.onValueChanged.AddListener(delegate { EnableButton(nameInputField.text); });
    }

    private void SetUpInputField()
    {
        // If there is no stored name (i.e. it's the player's first time opening the game), then leave a blank input field
        if (!PlayerPrefs.HasKey(playerPrefsNameKey)) { return; }

        // Otherwise, fill in the input field with the last name the player used
        string storedName = PlayerPrefs.GetString(playerPrefsNameKey);

        nameInputField.text = storedName;

        EnableButton(storedName);
    }

    // Called upon loading the stored name, and whenever the input field is changed.
    public void EnableButton(string name)
    {
        // The button can be pressed if the name is neither null nor empty. Otherwise, the button is disabled.
        button.interactable = !string.IsNullOrEmpty(name);
    }

    // Called when the button is pressed.
    public void SavePlayerName()
    {
        // The inputted name is saved to PlayerPrefs and on the Photon Network.
        string playerName = nameInputField.text;

        PhotonNetwork.NickName = playerName;
        PlayerPrefs.SetString(playerPrefsNameKey, playerName);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
