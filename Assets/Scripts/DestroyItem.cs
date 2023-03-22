using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DestroyItem : MonoBehaviour
{
    public ParticleSystem ps;
    public CameraShake cameraShake;

    public GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destructable"))
        {
            ps.Play();
            for (int i = 0; i < conveyorBelt.onBelt.Count; i++)
            {
                if (conveyorBelt.onBelt[i] == other.gameObject)
                {
                    conveyorBelt.onBelt.RemoveAt(i);
                    break;
                }
            }
            if (other.GetComponent<Toy>() != null)
            {
                //GlobalControl.playerMoney = GlobalControl.playerMoney - 7;
                Destroy(other.gameObject);
            }
            else if (other.GetComponent<Bomb>() != null)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/big_explosion");

                cameraShake.enabled = true;
                Instantiate(explosion, new Vector3(-1.73f, 6.87f, 6.6f), Quaternion.identity);
                Instantiate(explosion, new Vector3(21.63f, 6.87f, 6.6f), Quaternion.identity);
                Instantiate(explosion, new Vector3(20.23f, 6.59f, -8.21f), Quaternion.identity);
                Instantiate(explosion, new Vector3(-2.48f, 3.09f, -8.21f), Quaternion.identity);
                Instantiate(explosion, new Vector3(10.76f, 3.09f, 2.23f), Quaternion.identity);
                //GlobalControl.playerMoney = GlobalControl.playerMoney - 20;
                StartCoroutine(loadGraveScene());
            }
        }  
    }

    public IEnumerator loadGraveScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
    }
}
