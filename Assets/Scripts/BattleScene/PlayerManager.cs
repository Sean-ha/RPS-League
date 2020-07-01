using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerManager : MonoBehaviourPun, IPunObservable
{
    private SpriteRenderer spriteRenderer = null;
    public Sprite[] characterSpriteList;

    private bool spriteChanged = false;

    public Character myCharacter = CharacterManager.playerCharacter;

    private int playersMove = 0;

    public string screenName;

    public int health, maxHealth;
    public int characterID;

    public bool ready;

    private void Awake()
    {
        characterSpriteList = Resources.LoadAll<Sprite>("CharacterSprites");

        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        // Changes sprite for your player
        if(photonView.IsMine)
        {
            spriteRenderer.sprite = characterSpriteList[myCharacter.spriteID];
        }
        screenName = photonView.Owner.NickName;

        health = myCharacter.currentHealth;
        maxHealth = myCharacter.maxHealth;
        characterID = myCharacter.characterID;
        ready = false;
    }

    public void GetValuesFromBattleManager(int move)
    {
        if(photonView.IsMine)
        {
            // Set variables in PlayerManager equal to variables in BattleManager. Then send those variables over
            // Photon network to communicate.
            playersMove = move;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            // We own this player; send data to opponent
            stream.SendNext(myCharacter.spriteID);
            stream.SendNext(health);
            stream.SendNext(maxHealth);
            stream.SendNext(characterID);
            stream.SendNext(playersMove);
            stream.SendNext(ready);

            if (playersMove != 0)
            {
                playersMove = 0;
            }
        }
        else
        {
            // Opponent; receive data
            int spriteID = (int)stream.ReceiveNext();
            health = (int)stream.ReceiveNext();
            maxHealth = (int)stream.ReceiveNext();
            characterID = (int)stream.ReceiveNext();
            playersMove = (int)stream.ReceiveNext();
            ready = (bool)stream.ReceiveNext();

            // Changes opponent's player's sprite
            if(!spriteChanged)
            {
                spriteRenderer.sprite = characterSpriteList[spriteID];
                spriteChanged = true;
            }
            // Sends both players' moves to BattleManager for processing
            if(playersMove != 0)
            {
                if(PhotonNetwork.IsMasterClient)
                {
                    BattleManager.otherChoice = playersMove;
                }
                else
                {
                    BattleManager.masterChoice = playersMove;
                }
                playersMove = 0;
            }
        }
    }
}
