using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [Header("Computer On/Off")]

    [SerializeField] private bool lightsOn = true;
    private float radius = 2.5f;
    [SerializeField] private Light redLight;

    [Header("Computer Assign Things")]

    [SerializeField] private Player player;
    [SerializeField] private GameObject computerUI;
    private int showComputerUIfor = 5;

    [Header("Sounds")]
    [SerializeField] private AudioClip objectiveCompletedSound;
    [SerializeField] private AudioSource auidoSource;
    private void Awake()
    {
        auidoSource = GetComponent<AudioSource>();
        redLight = GetComponent<Light>();
    }
    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position)  < radius)
        {
            if(Input.GetKeyDown("q"))
            {
                StartCoroutine(showComputerUI());
                lightsOn = false;
                redLight.intensity = 0;
                auidoSource.PlayOneShot(objectiveCompletedSound);

                ObjectivesComplete.Instance.GetObjectivesDone(true, true, false, false);

            }
        }
    }

    IEnumerator showComputerUI()
    {
        computerUI.SetActive(true);
        yield return new WaitForSeconds(showComputerUIfor);
        computerUI.SetActive(false);
    }
}
