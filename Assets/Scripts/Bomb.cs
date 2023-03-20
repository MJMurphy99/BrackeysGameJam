using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomb : Item
{
    public Sprite explosion;
    private DifficultyScalar ds;

    public bool isExploded;
    private bool isIgnited = false;

    private bool playSound = false;

    public void Start()
    {
        ds = FindObjectOfType<DifficultyScalar>();
        playSound = false;
    }

    private void BlowUp()
    {
        GetComponent<Animator>().enabled = false;
        StartCoroutine("Deteriorate");
    }

    public void Fuse()
    {
        StartCoroutine("FlashRed");
    }

    private IEnumerator Deteriorate()
    {
        yield return new WaitForSeconds(ds.AdjustTimer(1.5f));
        PlayExplosionSound();
        sr.sprite = explosion;
        isExploded = true;

        yield return new WaitForSeconds(1.0f);
        isExploded = false;
        Destroy(gameObject);
    }

    private IEnumerator FlashRed()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float mod = 4.0f;
        float incrmnt = 1 / mod, total = 1, c = total, rate = 0.5f / (2 * mod);
        bool changedSign = false, final3Sec = false;
        print(incrmnt);
        for (int i = 0; i < 5; i++)
        {
            for(int j = 0; j < (int)mod * 2; j++)
            {
                if (j == 0) changedSign = false;
                c -= incrmnt;
                sr.color = new Color(total, c, c);
                yield return new WaitForSeconds(rate);
                if(!changedSign && j == mod - 1)
                {
                    changedSign = true;
                    incrmnt = -incrmnt;
                }
            }
            if(!final3Sec && i == 1)
            {
                final3Sec = true;
                mod = 1.0f;
                incrmnt = 1 / mod; 
                rate = 0.5f / (2 * mod);
            }
        }
        BlowUp();
    }

    public virtual void OnTriggerStay(Collider other)
    {
        sr = GetComponent<SpriteRenderer>();
        if (other.CompareTag("Player"))
        {
            if (isExploded)
            {
                StartCoroutine(loadGraveScene());
            }
        }
    }

    public IEnumerator loadGraveScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }

    public void PlayExplosionSound()
    {
        if (playSound == false)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/bomb_explosion");
            playSound = true;
        }
    }

    public override void PickUp()
    {
        base.PickUp();
        if (!isIgnited && transform.parent.CompareTag("Player"))
        {
            isIgnited = true;
            Fuse();
        }
    }
}
