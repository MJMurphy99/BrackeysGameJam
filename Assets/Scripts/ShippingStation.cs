using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippingStation : Interactable
{
    public ParticleSystem psStandard, psSpecial;
    public DifficultyScalar ds;
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
                    if (t.name.CompareTo("Special") == 0)
                    {
                        psSpecial.Play();
                        GlobalControl.playerMoney += 15;
                    }

                    psStandard.Play();
                    GlobalControl.playerMoney = GlobalControl.playerMoney + 15;
                    t.DestroyGameObject();
                    ds.UpdateModifiers();
                }
                else
                    t = null;
            }

    }
}
