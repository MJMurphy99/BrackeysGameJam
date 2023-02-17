using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destructable"))
        {
            for (int i = 0; i < conveyorBelt.onBelt.Count; i++)
            {
                if (conveyorBelt.onBelt[i] == other.gameObject)
                {
                    conveyorBelt.onBelt.RemoveAt(i);
                    break;
                }
            }
            Destroy(other.gameObject);
        }  
    }
}
