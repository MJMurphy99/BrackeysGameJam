using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippingStation : Interactable
{
    private float timeSpent;
    public float totalTime;
    public float speed;
    public Transform itemPivot;
    public int workStationType;

    private Toy t;

    public override void StartInteractiveProcess()
    {
            if (playerController.item.GetComponent<Toy>() != null)
            {
                t = playerController.item.GetComponent<Toy>();
                if (t.itemID == workStationType)
                {
                    GlobalControl.playerMoney = GlobalControl.playerMoney + 15;
                    t.DestroyGameObject();
                }
                else
                    t = null;
            }

    }
}
