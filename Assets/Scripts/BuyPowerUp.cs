using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BuyPowerUp : MonoBehaviour
{
    //these needs to be ints
    public int costOfPowerUp;
    public int increaseCostOfPowerUpsMultiplier;

    public Button speedBtn, jumpBtn, throwBtn;

    public TMP_Text playerCurrencyDisplay, speedCostTxt, jumpCostTxt, throwCostTxt;

    public void Update()
    {
        speedCostTxt.text = "Increased Movement Speed. Cost: " + costOfPowerUp;
        jumpCostTxt.text = "Increased Jump Height. Cost: " + costOfPowerUp;
        throwCostTxt.text = "Increased Throw Power. Cost: " + costOfPowerUp;

        playerCurrencyDisplay.text = "Accrued Generational Wealth: " + GlobalControl.playerMoney;
    }

    public void purchaseSpeed()
    {
        if (costOfPowerUp > GlobalControl.playerMoney)
        {
            ColorBlock color = speedBtn.colors;
            color.pressedColor = Color.red;
            speedBtn.colors = color;
        } else
        {
            GlobalControl.playerSpeedPowerCollected = true;
            costOfPowerUp = costOfPowerUp * increaseCostOfPowerUpsMultiplier;
            GlobalControl.playerMoney -= costOfPowerUp;
            speedBtn.interactable = false;

            FMODUnity.RuntimeManager.PlayOneShot("event:/UI_boughtpowerup");
        }
    }

    public void purchaseJump()
    {
        if (costOfPowerUp > GlobalControl.playerMoney)
        {
            ColorBlock color = jumpBtn.colors;
            color.pressedColor = Color.red;
            jumpBtn.colors = color;
        }
        else
        {
            GlobalControl.playerJumpPowerCollected = true;
            costOfPowerUp = costOfPowerUp * increaseCostOfPowerUpsMultiplier;
            GlobalControl.playerMoney -= costOfPowerUp;
            jumpBtn.interactable = false;
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI_boughtpowerup");
        }
    }

    public void purchaseThrow()
    {
        if (costOfPowerUp > GlobalControl.playerMoney)
        {
            ColorBlock color = throwBtn.colors;
            color.pressedColor = Color.red;
            throwBtn.colors = color;
        }
        else
        {
            GlobalControl.playerThrowPowerCollected = true;
            costOfPowerUp = costOfPowerUp * increaseCostOfPowerUpsMultiplier;
            GlobalControl.playerMoney -= costOfPowerUp;
            throwBtn.interactable = false;
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI_boughtpowerup");
        }
    }
}
