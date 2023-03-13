using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyorSpawner : MonoBehaviour
{
    public GameObject[] conveyorItems;

    public bool canSpawn = true;
    public Transform spawner;

    public int rand;
    public float spawnerRate;
    public int appearanceProb, appearanceMod;

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
        rand = Random.Range(0, appearanceProb);

        int itemIndex = rand <= appearanceMod ? 1 : 0;

        Instantiate(conveyorItems[itemIndex], spawner.position, Quaternion.identity);

        canSpawn = true;
    }
}
