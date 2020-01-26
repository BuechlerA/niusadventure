using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stats 
{
    [SerializeField]
    private CoinCounterScript bar;
    [SerializeField]
    private float maxVal;
    [SerializeField]
    private float currentVal;

    public float CurrentVal
    {
        get
        {
            return currentVal;
        }

        set
        {
           
            currentVal = value;
            Bar.Value = currentVal;
        }
    }

    public float MaxVal
        // This is used so you can increase the maximum health and give the parameter to the bar and display it properly
    {
        get
        {
            return maxVal;
        }

        set
        {
            
            this.maxVal = value;
            Bar.MaxValue = maxVal;
        }
    }

    public CoinCounterScript Bar
    {
        get
        {
            return bar;
        }

        set
        {
            bar = value;
        }
    }
    public void initialize ()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
        
}
