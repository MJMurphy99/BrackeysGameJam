using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Rigidbody rb;
    public SpriteRenderer sr;
    //public Animator anim;

    public float speed;

    private Vector2 moveInput;

    //Jump
    public float jumpForce;
    public bool isGrounded = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        //moveInput.Normalize();

        rb.velocity = new Vector3(moveInput.x * speed, rb.velocity.y, moveInput.y * speed);


        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    rb.velocity = new Vector3(rb.velocity.x - speed, rb.velocity.y, rb.velocity.z);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    rb.velocity = new Vector3(rb.velocity.x + speed, rb.velocity.y, rb.velocity.z);
        //}
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z * speed);
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z * -speed);
        //}
        //if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        //{
        //    rb.velocity = new Vector3(0, rb.velocity.y, 0);
        //}




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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
