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

    private GameObject item;

    public override void StartInteractiveProcess()
    {
        item = pickUpItem.item;//When new types of items (toys and packages) are added, this will need to be updated to check for that
        pickUpItem.RemoveItemFromHands();
        item.transform.parent = itemPivot;
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.transform.position = itemPivot.transform.position;
        StartCoroutine("UpgradeItem");
    }

    public void CancelInteractiveProcess()
    {
        StopCoroutine("UpgradeItem");
        timeSpent = 0;
    }

    private IEnumerator UpgradeItem()
    {
        while (timeSpent < totalTime)
        {
            yield return new WaitForSeconds(speed);
            timeSpent += speed;
        }

        timeSpent = 0;
        //Either replace parts item with toy item or activate toy state in a generic item base
    }
}
