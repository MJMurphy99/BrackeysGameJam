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

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        Instantiate(conveyorItems[randItemIndex], spawner);

        canSpawn = true;
    }
}
