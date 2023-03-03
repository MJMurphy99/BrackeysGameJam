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

    public static bool onGround;
    //public bool isGrounded = true;

    public Vector3 direction;
    public static Vector3 facing;
    public static Interactable interaction = null;
    public Transform dropShadow;
    public float groundDepth;
    public bool isHoldingitem = false;
    private float chargeCounter = 0.0f;
    public float chargeMax;
    public int strength;
    public static GameObject item;

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

        if (GlobalControl.playerThrowPowerCollected == true)
        {
            strength = strength * 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded())
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }

        UpdateDropShadow();
        Interact();
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
            FMODUnity.RuntimeManager.PlayOneShot("event:/player_jump");
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
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(raycastObject.position, 0.1f, groundMask);
    }

    private void UpdateDropShadow()
    {
        RaycastHit hit;
        Physics.Raycast(raycastObject.position, Vector3.down, out hit);

        groundDepth = hit.point.y;
        Vector3 shadowPos = dropShadow.position;
        dropShadow.transform.position = new Vector3(shadowPos.x, groundDepth, shadowPos.z);
    }

    private void Interact()
    {
        if (item != null)
        {
            if (Input.GetKey(KeyCode.E))
            {
                chargeCounter += Time.deltaTime;
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                if (interaction != null)
                {
                    if (!interaction.GetEmptyHandsCheck())
                        interaction.StartInteractiveProcess();
                    else
                    {
                        if (chargeCounter >= 0.25f && chargeCounter < 0.75f)
                            item.transform.position = transform.position * 1f;
                        else if (chargeCounter >= 0.75f)
                            item.GetComponent<Rigidbody>().velocity = playerController.facing * strength;

                        item.GetComponent<Item>().PutDown();
                        item = null;
                        chargeCounter = 0.0f;
                    }
                }
                else
                {
                    if (chargeCounter >= 0.5f)
                    {
                        if (item.GetComponent<Bomb>() != null)
                        {
                            item.GetComponent<Rigidbody>().velocity = facing * strength;
                            item.GetComponent<Bomb>().BlowUp();
                        } else
                        {
                            item.GetComponent<Rigidbody>().velocity = facing * strength;
                        }
                        
                    }  
                    else{
                        if (item.GetComponent<Bomb>() != null)
                        {
                            item.transform.position = transform.position + facing + (Vector3.up * 0.5f);
                            item.GetComponent<Bomb>().BlowUp();
                        } else
                        {
                            item.transform.position = transform.position + facing + (Vector3.up * 0.5f);
                        }
                    }
                    item.GetComponent<Item>().PutDown();
                    item = null;
                    chargeCounter = 0.0f;
                }
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.E) && interaction != null)
            {
                interaction.StartInteractiveProcess();
            }
        }
    }
}
