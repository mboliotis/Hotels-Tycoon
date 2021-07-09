using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class DialogControls : MonoBehaviour
{
    public GameObject dialog; //self refference
    public GameObject gameManager; // the game manager holds data about the game like dices etc
    public GameObject player; // the main piece that moves on the board

    

    /*
     * this method will append the plots name at player's properties 
     * and substract the price of the plot from player's money
     */
    public void BuyPlot()
    {
        string plotName;
        
        // get plot's name
        plotName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        
        if (player.GetComponent<PlayerProperty>().plotsOwned.Contains(plotName))
        {
            CloseDialog();
            return;
        }

        // append the plot to the player's propeties
        player.GetComponent<PlayerProperty>().plotsOwned.Add(plotName);

        int plotPrice = gameManager.GetComponent<GameManager>().plotsPrices[ResolvePlotNumber(plotName) - 1];
        // substract the price from player's money
        player.GetComponent<PlayerControls>().money = player.GetComponent<PlayerControls>().money - plotPrice;

        gameManager.GetComponent<GameManager>().BagOfGoldSFX();

        CloseDialog();
    }


    // this method will close the dialog box
    public void CloseDialog()
    {
        this.gameObject.SetActive(false);
    }


    private int ResolvePlotNumber(string plotName)
    {
        return (int)Char.GetNumericValue(plotName[plotName.Length - 1]);
    }


}
