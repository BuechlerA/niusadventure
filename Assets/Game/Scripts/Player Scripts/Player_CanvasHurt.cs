using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CanvasHurt : MonoBehaviour
{

    public GameObject hurtCanvas;
    private Player_Master playermaster;
    private float secondsTillHide = 2;

    void OnEnable()
    {
        SetInitialReferences();
        playermaster.EventPlayerHealthDeduction += TurnOnHurtEffect;
    }

    void OnDisable()
    {
        playermaster.EventPlayerHealthDeduction -= TurnOnHurtEffect;

    }

    void SetInitialReferences()
    {
        playermaster = GetComponent<Player_Master>();
    }

    void TurnOnHurtEffect(int dummy)
    {
        if (hurtCanvas != null)
        {
            StopAllCoroutines();
            hurtCanvas.SetActive(true);
            StartCoroutine(ResetHurtCanvas());
        }
    }
    IEnumerator ResetHurtCanvas()
    {
        yield return new WaitForSeconds(secondsTillHide);
        hurtCanvas.SetActive(false);
    }
}
