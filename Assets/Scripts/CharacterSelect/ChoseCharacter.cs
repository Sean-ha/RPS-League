using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoseCharacter : MonoBehaviour
{
    public GameObject scrollArea = null;

    public GameObject descriptionPanel = null;
    public GameObject characterImage = null;
    public TextMeshProUGUI abilityDescription = null;
    public Button selectCharacterButton = null;
    public TextMeshProUGUI characterName = null;

    private Sprite[] characterSpriteList;

    private void Awake()
    {
        characterSpriteList = Resources.LoadAll<Sprite>("CharacterSprites");
    }

    // Called when the user selects Character1
    public void ChooseCharacter()
    {
        CharacterManager.playerCharacter = new Character();
        SetDescriptionPanel();
    }

    public void ChooseCharacter1()
    {
        SetDescriptionPanel();
        characterImage.GetComponent<Image>().sprite = characterSpriteList[1];
        abilityDescription.text = "Every round, acquire 1 stack of TERROR.\nUpon winning with SCISSORS, use all of your " +
            "stacks and heal for that amount.\nThis cannot overheal. This can be activated only once per game.";
        characterName.text = "Creature";

        selectCharacterButton.GetComponent<Button>().onClick.AddListener(Char1);
    }

    public void ChooseCharacter2()
    {
        SetDescriptionPanel();
        characterImage.GetComponent<Image>().sprite = characterSpriteList[2];
        abilityDescription.text = "Heal 1 health upon winning a round.\nHeal 2 health if you win using PAPER.";
        characterName.text = "Angel";

        selectCharacterButton.GetComponent<Button>().onClick.AddListener(Char2);
    }

    public void ChooseCharacter3()
    {
        SetDescriptionPanel();
        characterImage.GetComponent<Image>().sprite = characterSpriteList[3];
        abilityDescription.text = "Deal +1 damage upon winning a round.\nDeal +2 damage if you win using ROCK.";
        characterName.text = "Wretch";

        selectCharacterButton.GetComponent<Button>().onClick.AddListener(Char3);
    }

    public void ChooseCharacter4()
    {
        SetDescriptionPanel();
        characterImage.GetComponent<Image>().sprite = characterSpriteList[4];
        abilityDescription.text = "Deal double damage.\nIf you lose against ROCK, instantly lose the game.";
        characterName.text = "Ant";

        selectCharacterButton.GetComponent<Button>().onClick.AddListener(Char4);
    }

    public void ChooseCharacter5()
    {
        SetDescriptionPanel();
        characterImage.GetComponent<Image>().sprite = characterSpriteList[5];
        abilityDescription.text = "Upon winning, take 2 damage to deal +2 damage.\n" +
            "If you win with SCISSORS, do not damage yourself.";
        characterName.text = "Crimson";

        selectCharacterButton.GetComponent<Button>().onClick.AddListener(Char5);
    }

    public void ChooseCharacter6()
    {
        SetDescriptionPanel();
        characterImage.GetComponent<Image>().sprite = characterSpriteList[6];
        abilityDescription.text = "The first time you go below 0 health, revive with 5 health remaining.";
        characterName.text = "Phoenix";

        selectCharacterButton.GetComponent<Button>().onClick.AddListener(Char6);
    }

    public void ChooseCharacter7()
    {
        SetDescriptionPanel();
        characterImage.GetComponent<Image>().sprite = characterSpriteList[7];
        abilityDescription.text = "Winning a round will set your current health to 5.";
        characterName.text = "Fairy";

        selectCharacterButton.GetComponent<Button>().onClick.AddListener(Char7);
    }

    public void ChooseCharacter8()
    {
        SetDescriptionPanel();
        characterImage.GetComponent<Image>().sprite = characterSpriteList[8];
        abilityDescription.text = "Deal 1 damage to your opponent if you draw.";
        characterName.text = "Protagonist";

        selectCharacterButton.GetComponent<Button>().onClick.AddListener(Char8);
    }

    public void ChooseCharacter9()
    {
        SetDescriptionPanel();
        characterImage.GetComponent<Image>().sprite = characterSpriteList[9];
        abilityDescription.text = "Upon winning with paper, heal for 1 health.\nUpon winning with scissors, deal +1 damage.";
        characterName.text = "Nurse";

        selectCharacterButton.GetComponent<Button>().onClick.AddListener(Char9);
    }

    public void ChooseCharacter10()
    {
        SetDescriptionPanel();
        characterImage.GetComponent<Image>().sprite = characterSpriteList[10];
        abilityDescription.text = "Gain a stack of BRAIN POWER if you win using PAPER.\nUpon reaching 3 stacks, utterly" +
            " annihilate your opponent.";
        characterName.text = "Scholar";

        selectCharacterButton.GetComponent<Button>().onClick.AddListener(Char10);
    }

    public void ChooseCharacter11()
    {
        SetDescriptionPanel();
        characterImage.GetComponent<Image>().sprite = characterSpriteList[11];
        abilityDescription.text = "Your first attack deals 1 damage.\nAll your attacks will deal 1 more damage" +
            " than your previous one.";
        characterName.text = "Executioner";

        selectCharacterButton.GetComponent<Button>().onClick.AddListener(Char11);
    }

    private void SetDescriptionPanel()
    {
        scrollArea.SetActive(false);
        descriptionPanel.SetActive(true);
    }

    private void Char1()
    {
        CharacterManager.playerCharacter = new MysteriousCreature();
    }

    private void Char2()
    {
        CharacterManager.playerCharacter = new Angel();
    }

    private void Char3()
    {
        CharacterManager.playerCharacter = new Wretch();
    }

    private void Char4()
    {
        CharacterManager.playerCharacter = new Ant();
    }

    private void Char5()
    {
        CharacterManager.playerCharacter = new Crimson();
    }

    private void Char6()
    {
        CharacterManager.playerCharacter = new Phoenix();
    }

    private void Char7()
    {
        CharacterManager.playerCharacter = new Fairy();
    }

    private void Char8()
    {
        CharacterManager.playerCharacter = new Protagonist();
    }

    private void Char9()
    {
        CharacterManager.playerCharacter = new Nurse();
    }

    private void Char10()
    {
        CharacterManager.playerCharacter = new Scholar();
    }

    private void Char11()
    {
        CharacterManager.playerCharacter = new Executioner();
    }
}
