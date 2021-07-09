using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

    public TextMeshProUGUI self;
    public GameObject target;

    bool pause = false;
    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                pause = false;
            }
            else
            {
                pause = true;
            }
        }

        if (!pause)
        {
            self.rectTransform.position = Vector2.Lerp(self.rectTransform.position, target.transform.position, 0.001f);

            if (self.rectTransform.position.y > 1600)
            {
                Invoke("MainMenu", 2);
            }

        }




    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
