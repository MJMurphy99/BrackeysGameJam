using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractables : Interactable
{
    public int whichButton;
    public int whichSound;
    public ButtonManager bm;
    public butoonSounds bs;
    public Animator anim;

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
            anim.SetBool("open", true);
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            base.OnTriggerExit(other);
            anim.SetBool("open", false);
        }
    }
}
