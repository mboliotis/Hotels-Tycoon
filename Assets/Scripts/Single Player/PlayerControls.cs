using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControls : MonoBehaviour
{
    /*
     * This Class contain informations about player's movement,
     * player's money
     */


    public Animator playerAnimCtrl;

    public GameObject player, gameManager, mainCamera;
    public Transform targetLocation; // Destination Node
    public GameObject[] nodes;  // Board Nodes

    // movement variables
    public bool mov = false; // Movement Flag
    public int steps; // D6 result
    public int currentLoc; // Current Node
    
    // money varaibles
    public int money; // Player's Money
    public TextMeshProUGUI displayMoney;


    // build variables
    public bool shallBuild, shallBuy;
    

    // build dialog
    public GameObject buildDialog;
    public GameObject buildRoll;
    

    public void Start()
    {
        money = 2000;
        currentLoc = 1;
        steps = 0;
        player.transform.position = nodes[0].transform.position; // player's starting position

        shallBuild = true;
        shallBuy = true;

        buildDialog.SetActive(false);
    }



    // Update is called once per frame
    public void Update()
    {
        

        // count how many steps remaining from the dice roll
        // if that number is zero stop moving
        if ( steps > 0)
        {
            mov = true;
            shallBuild = true;
            shallBuy = true;
        }

        if (gameManager.GetComponent<GameManager>().currentState == 2)
        {
            mainCamera.GetComponent<CameraControls>().buildView = true;
        }
        else
        {
            if (mov)
            {
                // walking animation
                playerAnimCtrl.SetBool("mov", true);

                // if the current node-number is equal to end-node then i am at the beggining
                if (currentLoc == nodes.Length)
                {
                    currentLoc = 0;
                    money = money + 2000;
                }

                // dice destination node
                targetLocation = nodes[currentLoc].transform;
                // moving towards destination node
                player.transform.position = Vector3.MoveTowards(player.transform.position, targetLocation.position, 5 * Time.deltaTime);
                // rotate the player to look at the node he moves towards
                player.transform.LookAt(targetLocation.position);

                // while walking, if reach one node go for the next until steps is zero
                if (Vector3.Distance(player.transform.position, targetLocation.position) < 0.01f)
                {
                    steps = steps - 1;

                    // if steps zero then check the current node type
                    if (steps == 0)
                    {
                        mov = false;

                    }

                    currentLoc = currentLoc + 1;
                }
            }
            else
            {
                playerAnimCtrl.SetBool("mov", false);

                if (nodes[currentLoc - 1].CompareTag("Build"))
                {

                    if (shallBuild && player.GetComponent<PlayerProperty>().plotsOwned.Count > 0)
                    {
                        mainCamera.GetComponent<CameraControls>().buildView = true;
                        buildDialog.SetActive(true);


                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit, 10000f))
                            {
                                if (hit.collider.gameObject.CompareTag("castle_base"))
                                {

                                    if (player.GetComponent<PlayerProperty>().plotsOwned.Contains(hit.collider.gameObject.transform.parent.name.ToUpper()))
                                    {
                                        DisplayBuildDice(nodes[currentLoc - 1], hit.collider.gameObject, player);


                                    }
                                    else
                                    {
                                        // do something
                                    }

                                }


                            }

                        }

                    }
                    else
                    {
                        gameManager.GetComponent<GameManager>().currentState = 0;

                    }

                }
                else if (nodes[currentLoc - 1].CompareTag("Buy")) // if the node is a Buy Node
                {
                    if (shallBuy)
                    {
                        // enable node menu
                        foreach (Transform child in nodes[currentLoc - 1].transform)
                        {
                            if (child.CompareTag("BuyDialog"))
                            {
                                child.gameObject.SetActive(true);
                            }
                        }
                        // reinitialize the current state of the game (used for game manager)
                        gameManager.GetComponent<GameManager>().currentState = 0;
                        shallBuy = false;
                    }
                }
                else
                {
                    gameManager.GetComponent<GameManager>().currentState = 0;
                }
            }
        }
        
        


        DisplayMoney();

    }

    // this method display the player's money on the screen
    public void DisplayMoney()
    {
        displayMoney.SetText("{0}", money);
    }


    public void DisplayBuildDice(GameObject _node, GameObject _castleBase, GameObject _player)
    {
        buildRoll.SetActive(true);
        buildRoll.GetComponent<BuildRoll>().node = _node;
        buildRoll.GetComponent<BuildRoll>().castleBase = _castleBase;
        buildRoll.GetComponent<BuildRoll>().player = _player;

    }


    public void HideBuildDice()
    {
        buildRoll.SetActive(false);
    }


}
