using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyorSpawner : MonoBehaviour
{
    public GameObject[] conveyorItems;

    public bool canSpawn = true;
    public Transform spawner;

    public int randItemIndex;
    public float spawnerRate;

    // Update is called once per frame
    void Update()
    {
        if(canSpawn == true)
        {
            StartCoroutine(SpawnConveyorItem());
        }
    }

    IEnumerator SpawnConveyorItem()
    {
        canSpawn = false;

        yield return new WaitForSeconds(spawnerRate);
        randItemIndex = Random.Range(0, conveyorItems.Length);

        Instantiate(conveyorItems[randItemIndex], spawner.position, Quaternion.identity);

        canSpawn = true;
    }
}
