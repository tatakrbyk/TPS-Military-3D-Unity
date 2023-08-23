using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    
    [SerializeField] private CharacterController characterController;
    [Header("Camera")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private GameObject deathCamera;
    
    
    [SerializeField] private GameObject endGameMenuUI;

    [SerializeField] private Animator animator;

    [SerializeField] public float playerSpeed = 1.9f;
    [SerializeField] public float playerSprint = 4.5f;
    
    [SerializeField] private float tunCalmTime = 0.1f;
    private float turnCalmVelocity;

    // Player Health and Things
    private float playerHealth = 1000f;
    private float presentHealth;
    [SerializeField] private HealthBarUI healthBarUI;

    //player jumping and velocity 
    Vector3 velocity;
    private float jumpRange = 1f;

    [SerializeField] private Transform surfaceCheck;
    [SerializeField] private LayerMask surfaceMask;
    public float surfaceDistance = 0.4f;
    private bool onSurface;

    [Header("Sounds")]
    [SerializeField] private AudioClip playerHurtSound;
    [SerializeField] private AudioSource audioSource;

    private float gravity = -9.81f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();  
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        presentHealth = playerHealth;
        healthBarUI.GiveFullHealth(playerHealth);
    }
    private void Update()
    {
        SurfaceCheck();
        PlayerMovement();
        Sprint();
        Jump();
    }

    private void SurfaceCheck()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);

        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2;
        }

        // gravity

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void PlayerMovement()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

        if (moveDir.magnitude >= 0.1f)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Running", false);
            animator.SetBool("Idle", false);
            animator.SetBool("AimWalk", false);
            animator.SetBool("IdleAim", false);

            animator.SetTrigger("Jump");


            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y; //  Mathf.Atan2 -> arctan y/x, Mathf.Rad2Deg -> Radians to degress 360 / (Pi * 2) 
            float angle =Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, tunCalmTime); // Gradually changes an angle given in degress towards a desired goal angle over time.
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * playerSpeed * Time.deltaTime); 
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetTrigger("Jump");
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
            animator.SetBool("AimWalk", false);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && onSurface) {
            animator.SetBool("Walk", false);
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.ResetTrigger("Jump");
        }
    }

    private void Sprint()
    {
        if(Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onSurface)
        {
            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");

            Vector3 moveDir = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

            if (moveDir.magnitude >= 0.1f)
            {
                animator.SetBool("Running", true);
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", false);
                animator.SetBool("IdleAim", false);
                float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y; //  Mathf.Atan2 -> arctan y/x, Mathf.Rad2Deg -> Radians to degress 360 / (Pi * 2) 
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, tunCalmTime); // Gradually changes an angle given in degress towards a desired goal angle over time.
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characterController.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", false);
            }
        }
        
    }
    public void playerHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        healthBarUI.SetHealth(presentHealth);
        audioSource.PlayOneShot(playerHurtSound);

        if (presentHealth <= 0)
        {
            playerDie();
        }
    }

    private void playerDie()
    {
        endGameMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None; // Loc to mause cursor
        deathCamera.SetActive(true);

        Destroy(gameObject, 1.0f);


    }

}
