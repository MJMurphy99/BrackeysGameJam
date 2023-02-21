using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoilerInteraction : Interactable
{
    public CameraShake cameraShake;

    public GameObject explosion;

    public GameObject boilerIndicator;

    private bool playSound = false;

    public float boilerTimer, fullyRefreshedBoilerTime, alertTime;

    public void Start()
    {
        boilerIndicator.SetActive(false);
        playSound = false;
    }

    public void Update()
    {
        boilerTimer -= Time.deltaTime;


        if (boilerTimer < alertTime && boilerTimer > 0f)
        {
            boilerIndicator.SetActive(true);
        }
        else if (boilerTimer < 0f)
        {
            //FMODUnity.RuntimeManager.PlayOneShot("event:/big_explosion");

            cameraShake.enabled = true;
            Instantiate(explosion, new Vector3(-1.73f, 6.87f, 6.6f), Quaternion.identity);
            Instantiate(explosion, new Vector3(21.63f, 6.87f, 6.6f), Quaternion.identity);
            Instantiate(explosion, new Vector3(20.23f, 6.59f, -8.21f), Quaternion.identity);
            Instantiate(explosion, new Vector3(-2.48f, 3.09f, -8.21f), Quaternion.identity);
            Instantiate(explosion, new Vector3(10.76f, 3.09f, 2.23f), Quaternion.identity);
            GlobalControl.playerMoney = GlobalControl.playerMoney - 20;
            StartCoroutine(loadGraveScene());
            PlayExplosionSound();
        }
        else
        {
            boilerIndicator.SetActive(false);
        }
    }

    public override void StartInteractiveProcess()
    {
        boilerTimer = fullyRefreshedBoilerTime;

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



