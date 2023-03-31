using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CauseOfDeathExplanation : MonoBehaviour
{
    public TMP_Text causeOfDeathText;

    public GameObject panel;

    private void Update()
    {
        if (panel.activeSelf == true)
        {
            causeOfDeathText.text = "";
        } else
        {
            SetCauseOfDeath(GlobalControl.causeOfDeath);
        }
    }

    public void SetCauseOfDeath(int i)
    {
        switch (i)
        {
            case 0:
                {
                    causeOfDeathText.text = "Here lies " + GlobalControl.playerName +". \n Cause Of Death: \n Unknown... How spooky...";
                    break;
                }
            case 1:
                {
                    causeOfDeathText.text = "Here lies " + GlobalControl.playerName + ". \n Cause Of Death: \n Failure to SAFELY dispose of defective toy.";
                    break;
                }
            case 2:
                {
                    causeOfDeathText.text = "Here lies " + GlobalControl.playerName + ". \n Cause Of Death: \n Allowed defective toy into incinerator, detonating factory.";
                    break;
                }
            case 3:
                {
                    causeOfDeathText.text = "Here lies " + GlobalControl.playerName + ". \n Cause Of Death: \n Overcame safety measures to willingly enter incinerator.";
                    break;
                }
            case 4:
                {
                    causeOfDeathText.text = "Here lies " + GlobalControl.playerName + ". \n Cause Of Death: \n Bladder Explosion.";
                    break;
                }
            case 5:
                {
                    causeOfDeathText.text = "Here lies " + GlobalControl.playerName + ". \n Cause Of Death: \n Natural Causes. (Lost Health Insurance after being fired)";
                    break;
                }
            default:
                {
                    causeOfDeathText.text = "Here lies " + GlobalControl.playerName + ". \n Cause Of Death: \n Unknown... How spooky...";
                    break;
                }
        }
    }
}
