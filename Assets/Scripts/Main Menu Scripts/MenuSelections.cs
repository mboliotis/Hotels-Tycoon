using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelections : MonoBehaviour
{
    public void StartSinglePlayer( )
    {
        SceneManager.LoadScene("SinglePlayerMode");
    }

    public void Credits()
    {
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
