using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTurnOff : MonoBehaviour
{
    [Header("Generator Lights and Button")]
    [SerializeField] private GameObject greenLight;
    [SerializeField] private GameObject redLight;

    private bool button;

    [Header("Generator Sound Effects and Animator")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator animator;
    [SerializeField] Player player;

    private float radius = 2f;

    [SerializeField] private AudioClip objectiveCompletedSound;
    private void Awake()
    {
        button = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetKeyDown("q") && Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            button = true;
            animator.enabled = false;
            greenLight.SetActive(false);
            redLight.SetActive(true);
            audioSource.Stop();
            audioSource.PlayOneShot(objectiveCompletedSound);
            ObjectivesComplete.Instance.GetObjectivesDone(true, true, true, false);


        }
        else if(button == false)
        {
            greenLight.SetActive(true);
            redLight.SetActive(false);
        }
    }
}
