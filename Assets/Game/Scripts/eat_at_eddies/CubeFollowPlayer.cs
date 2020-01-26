using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFollowPlayer : MonoBehaviour
{
    private Vector3 dragPoint, playerPosition;
    private GameObject dragPointPrefab;
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.R))
        {
            dragPoint = transform.position;
            playerPosition = GameObject.FindGameObjectWithTag("player").transform.position;
            transform.position = playerPosition;
        }
    }

}
