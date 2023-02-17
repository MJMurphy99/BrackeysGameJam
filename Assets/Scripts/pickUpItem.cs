using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpItem : MonoBehaviour
{
    //private Transform playerPickUpTransform;
    public LayerMask pickUpMask;

    public Vector3 direction { get; set; }
    private GameObject itemHolding;

    public Transform holdPivot;
    public GameObject item;

    public bool isHoldingitem = false;

    private float chargeCounter = 0.0f;
    public float chargeMax;
    public int strength;


    // Update is called once per frame
    void Update()
    {
        if (isHoldingitem == true)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                chargeCounter += Time.deltaTime;
            }
            else if(Input.GetKeyUp(KeyCode.Q))
            {
                if (chargeCounter >= 0.25f)
                    itemHolding.GetComponent<Rigidbody>().velocity = playerController.facing * strength;
                else
                    itemHolding.transform.position = transform.position * 1f;
                    

                itemHolding.transform.parent = null;
                item.GetComponent<Rigidbody>().isKinematic = false;

                isHoldingitem = false;
                chargeCounter = 0.0f;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isHoldingitem == false)
        {
            if (Input.GetKey(KeyCode.E))
            {
                item = other.transform.parent.gameObject;
                itemHolding = item;
                itemHolding.transform.position = 
                    new Vector3(holdPivot.position.x, holdPivot.position.y, holdPivot.position.z);
                itemHolding.transform.parent = holdPivot.transform;
                //item.GetComponent<BoxCollider>().enabled = false;
                item.GetComponent<Rigidbody>().isKinematic = true;

                isHoldingitem = true;

                itemHolding.tag = "CurrentHeldItem";
            }
        }
    }
}