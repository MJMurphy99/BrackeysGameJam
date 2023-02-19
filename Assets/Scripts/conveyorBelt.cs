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

    public void Awake()
    {
        despawnableItems = null;
        timerDespawnableItems = null;
    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= onBelt.Count - 1; i++)
        {
            GameObject g = onBelt[i];
            if (g != null)
            {
                if (g.CompareTag("Player"))
                    g.GetComponent<Rigidbody>().velocity += direction * speed;
                else
                    g.GetComponent<Rigidbody>().position += direction * speed * Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}
