using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab = null;
    [SerializeField] private GameObject roundPanel = null;
    [SerializeField] private TextMeshProUGUI roundText = null;
    [SerializeField] private GameObject rpsButtonsPanel = null;
    [SerializeField] private TextMeshProUGUI timerText = null;
    [SerializeField] private TextMeshProUGUI battleDialogue = null;
    [SerializeField] private WinLoseFunctions winLoseFunctions = null;
    [SerializeField] private TextMeshProUGUI roundField = null;
    [SerializeField] private TextMeshProUGUI endOfGameText = null;
    [SerializeField] private GameObject endOfGamePanel = null;
    [SerializeField] private GameObject selected = null;

    [SerializeField] private GameObject masterMove = null;
    [SerializeField] private GameObject otherMove = null;

    private GameObject masterPlayer, otherPlayer;

    public static int roundNumber = 1;
    private int timer;

    public static int playerChoice = 0;

    public static int masterChoice = 0, otherChoice = 0;

    public static PlayerManager masterPM, otherPM;

    public Animator animator;

    private Sprite[] rpsSprites;

    void Start()
    {
        // Places the two players on a side of the screen and begins the round
        if (PhotonNetwork.IsMasterClient)
        {
            masterPlayer = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(-1.8f, 1.8f, 0), Quaternion.identity);
            masterPM = masterPlayer.GetComponent<PlayerManager>();
        }
        else
        {
            otherPlayer = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(1.8f, 1.8f, 0), Quaternion.identity); otherPM = otherPlayer.GetComponent<PlayerManager>();
            otherPM = otherPlayer.GetComponent<PlayerManager>();
        }

        rpsSprites = Resources.LoadAll<Sprite>("RPSButtons");

        BeginRound();
    }

    private void BeginRound()
    {
        roundField.text = "";
        masterChoice = 0;
        otherChoice = 0;
        timerText.text = "";
        // Moves the selection cursor off the screen
        selected.GetComponent<RectTransform>().anchoredPosition = new Vector3(650, 0, 0);
        // Displays the current round
        roundText.text = "Round " + roundNumber;
        StartCoroutine(DisplayRoundStart());
    }

    IEnumerator DisplayRoundStart()
    {
        // Displays the round number for 3 seconds before disappearing
        roundPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        roundPanel.SetActive(false);
        DisplayControls();
    }

    private void DisplayControls()
    {
        timer = 16;
        roundField.text = "Round " + roundNumber;

        // RPS buttons are displayed for user to press
        rpsButtonsPanel.SetActive(true);

        StartCoroutine(TimerCountDown());
    }

    IEnumerator TimerCountDown()
    {
        // Lets the player pick an option. 0 is nothing, 1 is rock, 2 is paper, 3 is scissors.
        playerChoice = 0;

        while (timer != 0)
        {
            timer--;
            timerText.text = timer + "";

            if (PhotonNetwork.IsMasterClient)
            {
                masterChoice = playerChoice;
                masterPM.GetValuesFromBattleManager(playerChoice);
            }
            else
            {
                otherChoice = playerChoice;
                otherPM.GetValuesFromBattleManager(playerChoice);
            }

            yield return new WaitForSeconds(1);
        }

        // RPS buttons no longer shown
        rpsButtonsPanel.SetActive(false);

        // If the player doesn't pick an option, pick a random
        if (playerChoice == 0)
        {
            playerChoice = Random.Range(1, 4);
        }

        // Sends the necessary values to PlayerManager for sending thru photon network.
        if (PhotonNetwork.IsMasterClient)
        {
            masterChoice = playerChoice;
            masterPM.GetValuesFromBattleManager(playerChoice);
            masterPM.ready = true;
        }
        else
        {
            otherChoice = playerChoice;
            otherPM.GetValuesFromBattleManager(playerChoice);
            otherPM.ready = true;
        }

        while (!masterPM.ready || !otherPM.ready)
        {
            // Checking for disconnects during match
            GameObject[] managers = GameObject.FindGameObjectsWithTag("PlayerManagers");
            if(managers.Length != 2)
            {
                endOfGameText.text = "YOU WIN";
                CanvasGroup canvasGroup = endOfGamePanel.GetComponent<CanvasGroup>();
                endOfGamePanel.SetActive(true);

                MainMenuManager.winCount++;
                PlayerPrefs.SetInt("Wins", MainMenuManager.winCount);
                StartCoroutine(FadePanel(canvasGroup));
                break;
            }

            yield return new WaitForSeconds(1);
        }

        if (masterPM.ready && otherPM.ready)
        {
            ProcessRound();
        }
    }

    // Determines the winner of the round
    public void ProcessRound()
    {
        DisplayMoves();
        // Draw
        if (masterChoice == otherChoice)
        {
            string drawText = "It's a draw. ";
            winLoseFunctions.drawText = "";
            int masterDraw = winLoseFunctions.Draw(masterPM);
            int otherDraw = winLoseFunctions.Draw(otherPM);

            drawText += winLoseFunctions.drawText;

            if (masterDraw != 0)
            {
                winLoseFunctions.Lose(otherPM.characterID, masterDraw, otherPM, 0);
                drawText += winLoseFunctions.loseText;
            }
            if (otherDraw != 0)
            {
                winLoseFunctions.Lose(masterPM.characterID, otherDraw, masterPM, 0);
                drawText += winLoseFunctions.loseText;
            }

            StartCoroutine(TypeSentence(drawText));
        }
        else if ((masterChoice % 3) + 1 == otherChoice)
        {
            // Non-master client wins
            WinFunction(false, masterChoice, otherChoice);
        }
        else if ((otherChoice % 3) + 1 == masterChoice)
        {
            // Master client wins
            WinFunction(true, masterChoice, otherChoice);
        }

        StartCoroutine(EndOfRound());
    }

    // Displays an image of Rock, Paper, or Scissors for each player depicting their choice for that round.
    public void DisplayMoves()
    {
        masterMove.GetComponent<SpriteRenderer>().sprite = rpsSprites[masterChoice - 1];
        otherMove.GetComponent<SpriteRenderer>().sprite = rpsSprites[otherChoice - 1];

        masterMove.SetActive(true);
        otherMove.SetActive(true);
    }

    // masterWon is a parameter that tells the method if the master client or the non-master client won.
    private void WinFunction(bool masterWon, int masterChoice, int otherChoice)
    {
        string winnerName;
        int damageToDeal;
        string battleText;

        if (masterWon)
        {
            winnerName = masterPM.screenName;
            damageToDeal = winLoseFunctions.Win(masterPM.characterID, masterPM, masterChoice);

            battleText = winnerName + " won the round. ";
            battleText += winLoseFunctions.winText;

            if (damageToDeal != 0)
            {
                winLoseFunctions.Lose(otherPM.characterID, damageToDeal, otherPM, otherChoice);
                battleText += " " + winLoseFunctions.loseText;
            }
        }
        else
        {
            winnerName = otherPM.screenName;
            damageToDeal = winLoseFunctions.Win(otherPM.characterID, otherPM, otherChoice);

            battleText = winnerName + " won the round. ";
            battleText += winLoseFunctions.winText;

            if (damageToDeal != 0)
            {
                winLoseFunctions.Lose(masterPM.characterID, damageToDeal, masterPM, masterChoice);
                battleText += " " + winLoseFunctions.loseText;
            }
        }
        StartCoroutine(TypeSentence(battleText));
    }

    IEnumerator EndOfRound()
    {
        // Simply waits for 8 seconds after the round is over for the players to read the information
        // After 8 seconds, begin the new round (or end the game if it's over).
        yield return new WaitForSeconds(8);
        masterPM.ready = false;
        otherPM.ready = false;

        // If either player is below 0 health, then the game is over.
        if (masterPM.health <= 0 || otherPM.health <= 0)
        {
            // If both players are below 0 health, it is a draw.
            if (masterPM.health <= 0 && otherPM.health <= 0)
            {
                endOfGameText.text = "DRAW";
                MainMenuManager.drawCount++;
            }
            else if (masterPM.health <= 0)
            {
                // Master client loses
                if (PhotonNetwork.IsMasterClient)
                {
                    endOfGameText.text = "YOU LOSE";
                    MainMenuManager.loseCount++;
                }
                else
                {
                    endOfGameText.text = "YOU WIN";
                    MainMenuManager.winCount++;
                }
            }
            else if (otherPM.health <= 0)
            {
                // Non-master client loses
                if (PhotonNetwork.IsMasterClient)
                {
                    endOfGameText.text = "YOU WIN";
                    MainMenuManager.winCount++;
                }
                else
                {
                    endOfGameText.text = "YOU LOSE";
                    MainMenuManager.loseCount++;
                }
            }
            // Saves won/lost/draw counts to PlayerPrefs
            PlayerPrefs.SetInt("Wins", MainMenuManager.winCount);
            PlayerPrefs.SetInt("Losses", MainMenuManager.loseCount);
            PlayerPrefs.SetInt("Draws", MainMenuManager.drawCount);

            // Fades the end of game panel in.
            CanvasGroup canvasGroup = endOfGamePanel.GetComponent<CanvasGroup>();
            endOfGamePanel.SetActive(true);
            StartCoroutine(FadePanel(canvasGroup));
        }
        else
        {
            roundNumber += 1;
            BeginRound();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        battleDialogue.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            battleDialogue.text += letter;
            yield return null;
        }
    }

    // Fades the panel in
    IEnumerator FadePanel(CanvasGroup canvasGroup)
    {
        // Fades out the canvases associated with each PlayerManager.
        GameObject[] canvases = GameObject.FindGameObjectsWithTag("PlayerManagerCanvas");
        foreach (GameObject canvasObject in canvases)
        {
            canvasObject.GetComponent<CanvasGroup>().alpha = 0.2f;
        }

        float counter = 0f;

        while (counter < 0.8f)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = counter;

            yield return null;
        }

        animator.SetBool("IsOffScreen", false);
    }

    // When your opponent leaves the room, win the game.
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        MainMenuManager.winCount++;
        PlayerPrefs.SetInt("Wins", MainMenuManager.winCount);

        // Fades the end of game panel in.
        endOfGameText.text = "YOU WIN";
        CanvasGroup canvasGroup = endOfGamePanel.GetComponent<CanvasGroup>();
        endOfGamePanel.SetActive(true);
        StartCoroutine(FadePanel(canvasGroup));
    }
}
