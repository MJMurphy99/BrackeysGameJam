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

    private Toy t;

    public override void StartInteractiveProcess()
    {
        if(t != null)
        {
            FinishedItem();
        }
        else
        {
            t = playerController.item.GetComponent<Toy>();
            if (t.itemID == workStationType)
            {
                t.SetPivot(transform);
                t.StartInteractiveProcess();
                ToggleInteractivity();
                StartCoroutine("UpgradeItem");
            }
            else
                t = null;
        }
    }

    private IEnumerator UpgradeItem()
    {
        while (timeSpent < totalTime)
        {
            yield return new WaitForSeconds(speed);
            timeSpent += speed;
        }

        timeSpent = 0;
        t.UpgradeItemStage();
        ToggleInteractivity();
    }

    private void FinishedItem()
    {
        t.SetPivot(playerHands);
        t.StartInteractiveProcess();
    }
}
