using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class despawnTimer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "OffBelt")
        {
            StartCoroutine(DespawnTimer());
        }
    }

    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
