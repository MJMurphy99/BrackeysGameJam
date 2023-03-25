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

    public bool panel1Active = true;

    public GameObject panel1, panel2;

    public Button speedBtn, jumpBtn, throwBtn, diaperBtn, hallPassBtn, workStationBtn;

    public TMP_Text playerCurrencyDisplay, speedCostTxt, jumpCostTxt, throwCostTxt, diaperTxt, hallPassTxt, workStationTxt, playerGen;

    private void Start()
    {
        StartCoroutine("UpdateGeneration");
    }

    public void Update()
    {
        speedCostTxt.text = "Increased Movement Speed. Cost: " + costOfPowerUp;
        jumpCostTxt.text = "Increased Jump Height. Cost: " + costOfPowerUp;
        throwCostTxt.text = "Increased Throw Power. Cost: " + costOfPowerUp;
        diaperTxt.text = "Increased Bladder Size. Cost: " + costOfPowerUp;
        hallPassTxt.text = "Gain a Hall Pass. Cost: " + costOfPowerUp;
        workStationTxt.text = "Speeds up Work Stations. Cost: " + costOfPowerUp;

        playerCurrencyDisplay.text = "Accrued Generational Wealth: " + GlobalControl.playerMoney;
    }

    public void purchaseSpeed()
    {
        if (GlobalControl.playerSpeedPowerCollected == false)
        {
            if (costOfPowerUp > GlobalControl.playerMoney)
            {
                ColorBlock color = speedBtn.colors;
                color.pressedColor = Color.red;
                speedBtn.colors = color;
            }
            else
            {
                GlobalControl.playerSpeedPowerCollected = true;
                GlobalControl.PowerUpTotal++;
                GlobalControl.playerMoney -= costOfPowerUp;
                costOfPowerUp = costOfPowerUp * increaseCostOfPowerUpsMultiplier;
                speedBtn.interactable = false;

                FMODUnity.RuntimeManager.PlayOneShot("event:/UI_boughtpowerup");
            }
        }
    }

    public void purchaseJump()
    {
        if (GlobalControl.playerJumpPowerCollected == false)
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
                GlobalControl.PowerUpTotal++;
                GlobalControl.playerMoney -= costOfPowerUp;
                costOfPowerUp = costOfPowerUp * increaseCostOfPowerUpsMultiplier;
                jumpBtn.interactable = false;
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI_boughtpowerup");
            }
        }
    }

    public void purchaseThrow()
    {
        if (GlobalControl.playerThrowPowerCollected == false)
        {
            if (costOfPowerUp > GlobalControl.playerMoney)
            {
                ColorBlock color = throwBtn.colors;
                color.pressedColor = Color.red;
                throwBtn.colors = color;
                Debug.Log(throwBtn.colors);
            }
            else
            {
                GlobalControl.playerThrowPowerCollected = true;
                GlobalControl.PowerUpTotal++;
                GlobalControl.playerMoney -= costOfPowerUp;
                costOfPowerUp = costOfPowerUp * increaseCostOfPowerUpsMultiplier; 
                throwBtn.interactable = false;
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI_boughtpowerup");
            }
        }
    }

    public void purchaseDiaper()
    {
        if (GlobalControl.playerDiaperPowerCollected == false)
        {
            if (costOfPowerUp > GlobalControl.playerMoney)
            {
                ColorBlock color = diaperBtn.colors;
                color.pressedColor = Color.red;
                diaperBtn.colors = color;
                Debug.Log(diaperBtn.colors);
            }
            else
            {
                GlobalControl.playerDiaperPowerCollected = true;
                GlobalControl.PowerUpTotal++;
                GlobalControl.playerMoney -= costOfPowerUp;
                costOfPowerUp = costOfPowerUp * increaseCostOfPowerUpsMultiplier;
                diaperBtn.interactable = false;
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI_boughtpowerup");
            }
        }
    }

    public void purchaseHallPass()
    {
        if (GlobalControl.playerHallPassPowerCollected == false)
        {
            if (costOfPowerUp > GlobalControl.playerMoney)
            {
                ColorBlock color = hallPassBtn.colors;
                color.pressedColor = Color.red;
                hallPassBtn.colors = color;
            }
            else
            {
                GlobalControl.playerHallPassPowerCollected = true;
                GlobalControl.PowerUpTotal++;
                GlobalControl.playerMoney -= costOfPowerUp;
                costOfPowerUp = costOfPowerUp * increaseCostOfPowerUpsMultiplier;
                hallPassBtn.interactable = false;
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI_boughtpowerup");
            }
        }
    }

    public void purchaseWorkSpeed()
    {
        if (GlobalControl.playerWorkSpeedPowerCollected == false)
        {
            if (costOfPowerUp > GlobalControl.playerMoney)
            {
                ColorBlock color = workStationBtn.colors;
                color.pressedColor = Color.red;
                workStationBtn.colors = color;
            }
            else
            {
                GlobalControl.playerWorkSpeedPowerCollected = true;
                GlobalControl.PowerUpTotal++;
                GlobalControl.playerMoney -= costOfPowerUp;
                costOfPowerUp = costOfPowerUp * increaseCostOfPowerUpsMultiplier;
                workStationBtn.interactable = false;
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI_boughtpowerup");
            }
        }
    }

    public void changeShopOptions()
    {
        if (panel1Active)
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
            panel1Active = false;
        } else
        {
            panel2.SetActive(false);
            panel1.SetActive(true);
            panel1Active = true;
        }
    }

    private IEnumerator UpdateGeneration()
    {
        string txt = "Current Family Generation: ";
        playerGen.text = txt + GlobalControl.deathCounter;

        yield return new WaitForSeconds(1.0f);

        playerGen.text = txt + (GlobalControl.deathCounter + 1);
    }
}
