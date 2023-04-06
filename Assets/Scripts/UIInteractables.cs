using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractables : Interactable
{
    public int whichButton;
    public int whichSound;
    public ButtonManager bm;
    public butoonSounds bs;

    public override void StartInteractiveProcess()
    {
        bm.SelectButton(whichButton);
        bs.SoundType(whichSound);
    }
}
