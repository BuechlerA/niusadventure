using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconRotation : MonoBehaviour
{
    public GameObject icon;
    public GameObject playersCamera;

    private float cameraRotation;

	void Update ()
    {
        if(icon.activeSelf)
        {
            cameraRotation = playersCamera.transform.eulerAngles.y;
            icon.transform.eulerAngles = new Vector3(transform.eulerAngles.x, cameraRotation, transform.eulerAngles.z);
        }

    }
}
