using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ToggleInventoryUI : MonoBehaviour
{
    [Tooltip ("Does this game mode have an inventory= Set to true if that is the case")]
    public bool hasInventory;
    public GameObject inventoryUI;
    public string toggleInventoryButton;
    private GameManager_Master gameManagerMaster;

    void Start()
    {
        SetInititalReferences();
    }

    void Update()
    {
        CheckForInventoryUIToggleRequest();
    }

    void SetInititalReferences()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
        if(toggleInventoryButton =="")
        {
            Debug.LogWarning("Please tpye in the name of the button sued to toggle the inventory in " +
                "Gamemanager_ToggleInventoryUI");
            this.enabled = false;
        }
    }

    void CheckForInventoryUIToggleRequest()
    {
        if(Input.GetButtonUp(toggleInventoryButton) && !gameManagerMaster.isMenuOn && !gameManagerMaster.isGameOver && hasInventory )
        {
            ToggleInventoryUI();
        }
    }

    void ToggleInventoryUI()
    {
        if(inventoryUI != null)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            gameManagerMaster.isInstructionsOn  = !gameManagerMaster.isInstructionsOn;
            gameManagerMaster.CallInstructionsToggleEvent();
        }
    }
}
