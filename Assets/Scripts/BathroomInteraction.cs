using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BathroomInteraction : Interactable
{
    public ParticleSystem ps;
    public float firstIndication, secondIndication;
    public float totalWarningT;
    public DifficultyScalar ds;
    public float bathroomTimer, fullyRefreshedBathroomTime, refreshSpeed;

    public GameObject bathroomIndicator, player;
    private bool depressurizing = false;

    public Sprite[] timerPhases;
    public float[] timeStamps;
    private int currentSpriteIndex;
    public SpriteRenderer timerSR, pSR;
    public GameObject timer; 
    public float[] thresholds;
    public int[] decrementMod;
    private float terminal = 3.0f, caution;
    private bool warning = false;

    // Start is called before the first frame update
    void Start()
    {
        bathroomIndicator.SetActive(false);
        caution = totalWarningT - terminal;
        pSR = player.GetComponent<SpriteRenderer>();

        if (GlobalControl.playerDiaperPowerCollected)
        {
            fullyRefreshedBathroomTime = fullyRefreshedBathroomTime * 2;
        }
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
            player.SetActive(true);
        }

        if(bathroomTimer <= firstIndication && bathroomTimer > secondIndication)
        {
            if (!bathroomIndicator.activeSelf) bathroomIndicator.SetActive(true);
        }
        else if(bathroomTimer <= secondIndication && bathroomTimer > totalWarningT)
        {
            if (!ps.isPlaying) ps.Play();
        }
        else if (!warning && bathroomTimer < totalWarningT)
        {
            warning = true;
            StartCoroutine("FlashRed");
        }
    }

    private void RandomDecrement()
    {
        if (!ds.grace)
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
            bathroomTimer -= useMod * Time.deltaTime;
        }
    }

    public override void StartInteractiveProcess()
    {
        StartCoroutine("RelievePressure");
        player.SetActive(false);
    }

    public IEnumerator RelievePressure()
    {
        pSR.color = Color.white;
        StopCoroutine("DeactivateTimer");
        StopCoroutine("FlashRed");
        UpdateTimer();
        timer.SetActive(true);
        depressurizing = true;
        while (bathroomTimer < fullyRefreshedBathroomTime)
        {
            yield return new WaitForSeconds(0.5f);
            bathroomTimer += refreshSpeed;
            if(bathroomTimer > totalWarningT && bathroomTimer < secondIndication)
            {
                warning = false;
            }
            else if(bathroomTimer > secondIndication && bathroomTimer < firstIndication)
            {
                if (ps.isPlaying) ps.Stop();
            }
            else if(bathroomTimer > firstIndication)
            {
                bathroomIndicator.SetActive(false);
            }
            UpdateTimer();
        }
        if (bathroomTimer > fullyRefreshedBathroomTime)
            bathroomTimer = fullyRefreshedBathroomTime;
        depressurizing = false;
        player.SetActive(true);
        ToggleInteractivity();
        interactable = true;
        StartCoroutine("DeactivateTimer");
    }

    private void UpdateTimer()
    {
        float percentComplete = bathroomTimer / fullyRefreshedBathroomTime * 100;
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

    private IEnumerator FlashRed()
    {
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
                pSR.color = new Color(total, c, c);
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

    private void BlowUp()
    {
        StartCoroutine(loadGraveScene());
    }
}
