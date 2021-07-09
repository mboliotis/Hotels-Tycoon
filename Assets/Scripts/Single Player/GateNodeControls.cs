using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateNodeControls : MonoBehaviour
{

    public GameObject gameManager;

    public void OnTriggerEnter(Collider other)
    {
        gameManager.GetComponent<GameManager>().currentState = 2;
        Debug.Log(other.gameObject.name);
    }
}
