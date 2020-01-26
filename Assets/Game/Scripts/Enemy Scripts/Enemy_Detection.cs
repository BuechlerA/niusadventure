using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Detection : MonoBehaviour
{
    private Enemy_Master enemyMaster;
    private Transform myTransform;
    public Transform head;

    public LayerMask sightLayer;
    public LayerMask playerLayer;

    private float checkRate;
    private float detectRadius = 80;
    private float nextCheck;

    private RaycastHit hit;


    void OnEnable()
    {
        SetInitialReference();
        enemyMaster.EventEnemyDie += DisableThis;
    }
    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableThis;

    }
    void Update()
    {
        CarryOutDetection();
    }

    void SetInitialReference()
    {
        enemyMaster = GetComponent<Enemy_Master>();
        myTransform = transform;

        if (head == null)
        {
            head = myTransform;
        }
        checkRate = Random.Range(0.8f, 1.2f);
    }

    void CarryOutDetection()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;

            Collider[] colliders = Physics.OverlapSphere(myTransform.position, detectRadius, playerLayer);

            if (colliders.Length > 0)
            {
                foreach (Collider potentialTargetCollider in colliders)
                {
                    if (potentialTargetCollider.CompareTag(GameManager_References._playerTag))
                    {
                        if (CanPotentialTargetbeSeen(potentialTargetCollider.transform))
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                enemyMaster.CallEventEnemyLostTarget();
            }
        }
    }

    bool CanPotentialTargetbeSeen(Transform potentialTarget)
    {
        if (Physics.Linecast(head.position, potentialTarget.position, out hit, sightLayer))
        {
            if (hit.transform == potentialTarget)
            {
                enemyMaster.CallEventEnemySetNavTarget(potentialTarget);
                return true;
            }
            else
            {
                enemyMaster.CallEventEnemyLostTarget();
                return false;
            }
        }
        else
        {
            enemyMaster.CallEventEnemyLostTarget();
            return false;
        }
    }

    void DisableThis()
    {
        this.enabled = false;
    }
}
