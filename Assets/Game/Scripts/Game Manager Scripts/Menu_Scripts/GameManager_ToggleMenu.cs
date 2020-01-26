using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ToggleMenu : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;
    public GameObject menu;

    void Start()
    {
        ToggleMenu();
    }

    void Update()
    {
        CheckForMenuToggleRequest();
    }

    void OnEnable()
    {
        SetInitialReferences();
        gameManagerMaster.GameOverEvent += ToggleMenu;
    }

    void OnDisAble()
    {
        gameManagerMaster.GameOverEvent -= ToggleMenu;
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
    }

    void CheckForMenuToggleRequest()
    {
        if(Input.GetButtonDown("Cancel") && !gameManagerMaster.isGameOver && !gameManagerMaster.isInstructionsOn)
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        if (menu!= null)
        {
            menu.SetActive(!menu.activeSelf);
            gameManagerMaster.isMenuOn = !gameManagerMaster.isMenuOn;
            gameManagerMaster.CallEventMenuToggle();
        }
    }

}
