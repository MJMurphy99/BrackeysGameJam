using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameManager : MonoBehaviour
{
    private TextMeshProUGUI name;

    private Dictionary<int, KeyValuePair<string, int>> romanNumbersDictionary = new()
    {
        {
            0,
            new KeyValuePair<string, int>("I", 1)
        },
        {
            1,
            new KeyValuePair<string, int>("IV", 4)
        },
        {
            2,
            new KeyValuePair<string, int>("V", 5)
        },
        {
            3,
            new KeyValuePair<string, int>("IX", 9)
        },
        {
            4,
            new KeyValuePair<string, int>("X", 10)
        },
        {
            5,
            new KeyValuePair<string, int>("XL", 40)
        },
        {
            6,
            new KeyValuePair<string, int>("L", 50)
        },
        {
            7,
            new KeyValuePair<string, int>("XC", 90)
        },
        {
            8,
            new KeyValuePair<string, int>("C", 100)
        },
        {
            9,
            new KeyValuePair<string, int>("CD", 400)
        },
        {
            10,
            new KeyValuePair<string, int>("D", 500)
        },
        {
            11,
            new KeyValuePair<string, int>("CM", 900)
        },
        {
            12,
            new KeyValuePair<string, int>("M", 1000)
        }
    };

    // Start is called before the first frame update
    void Start()
    {
        name = GetComponent<TextMeshProUGUI>();
        SetName(GlobalControl.deathCounter);           
    }

    private void SetName(int i)
    {
        string n = "";
        switch (i)
        {
            case 0:
            {
                GlobalControl.playerName = "Pickle";
                name.text = GlobalControl.playerName;
                break;
            }
            case 1:
            {
                GlobalControl.playerName = "Pickle Jr.";
                name.text = GlobalControl.playerName;
                break;
            }
            default:
            {
                for (int j = romanNumbersDictionary.Count - 1; j >= 0; j--)
                {
                    while (i >= romanNumbersDictionary[j].Value)
                    {
                        n += romanNumbersDictionary[j].Key;
                        i -= romanNumbersDictionary[j].Value;
                    }
                }

                GlobalControl.playerName = "Pickle " + n;
                name.text = GlobalControl.playerName;

                break;
            }
        }    
    }
}
