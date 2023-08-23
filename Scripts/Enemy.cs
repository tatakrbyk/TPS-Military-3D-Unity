using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    [Header("Enemy Health and Damage")]
    private float enemyHealth = 120f;
    private float presentHealth;
    public float giveDamage = 4f;
    [SerializeField] private HealthBarUI healthBarUI;

    [Header("Enemy Things")]
    [SerializeField] NavMeshAgent enemyAgent;
    [SerializeField] Transform LookPoint;
    [SerializeField] Camera shootingRaycastArea;
    [SerializeField] Transform playerBody;
    [SerializeField] LayerMask PlayerLayer;


    [Header("Enemy Guarding Var")]
    [SerializeField] private GameObject[] walkPoints;
    int currentEnemyPosition = 1;
    private float enemySpeed = 1.6f;
    private float walkingPointRadius = 2;

    [Header("Sounds and UI")]
    [SerializeField] private AudioClip shootingSound;
    [SerializeField] private AudioSource auidoSource;

    [Header("Enemy Shooting Var")]
    private float timebtwShoot = 0.5f;
    private bool previouslyShoot;

    [Header("Enemy Animation and Spark effect")]
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem muzzleSpark;

    [Header("Enemy mood/situations")]
    [SerializeField] private float visionRadius = 20;
    [SerializeField] private float shootingRadius = 9;
    private bool playerInVisionRadius;
    private bool playerInShootInRadius;

    private void Awake()
    {
        auidoSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        presentHealth = enemyHealth;
        healthBarUI.GiveFullHealth(enemyHealth);
        playerBody = GameObject.Find("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInShootInRadius = Physics.CheckSphere(transform.position, shootingRadius, PlayerLayer);

        if (!playerInVisionRadius && !playerInShootInRadius) Guard();
        if (playerInVisionRadius && !playerInShootInRadius) Purseuplayer();
        if (playerInVisionRadius && playerInShootInRadius) ShootPlayer();
    }

    private void ShootPlayer()
    {
        enemyAgent.SetDestination(transform.position);
        transform.LookAt(LookPoint);

        if(!previouslyShoot)
        {
            muzzleSpark.Play();
            auidoSource.PlayOneShot(shootingSound);
            RaycastHit hit;
            if (Physics.Raycast(shootingRaycastArea.transform.position, shootingRaycastArea.transform.forward, out hit, shootingRadius))
            {
                Debug.Log("Shooting" + hit.transform.name);

                Player playerBody = hit.transform.GetComponent<Player>();
                if(playerBody != null)
                {
                    playerBody.playerHitDamage(giveDamage);
                }

                animator.SetBool("Shoot", true);
                animator.SetBool("Walk", false);
                animator.SetBool("AimRun", false);
                animator.SetBool("AimDie", false);
                animator.SetBool("Die", false);
            }
            previouslyShoot = true;
            Invoke(nameof(ActiveShooting), timebtwShoot);
        }
    }

    private void Purseuplayer()
    {
        if(enemyAgent.SetDestination(playerBody.position))
        {
            animator.SetBool("Walk", false);
            animator.SetBool("AimRun", true);
            animator.SetBool("Shoot", false);

            visionRadius = 30f;
            shootingRadius = 15f;
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("AimRun", false);
            animator.SetBool("Shoot", false);

        }

        
    }

    private void Guard()
    {
        if (Vector3.Distance(walkPoints[currentEnemyPosition].transform.position, transform.position) < walkingPointRadius)
        {
            currentEnemyPosition = Random.Range(0, walkPoints.Length);
            if(currentEnemyPosition >= walkPoints.Length)
            {
                currentEnemyPosition = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentEnemyPosition].transform.position, Time.deltaTime * enemySpeed);

        // Changin enemy facing

        transform.LookAt(walkPoints[currentEnemyPosition].transform.position);
    }

    private void ActiveShooting()
    {
        previouslyShoot = false;

    }

    public void enemyHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        healthBarUI.SetHealth(presentHealth);

        // + vision and shooting radius
        visionRadius = 40f;
        shootingRadius = 30f;

        if(presentHealth <= 0)
        {
            animator.SetBool("Shoot", false);
            animator.SetBool("Walk", false);
            animator.SetBool("AimRun", false);
            animator.SetBool("AimDie", true);
            animator.SetBool("Die", true);
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        enemyAgent.SetDestination(transform.position);
        enemySpeed = 0f;
        shootingRadius = 0f;
        visionRadius = 0f;
        playerInVisionRadius = false;
        playerInShootInRadius = false;
        Object.Destroy(gameObject, 5.0f);
    }
}
