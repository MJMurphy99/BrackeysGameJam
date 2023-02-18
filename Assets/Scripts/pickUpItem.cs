using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpItem : MonoBehaviour
{
    //private Transform playerPickUpTransform;
    public LayerMask pickUpMask;

    public Vector3 direction { get; set; }

    public Transform holdPivot;
    public static GameObject item;

    public static bool isHoldingitem = false;

    private float chargeCounter = 0.0f;
    public float chargeMax;
    public int strength;
    private playerController pc;

    private void Start()
    {
        pc = GetComponent<playerController>();
    }

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
                if(playerController.interaction != null)
                {
                    if(!playerController.interaction.GetEmptyHandsCheck())
                        playerController.interaction.StartInteractiveProcess();
                }
                else
                {
                    if (chargeCounter >= 0.25f)
                        item.GetComponent<Rigidbody>().velocity = playerController.facing * strength;
                    else
                        item.transform.position = transform.position * 1f;


                    RemoveItemFromHands();
                    chargeCounter = 0.0f;
                }
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
                item.transform.position = 
                    new Vector3(holdPivot.position.x, holdPivot.position.y, holdPivot.position.z);
                item.transform.parent = holdPivot.transform;
                item.GetComponent<Rigidbody>().isKinematic = true;

                isHoldingitem = true;
            }
        }
    }

    public static void RemoveItemFromHands()
    {
        item.transform.parent = null;
        item.GetComponent<Rigidbody>().isKinematic = false;

        isHoldingitem = false;
    }
}