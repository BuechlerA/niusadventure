using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Master : MonoBehaviour
{

    public delegate void GameManagerEventHandler();
    public event GameManagerEventHandler MenuToggleEvent;
    public event GameManagerEventHandler InstructionsToggleEvent;
    public event GameManagerEventHandler RestartLevelEvent;
    public event GameManagerEventHandler GoToMenuSceneEvent;
    public event GameManagerEventHandler GameOverEvent;
    public event GameManagerEventHandler ExitGame;


    public bool isGameOver;
    public bool isInstructionsOn;
    public bool isMenuOn;
    public bool isExitOn;

    public void CallEventMenuToggle()
    {
        if (MenuToggleEvent !=null)
        {
            MenuToggleEvent();
        }
    }

    public void CallInstructionsToggleEvent()
    {
        if (InstructionsToggleEvent != null)
        {
            InstructionsToggleEvent();
        }
    }
    public void CallEventRestartLevel()
    {
        if (RestartLevelEvent != null)
        {
            RestartLevelEvent();
        }
    }
    public void CallEventGameOver()
    {
        if (GameOverEvent != null)
        {
            isGameOver = true;
            GameOverEvent();
        }
    }
    public void CallEventGoToMenuScene()
    {
        if (GoToMenuSceneEvent != null)
        {
            GoToMenuSceneEvent();
        }
    }
    public void CallEventExitGame()
    {
        if (ExitGame != null)
        {
            ExitGame();
        }
    }
}
