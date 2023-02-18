using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : Interactable
{
    public int itemID;
    public Sprite[] itemStages;
    public BoxCollider c;

    private Transform pivot;

    public void UpgradeItemStage()
    {
        itemID++;
        sr.sprite = itemStages[itemID];
    }

    public override void StartInteractiveProcess()
    {
        PickUp();
        if(interactable)
            ToggleInteractivity();
    }

    private void PickUp()
    {
        if (transform.parent != null)
        {
            if (transform.parent.CompareTag("Player"))
                playerController.item = null;
            transform.parent = null;
        }
            
        transform.position =
            new Vector3(pivot.position.x, pivot.position.y, pivot.position.z);
        transform.parent = pivot.transform;
        GetComponent<Rigidbody>().isKinematic = true;
        c.isTrigger = true;
    }

    public void PutDown()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        ToggleInteractivity();
        c.isTrigger = false;
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            pivot = player.transform.GetChild(0);
            playerController.item = gameObject;
        }
    }

    public void SetPivot(Transform t)
    {
        pivot = t;
    }
}
