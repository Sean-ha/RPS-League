using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The default character class which is the parent of all specific playable characters.
// Framework for all characters.
public class Character
{
    public int maxHealth;
    public int currentHealth;
    public int spriteID;
    public int characterID;

    public Character()
    {
        maxHealth = 1;
        currentHealth = 1;
        spriteID = 0;
        characterID = 0;
    }
}

// All characters are below:
public class MysteriousCreature : Character
{
    public MysteriousCreature()
    {
        maxHealth = 10;
        currentHealth = 10;
        spriteID = 1;
        characterID = 1;
    }
}

public class Angel : Character
{
    public Angel()
    {
        maxHealth = 10;
        currentHealth = 10;
        spriteID = 2;
        characterID = 2;
    }
}

public class Wretch : Character
{
    public Wretch()
    {
        maxHealth = 10;
        currentHealth = 10;
        spriteID = 3;
        characterID = 3;
    }
}

public class Ant : Character
{
    public Ant()
    {
        maxHealth = 10;
        currentHealth = 10;
        spriteID = 4;
        characterID = 4;
    }
}

public class Crimson : Character
{
    public Crimson()
    {
        maxHealth = 10;
        currentHealth = 10;
        spriteID = 5;
        characterID = 5;
    }
}

public class Phoenix : Character
{
    public Phoenix()
    {
        maxHealth = 10;
        currentHealth = 10;
        spriteID = 6;
        characterID = 6;
    }
}

public class Fairy : Character
{
    public Fairy()
    {
        maxHealth = 10;
        currentHealth = 10;
        spriteID = 7;
        characterID = 7;
    }
}

public class Protagonist : Character
{
    public Protagonist()
    {
        maxHealth = 10;
        currentHealth = 10;
        spriteID = 8;
        characterID = 8;
    }
}

public class Nurse : Character
{
    public Nurse()
    {
        maxHealth = 10;
        currentHealth = 10;
        spriteID = 9;
        characterID = 9;
    }
}

public class Scholar : Character
{
    public Scholar()
    {
        maxHealth = 10;
        currentHealth = 10;
        spriteID = 10;
        characterID = 10;
    }
}

public class Executioner : Character
{
    public Executioner()
    {
        maxHealth = 10;
        currentHealth = 10;
        spriteID = 11;
        characterID = 11;
    }
}