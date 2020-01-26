using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ComboAttackScript : MonoBehaviour
{

    bool ActivateTimerToReset = false;
    public bool isAttacking = false;
    public float comboTimer = 0.5f;
    public int currentComboState = 0;
    public float originalTimer;
    public Collider hitCollider;
    Animator animator;
    public PlayerController playerController;



    void Start()
    {
        originalTimer = currentComboState;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        NewComboSystem();
        ResetComboState(ActivateTimerToReset);


    }

    void ResetComboState(bool franzJustusMaximilianFriedrichFrederickLisdad)
    {
        if (franzJustusMaximilianFriedrichFrederickLisdad)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                currentComboState = 0;
                ActivateTimerToReset = false;
                comboTimer = 0.5f;
                hitCollider.enabled = false;
                animator.SetInteger("attackState",0);
                isAttacking = false;
                
            }
        }
    }


    void NewComboSystem()
    {
        if (playerController.canJump == true && Input.GetKeyDown(KeyCode.B)  || Input.GetButtonDown("Attack"))
        {
           
            currentComboState++;
            ActivateTimerToReset = true;   
            if (currentComboState == 1)
            {
                isAttacking = true;
                Debug.Log("hit1");
                hitCollider.enabled = true;
                animator.SetInteger("attackState", 1);
            }
            if (currentComboState == 2)
            {
                isAttacking = true;
                Debug.Log("hit2");
                hitCollider.enabled = true;
                animator.SetInteger("attackState", 2);

            }
            if (currentComboState >= 3)
            {
                isAttacking = true;
                Debug.Log("hit3");
                hitCollider.enabled = true;
                animator.SetInteger("attackState", 3);
            }          
        }
    }
}
