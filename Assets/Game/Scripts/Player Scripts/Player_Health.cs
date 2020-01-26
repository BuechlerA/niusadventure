using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Player_Health : MonoBehaviour
{ 

private GameManager_Master gameManagerMaster;
private Player_Master playerMaster;
public int playerHealth;
public Text healthText;

void OnEnable()
{
    SetInitialReferences();
    SetUI();
    playerMaster.EventPlayerHealthDeduction += DeductHealth;
    playerMaster.EventPlayerHealthIncrease += IncreaseHealth;
}

void OnDisAble()
{

    playerMaster.EventPlayerHealthDeduction -= DeductHealth;
    playerMaster.EventPlayerHealthIncrease -= IncreaseHealth;
}

void SetInitialReferences()
{
    gameManagerMaster = GameObject.Find("GameManager").GetComponent<GameManager_Master>();
    playerMaster = GetComponent<Player_Master>();
}

void Start()
{
  // StartCoroutine(TestHealthDeduction());
}

IEnumerator TestHealthDeduction()
{
    yield return new WaitForSeconds(2);
        //DeductHealth(2);
        playerMaster.CallEventPlayerHealthDeduction(2);

}

void DeductHealth(int healthChange)
{
    playerHealth -= healthChange;

    if (playerHealth <= 0)
    {
        playerHealth = 0;
        gameManagerMaster.CallEventGameOver();
    }
        SetUI();

    }
void IncreaseHealth(int healthChange)
{
    playerHealth += healthChange;

    if (playerHealth > 4)
    {
            playerHealth = 4;
    }
    SetUI();
}

void SetUI()
{
    if (healthText != null)
    {
        healthText.text = playerHealth.ToString();
    }
}


}


