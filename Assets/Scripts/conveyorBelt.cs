using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyorBelt : MonoBehaviour
{
    public float speed;

    public Vector3 direction;

    public List<GameObject> onBelt;

    public GameObject[] despawnableItems;
    public GameObject[] timerDespawnableItems;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= onBelt.Count - 1; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().velocity = speed * direction * Time.deltaTime;
        }

        foreach (var item in onBelt)
        {
            if(gameObject.tag == "DespawnItem")
            {
                onBelt.Remove(gameObject);
            }

            DespawnItems();
        }

        timerDespawnableItems = GameObject.FindGameObjectsWithTag("OffBelt");
    }

    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
        collision.gameObject.tag = "OnBelt";
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
        collision.gameObject.tag = "OffBelt";
    }

    public void DespawnItems()
    {
        despawnableItems = GameObject.FindGameObjectsWithTag("DespawnItem");
        for (int i = 0; i < despawnableItems.Length; i++)
        {
            Destroy(despawnableItems[i]);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    onBelt.Add(other.gameObject);
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    onBelt.Remove(other.gameObject);
    //}
}
