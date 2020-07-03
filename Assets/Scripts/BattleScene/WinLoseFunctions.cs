using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseFunctions : MonoBehaviour
{
    public string winText;
    public string loseText;
    public string drawText;

    public static int scholarCount;
    public static int executionerCount;
    public static bool phoenixRevived;
    public static int creatureCount;

    private void Start()
    {
        winText = "";
        loseText = "";
        drawText = "";

        scholarCount = 0;
        executionerCount = 0;
        phoenixRevived = false;
        creatureCount = 0;
    }

    // Each id corresponds to a different character's win function.
    // A PlayerManager is passed in so the method may edit its values
    // Returns the amount of damage to deal.
    // Battle dialogue is placed into public variable winText for access in BattleManager.
    public int Win(int id, PlayerManager winnerManager, int winnerChoice)
    {
        string screenName = winnerManager.screenName;
        winText = "";

        // Creature
        if (id == 1)
        {
            creatureCount++;
            // If you used scissors and you have more than 1 stack of TERROR
            if (winnerChoice == 3)
            {
                winnerManager.health += creatureCount;
                if (winnerManager.health > 10)
                {
                    winnerManager.health = 10;
                }
                winText = screenName + " heals for " + creatureCount + " health.";
                creatureCount = 0;
            }
            return 2;
        }
        // Angel
        else if (id == 2)
        {
            if (winnerChoice == 2)
            {
                winnerManager.health += 2;
                winText = screenName + " heals 2 health for winning with PAPER.";
            }
            else
            {
                winnerManager.health += 1;
                winText = screenName + " heals 1 health.";
            }
            return 2;
        }
        // Wretch
        else if (id == 3)
        {
            if (winnerChoice == 1)
            {
                winText = screenName + " deals +2 damage for winning with ROCK.";
                return 4;
            }
            else
            {
                winText = screenName + " deals +1 damage.";
                return 3;
            }
        }
        // Ant
        else if (id == 4)
        {
            winText = screenName + " deals double damage.";
            return 4;
        }
        // Crimson
        else if (id == 5)
        {
            if (winnerChoice != 3)
            {
                winText = screenName + " loses 1 health to deal +2 damage.";
                winnerManager.health -= 1;
                return 4;
            }
            else
            {
                winText = screenName + " deals +2 damage because it used SCISSORS.";
                return 4;
            }
        }
        // Phoenix
        else if (id == 6)
        {
            return 2;
        }
        // Fairy
        else if (id == 7)
        {
            winText = screenName + "'s health is set to 5 for winning.";
            winnerManager.health = 5;
            return 2;
        }
        // Protagonist
        else if (id == 8)
        {
            return 2;
        }
        // Nurse
        else if (id == 9)
        {
            if (winnerChoice == 2)
            {
                winText = screenName + " heals 1 health for winning with PAPER.";
                winnerManager.health += 1;
                return 2;
            }
            else if (winnerChoice == 3)
            {
                winText = screenName + " deals +1 damage for winning with SCISSORS.";
                return 3;
            }
            return 2;
        }
        // Scholar
        else if (id == 10)
        {
            if (winnerChoice == 2)
            {
                scholarCount++;
                if (scholarCount == 1)
                {
                    winText = screenName + " has 1 stack of BRAIN POWER.";
                }
                else if (scholarCount == 2)
                {
                    winText = screenName + " has 2 stacks of BRAIN POWER.";
                }
                else if (scholarCount == 3)
                {
                    winText = screenName + " has 3 stacks of BRAIN POWER! He unleashes a devastating blow!";
                    scholarCount = 0;
                    return 999;
                }
            }
            return 2;
        }
        // Executioner
        else if (id == 11)
        {
            executionerCount++;
            winText = screenName + " has " + executionerCount + " power.";
            return executionerCount;
        }

        return 2;
    }

    // Lose functions similarly to the Win function above. No return value.
    // Loser's PlayerManager is passed in.
    public void Lose(int id, int damageToTake, PlayerManager loserManager, int loserChoice)
    {
        loseText = "";
        string screenName = loserManager.screenName;

        // Creature
        if (id == 1)
        {
            creatureCount++;
            loseText = screenName + " is at " + creatureCount + "stacks of TERROR.";
        }
        // Ant
        else if (id == 4)
        {
            if (loserChoice == 3)
            {
                loseText = screenName + " lost against ROCK and got squished.";
                loserManager.health = 0;
                return;
            }
        }

        // Default lose function for characters w/o special lose effects
        loserManager.health -= damageToTake;
        loseText = loserManager.screenName + " takes " + damageToTake + " damage.";

        // Phoenix
        if (id == 6)
        {
            if (loserManager.health <= 0)
            {
                if (!phoenixRevived)
                {
                    winText = screenName + " is GLORIOUSLY REBORN with 5 health remaining.";
                    loserManager.health = 5;
                    phoenixRevived = true;
                }
            }
        }
    }

    // Called upon a draw happening. Only Protagonists (id=8) have any functionality in a draw.
    public int Draw(PlayerManager playerPM)
    {
        // Creature
        if(playerPM.characterID == 1)
        {
            creatureCount++;
            drawText += playerPM.screenName + " has " + creatureCount + " stacks of TERROR. ";
        }
        else if (playerPM.characterID == 8)
        {
            drawText += playerPM.screenName + " deals 1 damage. ";
            return 1;
        }
        return 0;
    }
}
