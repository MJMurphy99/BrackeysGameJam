using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithinWorkArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            DeathByBoss.inWorkSpace = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            DeathByBoss.inWorkSpace = false;
    }
}
