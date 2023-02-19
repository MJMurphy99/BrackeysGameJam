using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool needEmptyHands;
    public Transform playerHands;

    public SpriteRenderer sr;
    private Color highlight = new Color(255, 235, 0, 255);
    public bool interactable = true;
    private bool playerWaiting = false;
   

    // Update is called once per frame

    public abstract void StartInteractiveProcess();

    public virtual void OnTriggerEnter(Collider other)
    {
        sr = GetComponent<SpriteRenderer>();
        if (other.CompareTag("Player"))
        {
            if (interactable)
            {
                playerController.interaction = this;
                sr.color = highlight;
            }
            else playerWaiting = true;
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactable)
            {
                playerController.interaction = null;
                sr.color = Color.white;
            }
            else playerWaiting = false;
        }   
    }

    public bool GetEmptyHandsCheck()
    {
        return needEmptyHands;
    }

    public void ToggleInteractivity()
    {
        interactable = !interactable;
        if(!interactable)
        {
            sr.color = Color.white;
            if(playerController.interaction == this)
                playerController.interaction = null;
        }
        else
        {
            if(playerWaiting)
            {
                playerController.interaction = this;
                sr.color = highlight;
            }
        }
    }
}
