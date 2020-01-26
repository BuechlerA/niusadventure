using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ : MonoBehaviour {

    private Enemy_Master enemyMaster;
    private Collider myCollider;
    private Rigidbody myRigidbody;

    void OnEnabled()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDie -= ActivateRagdoll;
    }

    void OnDisable()
    {

    }

    void SetInitialReferences()
    {
        enemyMaster.transform.root.GetComponent<Enemy_Master>();

        if(GetComponent<Collider>() != null)
        {
            myCollider = GetComponent<Collider>();
        }

        if (GetComponent<Rigidbody>() != null)
        {
            myRigidbody = GetComponent<Rigidbody>();
        }
    }

    void ActivateRagdoll()
    {
        if(myRigidbody != null)
        {
            myRigidbody.isKinematic = false;
            myRigidbody.useGravity = true;
        }

        if(myCollider != null)
        {
            myCollider.isTrigger = false;
            myCollider.enabled = true;
        }
    }
}
