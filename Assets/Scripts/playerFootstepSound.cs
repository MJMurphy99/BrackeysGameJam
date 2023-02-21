using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFootstepSound : MonoBehaviour
{

    public void PlayFootstep()
    {
        if(playerController.onGround == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/player_footstep");
        }
    }

}
