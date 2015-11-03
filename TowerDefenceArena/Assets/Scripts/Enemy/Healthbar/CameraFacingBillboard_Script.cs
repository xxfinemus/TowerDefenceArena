using UnityEngine;
using System.Collections;

/// <summary>
/// Code taken almost word for word from http://wiki.unity3d.com/index.php?title=CameraFacingBillboard
/// </summary>
public class CameraFacingBillboard_Script : MonoBehaviour
{

    public Camera mainCam;

    // Sets the camera to automatically be the default main camera, set one manually to override this
    void Start()
    {
        if(mainCam == null)
        {
            mainCam = Camera.main;
        }
    }

    // Forces the object the script is attached to to face the given camera at all times
    void Update()
    {
        transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.forward, mainCam.transform.rotation * Vector3.up);
    }
}
