using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool needEmptyHands, longInteraction;
    public Transform playerHands;

    public SpriteRenderer sr;
    private Color highlight = new Color(255, 235, 0, 255);
    public bool interactable = true;
    private bool playerWaiting = false;
    public bool haltInteraction = false;
   

    // Update is called once per frame

    public abstract void StartInteractiveProcess();

    public bool StopInteractiveProcess()
    {
        ToggleInteractivity();
        haltInteraction = true;
        interactable = true;
        return false;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        sr = GetComponent<SpriteRenderer>();
        if (other.CompareTag("Player"))
        {
            if (interactable)
            {
                playerController.interaction.Add(this);
                sr.color = highlight;
            }
            playerWaiting = true;
        }   
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactable)
            {
                playerController.interaction.Remove(this);
                sr.color = Color.white;
            }
            playerWaiting = false;
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
            if(playerController.interaction.Find((i) => { return i == this; }) != null)
                playerController.interaction.Remove(this);
        }
        else
        {
            if(playerWaiting)
            {
                playerController.interaction.Add(this);
                sr.color = highlight;
            }
        }
    }
}
