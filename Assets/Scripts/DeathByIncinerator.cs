using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathByIncinerator : MonoBehaviour
{
    public ParticleSystem ps;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine("BelchSmoke");
    }

    private IEnumerator BelchSmoke()
    {
        ps.Play();
        yield return new WaitForSeconds(0.5f);
        GlobalControl.deathCounter++;
        GlobalControl.causeOfDeath = 3;
        SceneManager.LoadScene(2);
    }
}
