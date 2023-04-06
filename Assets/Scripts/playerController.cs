using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private static Rigidbody rb;
    public SpriteRenderer sr;
    public CapsuleCollider cc;
    public Animator anim;

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
    public static List<Interactable> interaction = new List<Interactable>();
    public Interactable[] inter;
    public Transform dropShadow;
    public float groundDepth;
    public bool isHoldingitem = false;
    public bool inTitleScene;
    private float chargeCounter = 0.0f;
    public float chargeMax;
    public int strength;
    public static GameObject item;
    private bool longInteracting = false;

    public static Interactable CurrentPriority
    {
        get
        {
            if(interaction.Count > 0)
            {
                if (item == null)
                {
                    float closest = 100;
                    int current = -1;
                    for (int i = 0; i < interaction.Count; i++)
                    {
                        if (interaction[i] is Item)
                        {
                            float d = Vector3.Distance(interaction[i].transform.position, rb.position);
                            if (d < closest)
                            {
                                closest = d;
                                current = i;
                            }
                        }
                    }
                    return current == -1 ? interaction[0] : interaction[current];
                }
                else
                {
                    for (int i = 0; i < interaction.Count; i++)
                    {
                        if (!interaction[i].GetEmptyHandsCheck()) return interaction[i];
                    }
                    return null;
                }
            }
            else return null;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetChild(0).GetComponent<Rigidbody>();
        //cc = GetComponent<CapsuleCollider>();
        //anim = GetComponent<Animator>();

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
        inter = interaction.ToArray();
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
        bool canInteract = CurrentPriority != null;
        bool hasItem = item != null;
        bool passedEmptyHandsCheck = canInteract &&
            !(hasItem && CurrentPriority.GetEmptyHandsCheck());
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (passedEmptyHandsCheck && CurrentPriority.longInteraction)
            {
                CurrentPriority.StartInteractiveProcess();
                longInteracting = true;
            }
        }
        else if (Input.GetKey(KeyCode.E) && hasItem)
        {
            chargeCounter += Time.deltaTime;

        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            if (canInteract && !CurrentPriority.longInteraction)
            {
                if (passedEmptyHandsCheck)
                    CurrentPriority.StartInteractiveProcess();
                else
                {
                    if (chargeCounter >= 0.25f && chargeCounter < 0.75f)
                        item.transform.position = rb.position + facing + (Vector3.up * 0.5f);
                    else if (chargeCounter >= 0.75f)
                        item.GetComponent<Rigidbody>().velocity = facing * strength;

                    item.GetComponent<Item>().PutDown();
                    item = null;
                    chargeCounter = 0.0f;
                }
            }
            else if (canInteract && CurrentPriority.longInteraction && longInteracting)
            {
                longInteracting = CurrentPriority.StopInteractiveProcess();
            }
            else if ((!canInteract || !longInteracting) && hasItem)
            {
                if (chargeCounter >= 0.5f)
                {
                    item.GetComponent<Rigidbody>().velocity = facing * strength;
                }
                else
                {
                    item.transform.position = rb.position + facing + (Vector3.up * 0.5f);
                }
                item.GetComponent<Item>().PutDown();
                item = null;
                chargeCounter = 0.0f;
            }
        }
    }
}
