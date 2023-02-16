using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpItem : MonoBehaviour
{
    //private Transform playerPickUpTransform;
    public LayerMask pickUpMask;

    public Vector3 direction { get; set; }
    private GameObject itemHolding;

    private GameObject player;
    public GameObject item;

    public bool isHoldingitem = false;

    //private void Start()
    //{
    //    playerPickUpTransform = player.transform.GetChild(0);
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //if (itemHolding)
            //{
            //    itemHolding.transform.position = transform.position * 1f;
            //    itemHolding.transform.parent = null;

            //    //if (itemHolding.GetComponent<Rigidbody>())
            //    //{
            //    //    itemHolding.GetComponent<Rigidbody>().isKinematic = false;
            //    //}
            //    itemHolding = null;
            //}
            //else
            //{
            //    Collider[] pickUpItem = Physics.OverlapSphere(transform.position + direction, 0.1f, pickUpMask);

            //    if (pickUpItem[0])
            //    {
            //        itemHolding = pickUpItem[0].gameObject;
            //        itemHolding.transform.position = playerPickUpTransform.position;
            //        itemHolding.transform.parent = player.transform;

            //        //if (itemHolding.GetComponent<Rigidbody>())
            //        //{
            //        //    itemHolding.GetComponent<Rigidbody>().isKinematic = true;
            //        //}
            //    }
            //}
        }
        if (isHoldingitem == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //itemHolding.tag = "OffBelt";

                itemHolding.transform.position = player.transform.position * 1f;
                itemHolding.transform.parent = null;
                //item.GetComponent<BoxCollider>().enabled = true;
                item.GetComponent<Rigidbody>().isKinematic = false;

                isHoldingitem = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isHoldingitem == false)
        {
            Debug.Log("Player Enter");

            player = other.gameObject;

            if(Input.GetKey(KeyCode.E))
            {
                itemHolding = item;
                itemHolding.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 1.1f, other.transform.position.z);
                itemHolding.transform.parent = other.transform;
                //item.GetComponent<BoxCollider>().enabled = false;
                item.GetComponent<Rigidbody>().isKinematic = true;

                isHoldingitem = true;

                itemHolding.tag = "CurrentHeldItem";
            }
        }
    }
}
