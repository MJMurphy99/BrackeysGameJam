using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathByBoss : MonoBehaviour
{
    public GameObject bossInChair, bossAtDoor;
    public static bool inWorkSpace = false;
    public int appearanceProb, appearanceMod;
    public float checkFrequency;
    private bool checkingForBoss = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkingForBoss) StartCoroutine("CheckForBoss");
    }

    private IEnumerator CheckForBoss()
    {
        checkingForBoss = true;
        yield return new WaitForSeconds(checkFrequency);
        if (WillBossAppear()) StartCoroutine("BossAppearsAtDoor");
        else checkingForBoss = false;
    }

    private bool WillBossAppear()
    {
        return Random.Range(1, appearanceProb) <= appearanceMod;
    }

    private IEnumerator BossAppearsAtDoor()
    {
        bossInChair.SetActive(false);
        yield return new WaitForSeconds(5);
        bossAtDoor.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        if (!inWorkSpace)
        {
            bossAtDoor.GetComponent<Animator>().SetBool("MakeAngry", true);
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(2);
        }
        else 
        {
            yield return new WaitForSeconds(1);
            bossAtDoor.SetActive(false);
            bossInChair.SetActive(true);
            checkingForBoss = false;
        }
    }
}
