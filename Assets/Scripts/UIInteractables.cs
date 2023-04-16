using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInteractables : Interactable
{
    public int whichButton;
    public int whichSound;
    public ButtonManager bm;
    public butoonSounds bs;
    public Animator anim;
    public GameObject interactBubble;
    public TextMeshProUGUI txt;

    private float playerX;

    public override void StartInteractiveProcess()
    {
        bm.SelectButton(whichButton);
        bs.SoundType(whichSound);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            base.OnTriggerEnter(other);

            /*playerX = other.transform.position.x;

            bool a = playerX > transform.position.x && interactBubble.transform.position.x > 0;
            bool b = playerX < transform.position.x && interactBubble.transform.position.x < 0;
            if (a || b) FlipInteractableBubble();*/


            anim.SetBool("open", true);
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool c = Mathf.Abs(playerX - other.transform.position.x) > 0.1f;
            playerX = other.transform.position.x;
            bool a = playerX > transform.position.x && interactBubble.transform.position.x > 0;
            bool b = playerX < transform.position.x && interactBubble.transform.position.x < 0;
            if ((a || b) && c) FlipInteractableBubble();
        }
    }*/

    public override void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            base.OnTriggerExit(other);
            anim.SetBool("open", false);
        }
    }

    private void FlipInteractableBubble()
    {
        float x = interactBubble.transform.position.x * -1;
        float y = interactBubble.transform.position.y;
        float z = interactBubble.transform.position.z;
        interactBubble.transform.position = new Vector3(x, y, z);

        x = interactBubble.transform.localScale.x * -1;
        y = interactBubble.transform.localScale.y;
        z = interactBubble.transform.localScale.z;
        interactBubble.transform.localScale = new Vector3(x, y, z);

        x = txt.transform.localScale.x * -1;
        y = txt.transform.localScale.y;
        z = txt.transform.localScale.z;
        txt.transform.localScale = new Vector3(x, y, z);

        x = x < 0 ? 8 : 0;
        y = txt.transform.position.y;
        z = txt.transform.position.z;
        txt.transform.position = new Vector3(x, y, z);
    }
}
