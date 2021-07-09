using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoll : MonoBehaviour
{
    public GameObject redImage, greenImage, hImage, twoImage; // dice results
    public GameObject dialog;
    public string result;

    GameObject[] dice;
    int Buildd6;


    public GameObject node, castleBase, player;

    public int CASTLE_COST = 2000;


    public GameObject goldSFX; // gold bag SFX


    public void Start()
    {
        result = "null";
        dice = new GameObject[] { redImage, greenImage, hImage, greenImage, twoImage, greenImage };

    }

    public void RollToBuild()
    {
         
         
        Buildd6 = Random.Range(0, 6);

        if (Buildd6 == 1 || Buildd6 == 3 || Buildd6 == 5)
        {
            result = "build";
            node.GetComponent<Node>().buildCastle(castleBase);
            player.GetComponent<PlayerControls>().money -= CASTLE_COST;
            goldSFX.GetComponent<AudioSource>().Play();

        }
        else if (Buildd6 == 0)
        {
            result = "rejected";
        }
        else if (Buildd6 == 2)
        {
            result = "free";
            node.GetComponent<Node>().buildCastle(castleBase);
             
        }
        else
        {
            result = "double";
            node.GetComponent<Node>().buildCastle(castleBase);
            player.GetComponent<PlayerControls>().money -= 2*CASTLE_COST;
            goldSFX.GetComponent<AudioSource>().Play();
        }

        dice[Buildd6].SetActive(true);
        Invoke(nameof(CloseDialog), 2);
    }

    public void CloseDialog()
    {
        dice[Buildd6].SetActive(false);
        dialog.SetActive(false);
        result = "null";
        player.GetComponent<PlayerControls>().shallBuild = false;
        player.GetComponent<PlayerControls>().mainCamera.GetComponent<CameraControls>().buildView = false;
        player.GetComponent<PlayerControls>().buildDialog.SetActive(false);
        // reinitialize the current state of the game (used for game manager)
        player.GetComponent<PlayerControls>().gameManager.GetComponent<GameManager>().currentState = 0;
    }
}
