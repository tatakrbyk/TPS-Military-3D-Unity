using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectivesComplete : MonoBehaviour
{
    [Header("Objectives to Complete")]

    [SerializeField] private TextMeshProUGUI objective1;
    [SerializeField] private TextMeshProUGUI objective2;
    [SerializeField] private TextMeshProUGUI objective3;
    [SerializeField] private TextMeshProUGUI objective4;

    public static ObjectivesComplete Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public void GetObjectivesDone(bool obj1, bool obj2, bool obj3, bool obj4)
    {
        if(obj1 == true)
        {
            objective1.text = "1. Key picked up";
            objective1.color = Color.green;
        }
        else
        {
            objective1.text = "1. Find key to open the gate";
            objective1.color = Color.white;
        }

        if (obj2 == true)
        {
            objective2.text = "2. Computer is offline";
            objective2.color = Color.green;
        }
        else
        {
            objective2.text = "2. Shutdown the computer system";
            objective2.color = Color.white;
        }

        if (obj3 == true)
        {
            objective3.text = "3. Generators is offline";
            objective3.color = Color.green;
        }
        else
        {
            objective3.text = "3. Shutdown both of the generators";
            objective3.color = Color.white;
        }

        if (obj4 == true)
        {
            objective4.text = "4. Mission Completed <3";
            objective4.color = Color.green;
        }
        else
        {
            objective4.text = "4. Find vehicle and escape from the facility";
            objective4.color = Color.white;
        }
    }
}
