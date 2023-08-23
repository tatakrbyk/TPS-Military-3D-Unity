using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class FootStepsSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [Header("FootSteps Sources")]
    [SerializeField] private AudioClip[] footStepsSound;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private AudioClip GetRandomFootStep()
    {
        return footStepsSound[UnityEngine.Random.Range(0, footStepsSound.Length)];
    }

    private void Step()
    {
        AudioClip clip = GetRandomFootStep();
        audioSource.PlayOneShot(clip);
    }

}
