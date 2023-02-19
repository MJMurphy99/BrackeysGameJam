using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BathroomInteraction : Interactable
{

    public float bathroomTimer, fullyRefreshedBathroomTime, alertTime;

    public GameObject bathroomIndicator;
    // Start is called before the first frame update
    void Start()
    {
        bathroomIndicator.SetActive(false);
    }

    public void Update()
    {
        bathroomTimer -= Time.deltaTime;

        if (bathroomTimer < alertTime && bathroomTimer > 0f)
        {
            bathroomIndicator.SetActive(true);
        }
        else if (bathroomTimer < 0f)
        {
            //kill the player
            StartCoroutine(loadGraveScene());
        } else
        {
            bathroomIndicator.SetActive(false);
        }
    }

    public override void StartInteractiveProcess()
    {
        bathroomTimer = fullyRefreshedBathroomTime;
    }

    public IEnumerator loadGraveScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }
}
