using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_GoToMenuScene : MonoBehaviour

{
    private GameManager_Master gameManagerMaster;

    void OnEnable ()
    {
        SetInitialReferences();
        gameManagerMaster.GoToMenuSceneEvent += GoToMenuScene;
    }

    void OnDisAble()
    {
        gameManagerMaster.GoToMenuSceneEvent -= GoToMenuScene;

    }


    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
    }

    void GoToMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
