using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    //saving inventory data between scenes from this tutorial https://www.sitepoint.com/saving-data-between-scenes-in-unity/

    public static int deathCounter = 0;
    public static int costOfPowerUp;
    public static GlobalControl Instance;
    public static bool playerSpeedPowerCollected, playerJumpPowerCollected, playerThrowPowerCollected, playerDiaperPowerCollected, playerHallPassPowerCollected, playerWorkSpeedPowerCollected;
    public static int playerMoney =25;
    private static int powerUpTotal = 0;
    public static int PowerUpTotal
    {
        get { return powerUpTotal; }
        set { powerUpTotal = value; }
    }

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

}

