using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithinWorkArea : MonoBehaviour
{
    public float totalWarningT;
    private float terminal = 3.0f, caution;
    private bool warning = false;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        caution = totalWarningT - terminal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            DeathByBoss.inWorkSpace = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            DeathByBoss.inWorkSpace = false;
    }

    public void Warning()
    {
        StartCoroutine("FlashGreen");
    }

    private IEnumerator FlashGreen()
    {
        float mod = 4.0f;
        float incrmnt = 0.5f / mod, total = 0, c = total, rate = 0.5f / (2 * mod);
        bool changedSign = false, final3Sec = false;

        int durr = (int)(caution / 0.5f);

        for (int i = 0; i < durr; i++)
        {
            for (int j = 0; j < (int)mod * 2; j++)
            {
                if (j == 0) changedSign = false;
                c += incrmnt;
                sr.color = new Color(0, 1, 0, c);
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
                incrmnt = 0.5f / mod;
                rate = 0.125f / (2 * mod);
                durr += (int)(terminal / 0.125f);
            }
        }

        Activate();
    }

    private void Activate()
    {
        sr.color = new Color(0, 1, 0, 0.5f);
    }

    public void Deactivate()
    {
        sr.color = new Color(0, 1, 0, 0);
    }
}
