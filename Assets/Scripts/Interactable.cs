using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool needEmptyHands;

    private SpriteRenderer sr;
    private Color highlight = new Color(255, 235, 0, 255);

    // Update is called once per frame

    public abstract void StartInteractiveProcess();

    private void OnTriggerEnter(Collider other)
    {
        sr = GetComponent<SpriteRenderer>();
        if (other.CompareTag("Player"))
        {
            playerController.interaction = this;
            sr.color = highlight;
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.interaction = null;
            sr.color = Color.white;
        }   
    }

    public bool GetEmptyHandsCheck()
    {
        return needEmptyHands;
    }
}
