using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : Item
{
    public int itemID;
    public Sprite[] itemStages;

    public void UpgradeItemStage()
    {
        itemID++;
        sr.sprite = itemStages[itemID];
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
