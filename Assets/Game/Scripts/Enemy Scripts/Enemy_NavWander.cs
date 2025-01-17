﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_NavWander : MonoBehaviour
{
    private Enemy_Master enemyMaster;
    private UnityEngine.AI.NavMeshAgent myNavMeshAgent;

    private Transform mytransform;
    private Vector3 wanderTarget;
    private UnityEngine.AI.NavMeshHit navHit;

    private float checkRate;
    private float nextCheck;
    private float wanderRange = 10;


    void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDie += DisableThis;
    }

    void OnDisAble()
    {
        enemyMaster.EventEnemyDie -= DisableThis;
    }

    void Update()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            CheckIfIShouldWander();
        }

    }

    void SetInitialReferences()
    {
        enemyMaster = GetComponent<Enemy_Master>();
        if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
        {
            myNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }
        checkRate = Random.Range(0.3f, 0.4f);
        mytransform = transform;
    }

    void CheckIfIShouldWander()
    {
        if(enemyMaster.myTarget == null && !enemyMaster.isOnRoute && !enemyMaster.isNavPaused )
        {
            if (RandomWanderTarget(mytransform.position, wanderRange, out wanderTarget))
            {
                myNavMeshAgent.SetDestination(wanderTarget);
                enemyMaster.isOnRoute = true;
                enemyMaster.CallEventEnemyWalking();
            }
        }
    }

    bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result)
    {
        Vector3 randomPoint = centre + Random.insideUnitSphere * wanderRange;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint,out navHit,1.0f,UnityEngine.AI.NavMesh.AllAreas))
        {
            result = navHit.position;
            return true;
        }
        else
        {
            result = centre;
            return false;
        }
    }

    void DisableThis()
    {
        this.enabled = false;
    }

}
