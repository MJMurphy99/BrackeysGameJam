using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool needEmptyHands;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void StartInteractiveProcess();//Might not be point to this, come back later

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            playerController.interaction = this;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerController.interaction = null;
    }

    public bool GetEmptyHandsCheck()
    {
        return needEmptyHands;
    }
}
