using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyerDespawner : MonoBehaviour
{
    public static bool readyToDespawn;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.tag = "DespawnItem";
    }
}
