using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCam : MonoBehaviour
{
    public GameObject player;


    private void Start()
    {
        Invoke("Death", 1);
    }


    // Update is called once per frame
    void Update()
    {
        
        this.gameObject.transform.Translate(Vector3.right * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(player.transform);
    }


    public void Death()
    {
        player.GetComponent<Animator>().SetBool("die", true);
    }

}
