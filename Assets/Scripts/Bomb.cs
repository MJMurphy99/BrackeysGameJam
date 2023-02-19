using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
    public Sprite explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlowUp()
    {
        sr.sprite = explosion;
        GetComponent<Rigidbody>().isKinematic = true;
        c.enabled = false;
        StartCoroutine("Deteriorate");
    }

    private IEnumerator Deteriorate()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }

}
