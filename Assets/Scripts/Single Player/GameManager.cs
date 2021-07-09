using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI d6Result;
    public Button d6Roll;
    public int currentState;  // states: 0 -> roll and move, 1 -> build, 2 -> buy gates
    public GameObject player;
    public int d6;
    public int[] plotsPrices;


    // Menu Objects
    public GameObject menu;  // exit menu

    public GameObject goldSFX;

    public void Start()
    {
        plotsPrices = new int[5];
        plotsPrices[0] = 500;
        plotsPrices[1] = 500;
        plotsPrices[2] = 500;
        plotsPrices[3] = 500;
        plotsPrices[4] = 500;

        currentState = 0;

        menu.SetActive(false);
    }

    public void Update()
    {
        if (currentState == 0)
        {
            d6Roll.enabled = true;
        }
        else if (currentState == 1)
        {
            d6Roll.enabled = false;
            
        }

        if (player.GetComponent<PlayerControls>().money <= 0)
        {
            Invoke("Die", 1);
        }

    }


    public void Die()
    {
        SceneManager.LoadScene(2);
    }



    public void Roll()
    {
        d6 = Random.Range(1, 7);
        d6Result.SetText("Dice Result: {0}", d6);
        currentState = 1;
        ResolveTheMove();
    }


    public void ResolveTheMove()
    {

        player.GetComponent<PlayerControls>().steps = d6;
    }


    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void BagOfGoldSFX()
    {
        goldSFX.GetComponent<AudioSource>().Play();
    }
}
