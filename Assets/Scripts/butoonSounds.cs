using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butoonSounds : MonoBehaviour
{
    public void SoundType(int index)
    {
        switch (index)
        {
            case 0:
            {
                playGeneral();
                break;
            }
            case 1:
            {
                playPlayButoon();
                break;
            }
        }
    }

    public void playGeneral()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_generalbutton");
    }

    public void playPlayButoon()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_playbutton");
    }
}
