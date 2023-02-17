using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyorBelt : MonoBehaviour
{
    public float speed;

    public Vector3 direction;

    public static List<GameObject> onBelt = new List<GameObject>();

    public GameObject[] despawnableItems;
    public GameObject[] timerDespawnableItems;


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= onBelt.Count - 1; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().velocity = speed * direction * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            onBelt.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            onBelt.Remove(collision.gameObject);
        }
    }
}
