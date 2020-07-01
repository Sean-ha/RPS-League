using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSelect : MonoBehaviour
{
    // Called when the "Character Select" button on the main menu is pressed. Simply loads the character select screen.
    public void GoToCharacterSelect()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelect");
    }

    public void GoToChangeName()
    {
        UnityEvent onCompleted = new UnityEvent();
        onCompleted.AddListener(GoToChangeNameScene);

        GameObject.Find("MainMenuManager").GetComponent<MainMenuManager>().CloseAllUI(onCompleted);
    }

    public void GoToChangeNameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EnterName");
    }
}
