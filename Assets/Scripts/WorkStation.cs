using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStation : Interactable
{
    private float timeSpent;
    public float totalTime;
    public float speed;
    public Transform itemPivot;
    public int workStationType;
    public Sprite[] timerPhases;
    public float[] timeStamps;
    private int currentSpriteIndex;
    public SpriteRenderer timerSR;
    public GameObject timer;

    private Toy t;

    public void Start()
    {
        if (GlobalControl.playerWorkSpeedPowerCollected)
        {
            totalTime = totalTime / 2;
        }
    }

    public override void StartInteractiveProcess()
    {
        if(t != null)
        {
            FinishedItem();
        }
        else
        {
            if(playerController.item != null && playerController.item.GetComponent<Toy>() != null)
            {
                t = playerController.item.GetComponent<Toy>();
                if (t.itemID == workStationType)
                {
                    t.SetPivot(transform.GetChild(2));
                    t.StartInteractiveProcess();
                    ToggleInteractivity();
                    StartCoroutine("UpgradeItem");
                }
                else
                    t = null;
            }
            
        }
    }

    private IEnumerator UpgradeItem()
    {
        timerSR.sprite = timerPhases[0];
        timer.SetActive(true);
        needEmptyHands = true;
        while (timeSpent < totalTime)
        {
            yield return new WaitForSeconds(speed);
            timeSpent += speed;
            UpdateTimer();
        }

        timeSpent = 0;
        t.UpgradeItemStage();
        ToggleInteractivity();
        StartCoroutine("DeactivateTimer");
    }

    private IEnumerator DeactivateTimer()
    {
        yield return new WaitForSeconds(1.5f);
        timer.SetActive(false);
    }

    private void FinishedItem()
    {
        if(playerController.item == null)
        {
            t.SetPivot(playerHands);
            t.StartInteractiveProcess();
            t = null;
            needEmptyHands = false;
        }
    }

    private void UpdateTimer()
    {
        float percentComplete = timeSpent / totalTime * 100;
 
        int phase = 0;

        for(int i = timeStamps.Length - 1; i >= 0; i--)
        {
            if (percentComplete >= timeStamps[i])
            {
                phase = i;
                break;
            }
        }

        if(phase != currentSpriteIndex)
        {
            currentSpriteIndex = phase;
            timerSR.sprite = timerPhases[phase];
        }
    }
}
