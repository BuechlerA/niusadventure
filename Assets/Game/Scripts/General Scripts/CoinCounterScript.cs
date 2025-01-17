﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounterScript : MonoBehaviour
{

    private float fillAmount;
    [SerializeField]
    private Image content;
    [SerializeField]
    private Text valueText;
    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            string[] tmp = valueText.text.Split(' ');
            valueText.text = tmp[0] + " " +value;
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }


    //---------------------------------------------------------------------------------
	void Start ()
    {
		
	}

    //---------------------------------------------------------------------------------
    void Update ()
    {
        HandleBar();
	}

    //---------------------------------------------------------------------------------
    private void HandleBar()
    {
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = fillAmount;

        }
    }
    private float Map (float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        //Current Health 2 - Valuewhendead 0) * (Inspector 1 -0) / (CharacterMaxhealth 4 - Valuewhendead 0)   +0

    }

}
