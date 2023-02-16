using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    private CharacterController cc;
    private Rigidbody rb;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        cc.Move(move * Time.deltaTime * speed);
    }
}
