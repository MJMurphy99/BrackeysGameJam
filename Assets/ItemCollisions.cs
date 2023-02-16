using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollisions : MonoBehaviour
{
    public LayerMask Ground;
    public LayerMask Conveyor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Ground)
        {
            gameObject.tag = "OffBelt";
        }

        if (collision.gameObject.layer == Conveyor)
        {
            gameObject.tag = "OnBelt";
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == Ground)
    //    {
    //        parentOJ.gameObject.tag = "OffBelt";
    //    }

    //    if (other.gameObject.layer == Conveyor)
    //    {
    //        parentOJ.gameObject.tag = "OnBelt";
    //    }
    //}
}
