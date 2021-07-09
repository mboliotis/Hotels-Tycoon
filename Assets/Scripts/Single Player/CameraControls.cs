using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public GameObject cameraLocationToBuild;
    public GameObject mainCamera;

    public float mouseSensitivity = 1.0f;
   

    public bool buildView;

    public GameObject previousCameraTransform;
    public bool isReset;


    // Start is called before the first frame update
    void Start()
    {
        buildView = false;
        isReset = false;
        mainCamera.transform.position = previousCameraTransform.transform.position;
        mainCamera.transform.rotation = previousCameraTransform.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (buildView)
        {
            
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraLocationToBuild.transform.position, 0.1f);
            mainCamera.transform.rotation = cameraLocationToBuild.transform.rotation;
            isReset = false;
        }
        else if(!isReset)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, previousCameraTransform.transform.position, 0.05f); ;
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, previousCameraTransform.transform.rotation, 0.05f);
        }

        
    }
    
 
}
