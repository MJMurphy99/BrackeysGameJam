using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BathroomInteraction : Interactable
{

    public float bathroomTimer, fullyRefreshedBathroomTime, alertTime, refreshSpeed;

    public GameObject bathroomIndicator, player;
    private bool depressurizing = false;

    public Sprite[] timerPhases;
    public float[] timeStamps;
    private int currentSpriteIndex;
    public SpriteRenderer timerSR;
    public GameObject timer;

    // Start is called before the first frame update
    void Start()
    {
        bathroomIndicator.SetActive(false);
    }

    public void Update()
    {
        if (!depressurizing)
            bathroomTimer -= Time.deltaTime;

        if (haltInteraction)
        {
            StopCoroutine("RelievePressure");
            StartCoroutine("DeactivateTimer");
            haltInteraction = false;
            depressurizing = false;
            player.SetActive(true);
        }

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
        StartCoroutine("RelievePressure");
        player.SetActive(false);
    }

    public IEnumerator RelievePressure()
    {
        StopCoroutine("DeactivateTimer");
        UpdateTimer();
        timer.SetActive(true);
        depressurizing = true;
        while (bathroomTimer < fullyRefreshedBathroomTime)
        {
            yield return new WaitForSeconds(0.5f);
            bathroomTimer += refreshSpeed;
            UpdateTimer();
        }
        if (bathroomTimer > fullyRefreshedBathroomTime)
            bathroomTimer = fullyRefreshedBathroomTime;
        depressurizing = false;
        player.SetActive(true);
        StartCoroutine("DeactivateTimer");
    }

    private void UpdateTimer()
    {
        float percentComplete = bathroomTimer / fullyRefreshedBathroomTime * 100;
        print(percentComplete);
        int phase = 0;

        for (int i = timeStamps.Length - 1; i >= 0; i--)
        {
            if (percentComplete >= timeStamps[i])
            {
                phase = i;
                break;
            }
        }

        if (phase != currentSpriteIndex)
        {
            currentSpriteIndex = phase;
            timerSR.sprite = timerPhases[phase];
        }
    }

    private IEnumerator DeactivateTimer()
    {
        yield return new WaitForSeconds(0.75f);
        timer.SetActive(false);
    }

    public IEnumerator loadGraveScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }
}
