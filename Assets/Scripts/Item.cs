using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public BoxCollider c;
    public Transform pivot;

    // Start is called before the first frame update
    public override void StartInteractiveProcess()
    {
        PickUp();
        if (interactable)
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

        if (pivot.parent != null && pivot.parent.CompareTag("Player"))
            playerController.item = gameObject;

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
        }
    }

    public void SetPivot(Transform t)
    {
        pivot = t;
    }
}
