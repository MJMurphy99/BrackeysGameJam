using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FlashRed");
    }

    private IEnumerator FlashRed()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float mod = 4.0f;
        float incrmnt = 255 / mod, total = 255, c = total, rate = 1;// 2 / (2 * mod);
        bool changedSign = false, final3Sec = false;
        //for (int i = 0; i < 10; i++)
        //{
            for (int j = 0; j < 4; j++)
            {
                //if (j == 0) changedSign = false;
                c -= incrmnt;
            print(sr.color);
                sr.color = new Color(total, c, c);
            print(sr.color);
            yield return new WaitForSeconds(rate);
                /*if (!changedSign && j == mod - 1)
                {
                    changedSign = true;
                    incrmnt = -incrmnt;
                }*/
            }
            /*if (!final3Sec && i == 1)
            {
                final3Sec = true;
                mod = 2.0f;
                incrmnt = 255 / mod;
                rate = 1 / (2 * mod);
            }*/
        //}
    }
}
