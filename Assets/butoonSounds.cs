using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butoonSounds : MonoBehaviour
{
    public void playGeneral()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_generalbutton");
    }

    public void playPlayButoon()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_playbutton");
    }
}
