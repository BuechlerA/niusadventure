using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{


    void Update()
    {



        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 2);
            //transform.position += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * 2);
            //transform.position += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * 2);
            //transform.position += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * 2);
            //transform.position += new Vector3(1, 0, 0);
        }
    }



}
