using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembleToy : Interactable
{
    private float timeSpent;
    public float totalTime;
    public float speed;
    public Transform partsPivot;

    private GameObject parts;

    public override void StartInteractiveProcess()
    {
        parts = pickUpItem.item;//When new types of items (toys and packages) are added, this will need to be updated to check for that
        pickUpItem.RemoveItemFromHands();
        parts.transform.parent = partsPivot;
        parts.GetComponent<Rigidbody>().isKinematic = true;
        parts.transform.position = partsPivot.transform.position;
        StartCoroutine("ToyAssembly");
    }

    public void CancelInteractiveProcess()
    {
        StopCoroutine("ToyAssembly");
        timeSpent = 0;
    }

    private IEnumerator ToyAssembly()
    {
        while(timeSpent < totalTime)
        {
            yield return new WaitForSeconds(speed);
            timeSpent += speed;
        }

        timeSpent = 0;
        //Either replace parts item with toy item or activate toy state in a generic item base
    }
}
