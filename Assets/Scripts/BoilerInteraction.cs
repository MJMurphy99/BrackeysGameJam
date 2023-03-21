using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoilerInteraction : Interactable
{
    public CameraShake cameraShake;
    public DifficultyScalar ds;
    public float totalWarningT;
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
    private float terminal = 3.0f, caution;
    private bool warning = false;

    public void Start()
    {
        boilerIndicator.SetActive(false);
        playSound = false;
        caution = totalWarningT - terminal;
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

        if (!warning && boilerTimer <= totalWarningT)
        {
            warning = true;
            boilerIndicator.SetActive(true);
            StartCoroutine("FlashRed");
        }
    }

    private void BlowUp()
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

    private void RandomDecrement()
    {
        int rand = Random.Range(0, 99);
        int useMod = 0;
        for (int i = 0; i < thresholds.Length; i++)
        {
            if (rand <= ds.AdjustFrequency(thresholds[i]))
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
        StopCoroutine("FlashRed");
        UpdateTimer();
        timer.SetActive(true);
        depressurizing = true;
        while(boilerTimer < fullyRefreshedBoilerTime)
        {
            yield return new WaitForSeconds(0.5f);
            boilerTimer += refreshSpeed;
            if(boilerTimer > totalWarningT)
            {
                warning = false;
                boilerIndicator.SetActive(false);
            }
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

    private IEnumerator FlashRed()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float mod = 4.0f;
        float incrmnt = 1 / mod, total = 1, c = total, rate = 0.5f / (2 * mod);
        bool changedSign = false, final3Sec = false;

        int durr = (int)(caution / 0.5f);

        for (int i = 0; i < durr; i++)
        {
            for (int j = 0; j < (int)mod * 2; j++)
            {
                if (j == 0) changedSign = false;
                c -= incrmnt;
                sr.color = new Color(total, c, c);
                yield return new WaitForSeconds(rate);
                if (!changedSign && j == mod - 1)
                {
                    changedSign = true;
                    incrmnt = -incrmnt;
                }
            }

            if (!final3Sec && i == durr - 1)
            {
                final3Sec = true;
                mod = 1.0f;
                incrmnt = 1 / mod;
                rate = 0.125f / (2 * mod);
                durr += (int)(terminal / 0.125f);
            }
        }
        BlowUp();
    }

}