using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private GameObject AimCam;
    [SerializeField] private GameObject AimCanvas;
    [SerializeField] private GameObject ThirdPersonCam;
    [SerializeField] private GameObject ThirPersonCanvas;

    // camera animator
    [SerializeField] private Animator animator;
    private void Update()
    {
        if(Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("AimWalk", true);
            animator.SetBool("Walk", true);


            ThirdPersonCam.SetActive(false);
            ThirPersonCanvas.SetActive(false);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);
        }else if(Input.GetButton("Fire2")){

            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("AimWalk", false);
            animator.SetBool("Walk", false);

            ThirdPersonCam.SetActive(false);
            ThirPersonCanvas.SetActive(false);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);
        }else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("IdleAim", false);
            animator.SetBool("AimWalk", false);

            ThirdPersonCam.SetActive(true);
            ThirPersonCanvas.SetActive(true);
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);
        }
    }
}
