using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Rigidbody rb;
    public SpriteRenderer sr;
    public CapsuleCollider cc;
    private Animator anim;

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
    public static Vector3 facing;
    public static Interactable interaction = null;
    public GameObject dropShadow;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();

        if (GlobalControl.playerSpeedPowerCollected == true)
        {
            speed = speed * 1.5f;
        }

        if (GlobalControl.playerJumpPowerCollected == true)
        {
            jumpForce = jumpForce * 1.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            anim.Play("Main Character_Run");

            facing.x = Input.GetAxisRaw("Horizontal");
            if (Input.GetAxisRaw("Vertical") != 0)
                facing.z = Input.GetAxisRaw("Vertical");
            else facing.z = 0;
        }
        else if(Input.GetAxisRaw("Vertical") != 0)
        {
            anim.Play("Main Character_Run");

            facing.x = 0;
            facing.z = Input.GetAxisRaw("Vertical");
        }
        else
        {
            anim.Play("Main Character_Idle");
        }


        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(moveInput.x * speed, rb.velocity.y, moveInput.y * speed);
       

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
        return Physics.CheckSphere(raycastObject.position, 0.1f);
    }
}
