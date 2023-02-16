using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Rigidbody rb;
    public SpriteRenderer sr;
    public CapsuleCollider cc;
    //public Animator anim;

    public float speed;
    public float beltSpeedLeft;
    public float beltSpeedRight;

    private Vector2 moveInput;

    //Jump
    public float jumpForce;
    public Transform raycastObject;
    public LayerMask groundMask;
    public LayerMask beltMask;
    //public bool isGrounded = true;

    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        if (!isOnBelt())
        {
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");
            //moveInput.Normalize();

            rb.velocity = new Vector3(moveInput.x * speed, rb.velocity.y, moveInput.y * speed);
        }
        else if(isOnBelt())
        {
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");

            rb.velocity = speed * direction * Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector3(moveInput.x * beltSpeedLeft, rb.velocity.y, moveInput.y * speed);
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    rb.velocity = new Vector3(moveInput.x * beltSpeedLeft, rb.velocity.y, moveInput.y * beltSpeedLeft);
                }
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector3(moveInput.x * beltSpeedRight, rb.velocity.y, moveInput.y * speed);
                if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    rb.velocity = new Vector3(moveInput.x * beltSpeedRight, rb.velocity.y, moveInput.y * beltSpeedRight);
                }

            }

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector3(moveInput.x, rb.velocity.y, moveInput.y * speed);
            }
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        //Debug(raycastObject.position, Vector3.up * .1f, Color.red);


        //Flip Sprite
        if (!sr.flipX && moveInput.x < 0)
        {
            sr.flipX = true;
        }
        else if(sr.flipX && moveInput.x > 0)
        {
            sr.flipX = false;
        }


        //animator control
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    anim.SetBool("Side", true);
        //    anim.SetBool("Backwards", false);
        //    anim.SetBool("Forward", false);
        //    anim.SetBool("Idle", false);
        //}

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    anim.SetBool("Side", true);
        //    anim.SetBool("Backwards", false);
        //    anim.SetBool("Forward", false);
        //    anim.SetBool("Idle", false);
        //}

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    anim.SetBool("Backwards", true);
        //    anim.SetBool("Side", false);
        //    anim.SetBool("Forward", false);
        //    anim.SetBool("Idle", false);
        //}

        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    anim.SetBool("Forward", true);
        //    anim.SetBool("Backwards", false);
        //    anim.SetBool("Side", false);
        //    anim.SetBool("Idle", false);
        //}

        //if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        //{
        //    rb.velocity = new Vector3(0, 0, 0);

        //    anim.SetBool("Idle", true);
        //    anim.SetBool("Side", false);
        //    anim.SetBool("Forward", false);
        //    anim.SetBool("Backwards", false);
        //}
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(raycastObject.position, 0.1f, groundMask);
        return Physics.CheckSphere(raycastObject.position, 0.1f, beltMask);
    }

    bool isOnBelt()
    {
        return Physics.CheckSphere(raycastObject.position, 0.1f, beltMask);
    }
}
