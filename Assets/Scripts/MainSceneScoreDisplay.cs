using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainSceneScoreDisplay : MonoBehaviour
{

    public TMP_Text playerCurrencyTxt;

    // Update is called once per frame
    void Update()
    {
        playerCurrencyTxt.text = "PayCheck: $" + GlobalControl.playerMoney;
    }
}
