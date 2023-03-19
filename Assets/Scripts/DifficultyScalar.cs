using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScalar : MonoBehaviour
{
    [Range(0, 100)]
    public float frqncyIncrement, timerIncrement, timerCap, frqncyCap;
    public int money2Timer, money2Frqncy;
    public int timerMod, frqncyMod;

    // Start is called before the first frame update
    void Start()
    {
        UpdateModifiers();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateModifiers()
    {
        if(timerMod < timerCap)
        {
            timerMod = (int)((GlobalControl.playerMoney / money2Timer + GlobalControl.PowerUpTotal)
                * timerIncrement);
        }


        if (frqncyMod < frqncyCap)
        {
            frqncyMod = (int)((GlobalControl.playerMoney / money2Frqncy + GlobalControl.PowerUpTotal)
                * frqncyIncrement);
        }


    }

    public float AdjustTimer(float baseVal)
    {
        return baseVal - timerIncrement * timerMod / 100.0f * baseVal;
    }

    public float AdjustFrequency(float baseVal)
    {
        return frqncyIncrement * frqncyMod / 100.0f * baseVal + baseVal;
    }
}
