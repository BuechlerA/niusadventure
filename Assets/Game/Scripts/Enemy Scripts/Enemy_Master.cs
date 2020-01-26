using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Master : MonoBehaviour
{
    public Transform myTarget;
    public bool isOnRoute;
    public bool isNavPaused;

    //delegates sind variablen die viele Funktionen speichern können und ausführen, man called delegates wie functionen 
    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventEnemyDie;
    public event GeneralEventHandler EventEnemyWalking;
    public event GeneralEventHandler EventEnemyReachedNavTarget;
    public event GeneralEventHandler EventEnemyAttack;
    public event GeneralEventHandler EventEnemyLostTarget;

    public delegate void HealthEventHandler(int health);
    public event HealthEventHandler EventEnemyDeductHealth;

    public delegate void NavTargetEventHandler(Transform targetTransform);
    public event NavTargetEventHandler EventEnemySetNavTarget;

    //------------------------------------------------------------------------------------------------------------------------------------------------------------

    public int enemyHealth = 100;

    public void CallEventEnemyDeductHealth(int health)
    {
        if (EventEnemyDeductHealth != null)
        {
            EventEnemyDeductHealth(health);
            Debug.Log(health);

            enemyHealth -= health;
            if (enemyHealth <= 0)
            {
                enemyHealth = 0;
                CallEventEnemyDie();
                Destroy(gameObject, Random.Range(10, 20));
            }
        }
    }
    public void CallEventEnemySetNavTarget(Transform targTransform)
    {
        if (EventEnemySetNavTarget != null)
        {
            EventEnemySetNavTarget(targTransform);
        }
        myTarget = targTransform;
    }
    public void CallEventEnemyDie()
    {
        if (EventEnemyDie !=null)
        {
            EventEnemyDie();
        }
    }
    public void CallEventEnemyWalking()
    {
        if (EventEnemyWalking != null)
        {
            EventEnemyWalking();
        }
    }
    public void CallEventEnemyReachedNavTarget()
    {
        if (EventEnemyReachedNavTarget != null)
        {
            EventEnemyReachedNavTarget();
        }
    }
    public void CallEventEnemyAttack()
    {
        if (EventEnemyAttack != null)
        {
            EventEnemyAttack();
        }
    }

    public void CallEventEnemyLostTarget()
    {
        if (EventEnemyLostTarget != null)
        {
            EventEnemyLostTarget();
        }
        myTarget = null;
    }
}
