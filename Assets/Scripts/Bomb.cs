using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomb : Item
{
    public Sprite explosion;

    public bool isExploded;

    private bool playSound = false;

    public void Start()
    {
        playSound = false;
    }

    public void BlowUp()
    {
        this.GetComponent<Animator>().enabled = false;
        StartCoroutine("Deteriorate");
    }

    private IEnumerator Deteriorate()
    {
        yield return new WaitForSeconds(1.5f);
        PlayExplosionSound();
        sr.sprite = explosion;
        isExploded = true;

        yield return new WaitForSeconds(0.5f);
        isExploded = false;
        Destroy(gameObject);
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

}
