using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoilerInteraction : Interactable
{
    public CameraShake cameraShake;

    public GameObject explosion;

    public GameObject boilerIndicator;

    private bool playSound = false, depressurizing = false;

    public float boilerTimer, fullyRefreshedBoilerTime, alertTime, refreshSpeed;

    public Sprite[] timerPhases;
    public float[] timeStamps;
    private int currentSpriteIndex;
    public SpriteRenderer timerSR;
    public GameObject timer;
    public float[] thresholds;
    public int[] decrementMod;

    public void Start()
    {
        boilerIndicator.SetActive(false);
        playSound = false;
    }

    public void Update()
    {
        if (!depressurizing)
            RandomDecrement();
        if (haltInteraction)
        {
            StopCoroutine("RelievePressure");
            StartCoroutine("DeactivateTimer");
            haltInteraction = false;
            depressurizing = false;
        }

        if (boilerTimer < alertTime && boilerTimer > 0f)
        {
            boilerIndicator.SetActive(true);
        }
        else if (boilerTimer < 0f)
        {
            //FMODUnity.RuntimeManager.PlayOneShot("event:/big_explosion");

            cameraShake.enabled = true;
            Instantiate(explosion, new Vector3(15f, 4f, 2.23f), Quaternion.identity);
            Instantiate(explosion, new Vector3(13f, 4f, 2.23f), Quaternion.identity);
            Instantiate(explosion, new Vector3(11f, 2f, 2.23f), Quaternion.identity);
            Instantiate(explosion, new Vector3(10.76f, 3.09f, 2.23f), Quaternion.identity);
            StartCoroutine(loadGraveScene());
            PlayExplosionSound();
        }
        else
        {
            boilerIndicator.SetActive(false);
        }
    }

    private void RandomDecrement()
    {
        int rand = Random.Range(0, 99);
        int useMod = 0;
        for (int i = 0; i < thresholds.Length; i++)
        {
            if (rand <= thresholds[i])
            {
                useMod = decrementMod[i];
                break;
            }
        }
        boilerTimer -= useMod * Time.deltaTime;
    }

    public override void StartInteractiveProcess()
    {
        StartCoroutine("RelievePressure");
    }

    public IEnumerator RelievePressure()
    {
        StopCoroutine("DeactivateTimer");
        UpdateTimer();
        timer.SetActive(true);
        depressurizing = true;
        while(boilerTimer < fullyRefreshedBoilerTime)
        {
            yield return new WaitForSeconds(0.5f);
            boilerTimer += refreshSpeed;
            UpdateTimer();
        }
        if (boilerTimer > fullyRefreshedBoilerTime)
            boilerTimer = fullyRefreshedBoilerTime;
        depressurizing = false;
        StartCoroutine("DeactivateTimer");
    }

    private void UpdateTimer()
    {
        float percentComplete = boilerTimer / fullyRefreshedBoilerTime * 100;
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
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
    }

    public void PlayExplosionSound()
    {
        if(playSound == false)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/big_explosion");
            playSound = true;
        }
    }


}



