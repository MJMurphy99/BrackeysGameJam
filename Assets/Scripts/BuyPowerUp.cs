using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BuyPowerUp : MonoBehaviour
{
    public int increaseCostOfPowerUpsMultiplier;

    public bool panel1Active = true;

    public GameObject panel1, panel2;

    public Button speedBtn, jumpBtn, throwBtn, diaperBtn, hallPassBtn, workStationBtn;

    public TMP_Text playerCurrencyDisplay, speedCostTxt, jumpCostTxt, throwCostTxt, diaperTxt, hallPassTxt, workStationTxt, playerGen, causeOfDeathTxt;

    private void Start()
    {
        StartCoroutine("UpdateGeneration");
        disableButtonsAlreadyPurchased();
    }

    public void Update()
    {
        speedCostTxt.text = "Increased Movement Speed. Cost: " + GlobalControl.costOfPowerUp;
        jumpCostTxt.text = "Increased Jump Height. Cost: " + GlobalControl.costOfPowerUp;
        throwCostTxt.text = "Increased Throw Power. Cost: " + GlobalControl.costOfPowerUp;
        diaperTxt.text = "Increased Bladder Size. Cost: " + GlobalControl.costOfPowerUp;
        hallPassTxt.text = "Gain a Hall Pass. Cost: " + GlobalControl.costOfPowerUp;
        workStationTxt.text = "Speeds up Work Stations. Cost: " + GlobalControl.costOfPowerUp;

        playerCurrencyDisplay.text = "Accrued Generational Wealth: " + GlobalControl.playerMoney;
    }

    public void purchaseSpeed()
    {
        if (GlobalControl.playerSpeedPowerCollected == false)
        {
            if (GlobalControl.costOfPowerUp > GlobalControl.playerMoney)
            {
                ColorBlock color = speedBtn.colors;
                color.pressedColor = Color.red;
                speedBtn.colors = color;
            }
            else
            {
                GlobalControl.playerSpeedPowerCollected = true;
                GlobalControl.PowerUpTotal++;
                GlobalControl.playerMoney -= GlobalControl.costOfPowerUp;
                GlobalControl.costOfPowerUp = GlobalControl.costOfPowerUp * increaseCostOfPowerUpsMultiplier;
                speedBtn.interactable = false;

                FMODUnity.RuntimeManager.PlayOneShot("event:/UI_boughtpowerup");
            }
        } else
        {
            speedBtn.interactable = false;
        }
    }

    public void purchaseJump()
    {
        if (GlobalControl.playerJumpPowerCollected == false)
        {
            if (GlobalControl.costOfPowerUp > GlobalControl.playerMoney)
            {
                ColorBlock color = jumpBtn.colors;
                color.pressedColor = Color.red;
                jumpBtn.colors = color;
            }
            else
            {
                GlobalControl.playerJumpPowerCollected = true;
                GlobalControl.PowerUpTotal++;
                GlobalControl.playerMoney -= GlobalControl.costOfPowerUp;
                GlobalControl.costOfPowerUp = GlobalControl.costOfPowerUp * increaseCostOfPowerUpsMultiplier;
                jumpBtn.interactable = false;
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI_boughtpowerup");
            }
        } else
        {
            jumpBtn.interactable = false;
        }
    }

    public void purchaseThrow()
    {
        if (GlobalControl.playerThrowPowerCollected == false)
        {
            if (GlobalControl.costOfPowerUp > GlobalControl.playerMoney)
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
                GlobalControl.playerMoney -= GlobalControl.costOfPowerUp;
                GlobalControl.costOfPowerUp = GlobalControl.costOfPowerUp * increaseCostOfPowerUpsMultiplier; 
                throwBtn.interactable = false;
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI_boughtpowerup");
            }
        } else
        {
            throwBtn.interactable = false;
        }
    }

    public void purchaseDiaper()
    {
        if (GlobalControl.playerDiaperPowerCollected == false)
        {
            if (GlobalControl.costOfPowerUp > GlobalControl.playerMoney)
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
                GlobalControl.playerMoney -= GlobalControl.costOfPowerUp;
                GlobalControl.costOfPowerUp = GlobalControl.costOfPowerUp * increaseCostOfPowerUpsMultiplier;
                diaperBtn.interactable = false;
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI_boughtpowerup");
            }
        } else
        {
            diaperBtn.interactable = false;
        }
    }

    public void purchaseHallPass()
    {
        if (GlobalControl.playerHallPassPowerCollected == false)
        {
            if (GlobalControl.costOfPowerUp > GlobalControl.playerMoney)
            {
                ColorBlock color = hallPassBtn.colors;
                color.pressedColor = Color.red;
                hallPassBtn.colors = color;
            }
            else
            {
                GlobalControl.playerHallPassPowerCollected = true;
                GlobalControl.PowerUpTotal++;
                GlobalControl.playerMoney -= GlobalControl.costOfPowerUp;
                GlobalControl.costOfPowerUp = GlobalControl.costOfPowerUp * increaseCostOfPowerUpsMultiplier;
                hallPassBtn.interactable = false;
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI_boughtpowerup");
            }
        } else
        {
            hallPassBtn.interactable = false;
        }
    }

    public void purchaseWorkSpeed()
    {
        if (GlobalControl.playerWorkSpeedPowerCollected == false)
        {
            if (GlobalControl.costOfPowerUp > GlobalControl.playerMoney)
            {
                ColorBlock color = workStationBtn.colors;
                color.pressedColor = Color.red;
                workStationBtn.colors = color;
            }
            else
            {
                GlobalControl.playerWorkSpeedPowerCollected = true;
                GlobalControl.PowerUpTotal++;
                GlobalControl.playerMoney -= GlobalControl.costOfPowerUp;
                GlobalControl.costOfPowerUp = GlobalControl.costOfPowerUp * increaseCostOfPowerUpsMultiplier;
                workStationBtn.interactable = false;
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI_boughtpowerup");
            }
        } else
        {
            workStationBtn.interactable = false;
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

    public void disableButtonsAlreadyPurchased()
    {
        if(GlobalControl.playerWorkSpeedPowerCollected)
        {
            workStationBtn.interactable = false;
        }

        if (GlobalControl.playerHallPassPowerCollected)
        {
            hallPassBtn.interactable = false;
        }

        if (GlobalControl.playerDiaperPowerCollected)
        {
            diaperBtn.interactable = false;
        }

        if (GlobalControl.playerJumpPowerCollected)
        {
            jumpBtn.interactable = false;
        }

        if (GlobalControl.playerThrowPowerCollected)
        {
            throwBtn.interactable = false;
        }

        if (GlobalControl.playerSpeedPowerCollected)
        {
            speedBtn.interactable = false;
        }
    }
}
