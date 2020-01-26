using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_PanelInstructions : MonoBehaviour


{

public GameObject panelInstructions;
    private GameManager_Master gameManager_Master;

    void OnEnable()
    {
        SetInitialReferences();
        gameManager_Master.GameOverEvent += TurnOffPanelInstructions;
    }



    void OnDisAble()
    {
        gameManager_Master.GameOverEvent -= TurnOffPanelInstructions;
    }

    void SetInitialReferences()
    {
        gameManager_Master = GetComponent<GameManager_Master>();
    }

    void TurnOffPanelInstructions()
    {
        if (panelInstructions != null)
        {
            panelInstructions.SetActive(false);

        }

    }

}

