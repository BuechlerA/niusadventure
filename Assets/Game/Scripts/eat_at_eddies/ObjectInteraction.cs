using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    private Vector3 playerPosition, endPos;
    private float lerp = 0, duration = 1, carryDelay;
    private int triggerIdent = 0; // 0 = none, 1 = throw, 2 = push-pull
    //private float radiusOS = 1f; //OverlapSphere
    //private Collider[] colliders; //OverlapSphere
    private GameObject objectToMove, objectToThrow, lerpPoint;
    
    private Vector3 faceDirection;
    private Transform playerCameraDirection;

    public float velocityDebug;
    private float bla = 3f;

    private bool _isPushing = false;
    private bool _isCarrying = false;

    void Start()
    {
        playerCameraDirection = GameObject.Find("PlayerCam").transform;
    }

    void Update()
    {

        playerPosition = transform.position;
        faceDirection = Vector3.ProjectOnPlane(playerCameraDirection.forward, Vector3.up);



        if (Input.GetKey(KeyCode.E) && objectToMove != null)
        {
            gameObject.transform.SetParent(objectToMove.transform);
            gameObject.GetComponent<PlayerController>().enabled = false;

            if (lerp < 0.5f)
            {
                lerp += Time.deltaTime / duration;
                transform.position = Vector3.Lerp(playerPosition, endPos, lerp);
            }

            if (lerp >= 0.5f)
            {


                // If player stand in x direction
                if(lerpPoint.name == "x")
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        //objectToMove.transform.Translate(Vector3.left * Time.deltaTime * bla);
                        objectToMove.GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * 1000);
                        velocityDebug = objectToMove.GetComponent<Rigidbody>().velocity.magnitude;
                        _isPushing = true;

                        if (objectToMove.GetComponent<Rigidbody>().velocity.magnitude > 5)
                        {
                            objectToMove.GetComponent<Rigidbody>().velocity = objectToMove.GetComponent<Rigidbody>().velocity.normalized * 5;
                        }
                    }
                }


                // If player stand in -x direction
                if (lerpPoint.name == "-x")
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        //objectToMove.transform.Translate(Vector3.right * Time.deltaTime * bla);
                        objectToMove.GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * 1000);
                        velocityDebug = objectToMove.GetComponent<Rigidbody>().velocity.magnitude;
                        _isPushing = true;

                        if (objectToMove.GetComponent<Rigidbody>().velocity.magnitude > 5)
                        {
                            objectToMove.GetComponent<Rigidbody>().velocity = objectToMove.GetComponent<Rigidbody>().velocity.normalized * 5;
                        }
                    }
                }


                // If player stand in z direction
                if (lerpPoint.name == "z")
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        //objectToMove.transform.Translate(Vector3.back * Time.deltaTime * bla);
                        objectToMove.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * 1000);
                        velocityDebug = objectToMove.GetComponent<Rigidbody>().velocity.magnitude;
                        _isPushing = true;

                        if (objectToMove.GetComponent<Rigidbody>().velocity.magnitude > 5)
                        {
                            objectToMove.GetComponent<Rigidbody>().velocity = objectToMove.GetComponent<Rigidbody>().velocity.normalized * 5;
                        }
                    }
                }


                // If player stand in -z direction
                if (lerpPoint.name == "-z")
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        //objectToMove.transform.Translate(Vector3.forward * Time.deltaTime * bla);
                        objectToMove.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1000);
                        velocityDebug = objectToMove.GetComponent<Rigidbody>().velocity.magnitude;
                        _isPushing = true;

                        if (objectToMove.GetComponent<Rigidbody>().velocity.magnitude > 5)
                        {
                            objectToMove.GetComponent<Rigidbody>().velocity = objectToMove.GetComponent<Rigidbody>().velocity.normalized * 5;
                        }
                    }
                }

            }
            else
            {

            }
            
        }
        else if (Input.GetKeyDown(KeyCode.E) && triggerIdent == 1)
        {
            _isCarrying = true;

        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            lerp = 0;
            _isPushing = false;
            gameObject.transform.parent = null;
            gameObject.GetComponent<PlayerController>().enabled = true;
        }

       
        if (_isCarrying)
        {
            carryDelay += Time.deltaTime;

            objectToThrow.transform.Find("mesh").GetComponent<Collider>().enabled = false;
            objectToThrow.transform.position = (playerPosition + (Vector3.up * 5));

            if (Input.GetKeyDown(KeyCode.E) && triggerIdent == 1 && carryDelay >= 0.3f)
            {
                _isCarrying = false;
                carryDelay = 0;
                objectToThrow.transform.Find("mesh").GetComponent<Collider>().enabled = true;
                objectToThrow.GetComponent<Rigidbody>().AddForce(faceDirection * 20000);
            }
        }

    }



    void OnTriggerStay(Collider target)
    {
        if (target.tag == "ObjectToThrow")
        {
            triggerIdent = 1;
            objectToThrow = target.transform.root.gameObject;
        }
        else if(target.tag == "CubeLerpPoint" && triggerIdent != 1)
        {
            endPos = target.transform.position;
            triggerIdent = 2;

            if(_isPushing == false)
            {
                objectToMove = target.transform.root.gameObject;
                lerpPoint = target.gameObject;
            }

            
        }
        
    }

    void OnTriggerExit(Collider target)
    {
        if (target.tag == "ObjectToThrow")
        {
            triggerIdent = 0;
        }
        if (target.tag == "CubeLerpPoint" && triggerIdent == 2)
        {
            if(_isPushing == false)
            {
                objectToMove = null;
                lerpPoint = null;
            }

            triggerIdent = 0;
        }
    }

}
