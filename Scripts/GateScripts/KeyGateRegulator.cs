using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyNetwork 
{
    public class KeyGateRegulator : MonoBehaviour
    {
        [Header("Animations")]

        [SerializeField] private Animator gateAnimation;
        private string openAnimationName = "GateOpen";
        private string closeAnimationName = "GateClose";

        private bool OpenGate = false;

        [Header("Time and UI")]
        [SerializeField] private GameObject showGateLockedUI = null;
        [SerializeField] private KeyList keyList = null;
        private int timeToShowUI = 1;
        private int waitTimer = 1;
        private bool pauseInteraction = false;

        [Header("Sound Effect")]
        [SerializeField] private AudioClip gateSound;
        [SerializeField] private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            gateAnimation = gameObject.GetComponent<Animator>();
        }

        public void StartAnimation()
        {
            if(keyList.hasKey)
            {
                Open_Gate();
            }
            else
            {
                StartCoroutine(showGateLocked());
            }
        }
        private IEnumerator stopGateInterConnection()
        {
            pauseInteraction = true;
            yield return new WaitForSeconds(waitTimer);
            pauseInteraction = false;
        }
        private void Open_Gate()
        {
            if(!OpenGate && !pauseInteraction)
            {
                gateAnimation.Play(openAnimationName, 0, 0.0f);
                audioSource.PlayOneShot(gateSound);
                OpenGate = true;
                ObjectivesComplete.Instance.GetObjectivesDone(true,false,false,false);
                StartCoroutine(stopGateInterConnection());
            }
            else if(OpenGate && !pauseInteraction) 
            {
                gateAnimation.Play(closeAnimationName, 0, 0.0f);
                audioSource.PlayOneShot(gateSound);
                OpenGate = false;
                StartCoroutine(stopGateInterConnection());

            }
        }

        IEnumerator showGateLocked()
        {
            showGateLockedUI.SetActive(true);
            yield return new WaitForSeconds(timeToShowUI);
            showGateLockedUI.SetActive(false);
        }
    }
}

