using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathByBoss : MonoBehaviour
{
    public GameObject bossInChair, bossAtDoor;
    public DifficultyScalar ds;
    public WithinWorkArea[] areas;
    public static bool inWorkSpace = false;
    public int appearanceProb, threshold;
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
        yield return new WaitForSeconds(ds.AdjustTimer(checkFrequency));
        if (WillBossAppear()) StartCoroutine("BossAppearsAtDoor");
        else checkingForBoss = false;
    }

    private bool WillBossAppear()
    {
        return Random.Range(0, appearanceProb) <= ds.AdjustFrequency(threshold);
    }

    private IEnumerator BossAppearsAtDoor()
    {
        for (int i = 0; i < areas.Length; i++) areas[i].Warning();
        bossInChair.SetActive(false);
        yield return new WaitForSeconds(8); 
        bossAtDoor.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        float durr = 0;
        while(durr < 3)
        {
            if (!inWorkSpace)
            {
                if (GlobalControl.playerHallPassPowerCollected == false)
                {
                    bossAtDoor.GetComponent<Animator>().SetBool("MakeAngry", true);
                    yield return new WaitForSeconds(1.0f);
                    GlobalControl.deathCounter++;
                    SceneManager.LoadScene(2);
                }
                else if (GlobalControl.playerHallPassPowerCollected == true)
                {
                    yield return new WaitForSeconds(1);
                    bossAtDoor.SetActive(false);
                    bossInChair.SetActive(true);
                    checkingForBoss = false;
                    GlobalControl.playerHallPassPowerCollected = false;
                    GlobalControl.PowerUpTotal--;
                }
            }
            yield return new WaitForSeconds(0.5f);
            durr += 0.5f;
        }

        bossAtDoor.SetActive(false);
        bossInChair.SetActive(true);
        checkingForBoss = false;
        for (int i = 0; i < areas.Length; i++) areas[i].Deactivate();
    }
}
