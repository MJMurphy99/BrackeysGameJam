using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyPowerUp : MonoBehaviour
{
    //these needs to be ints
    public int costOfPowerUp;
    public int increaseCostOfPowerUpsMultiplier;

    public Button speedBtn, jumpBtn, throwBtn;

    public TMP_Text playerCurrencyDisplay, speedCostTxt, jumpCostTxt, throwCostTxt;

    public void Update()
    {
        speedCostTxt.text = "Cost: " + costOfPowerUp;
        jumpCostTxt.text = "Cost: " + costOfPowerUp;
        throwCostTxt.text = "Cost: " + costOfPowerUp;

        playerCurrencyDisplay.text = "Avaliable Funds: " + GlobalControl.playerMoney;
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
        }
    }
}
