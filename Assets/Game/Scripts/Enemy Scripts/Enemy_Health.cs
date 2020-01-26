using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour {

    public Enemy_Master enemyMaster;
    public int enemyHealth = 100;

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R)) //debug
    //    {
    //        DeductHealth(25);
    //    }
    //}

    void OnEnabled()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDeductHealth += DeductHealth;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDeductHealth -= DeductHealth;
    }

    void SetInitialReferences()
    {
        enemyMaster = GetComponent<Enemy_Master>();
    }

    void DeductHealth(int health)
    {
        enemyHealth -= health;
        if(enemyHealth <= 0)
        {
            enemyHealth = 0;
            enemyMaster.CallEventEnemyDie();
            Destroy(gameObject, Random.Range(10, 20));
        }
    }
}
