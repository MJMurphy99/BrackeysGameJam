using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssembleToy : Interactable
{
    private float timeSpent;
    public float totalTime;
    public float speed;

    private GameObject parts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartInteractiveProcess()
    {
        parts = pickUpItem.item;//When new types of items (toys and packages) are added, this will need to be updated to check for that
       // pickUpItem.
        StartCoroutine("ToyAssembly");
    }

    public void CancelInteractiveProcess()
    {
        StopCoroutine("ToyAssembly");
        timeSpent = 0;
    }

    private IEnumerator ToyAssembly()
    {
        while(timeSpent < totalTime)
        {
            yield return new WaitForSeconds(speed);
            timeSpent += speed;
        }

        timeSpent = 0;
        //Either replace parts item with toy item or activate toy state in a generic item base
    }
}
