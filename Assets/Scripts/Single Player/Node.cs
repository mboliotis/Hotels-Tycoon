using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public GameObject[] plots;

    public GameObject castle;

    

    public void buildCastle(GameObject castleBase)
    {
        Object.Instantiate(castle, castleBase.transform.position, castleBase.transform.rotation, castleBase.transform.parent);
        Object.Destroy(castleBase);
    }


}
