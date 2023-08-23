using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    public Player player;

    // Rifle Things
    [SerializeField] private Camera cam;
    [SerializeField] private Animator animator;
    public float giveDamageOf = 10f;
    private float shootingRange = 100f;
    private float fireCharge = 15f;
    
    // Riffle Ammunition and Shooting
    private int maxAmmunition = 30;
    private int mag = 20;
    private int presentAmmunition;
    private bool setReloading = false;
    [SerializeField] private float reloadingTime = 1.3f;

    private float nextTimeToShoot = 0f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem muzzleSpark;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject goreEffect;
    [SerializeField] private GameObject droneEffect;

    [Header("Sounds And UI")]
    [SerializeField] private GameObject AmmoOutUI;
    [SerializeField] private int timeToShowUI = 1;
    [SerializeField] private AudioClip shootingSound;
    [SerializeField] private AudioClip reloadingSound;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        presentAmmunition = maxAmmunition;
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (setReloading) { return; }
        if (presentAmmunition < 0) {
           StartCoroutine(Reload());
           return;
        }

        SetAnimation(); 
        
    }

    private void Shoot()
    {
        

        // check for mag
        if( mag == 0) {

            // Show ammo out text
            StartCoroutine(ShowAmmoOut());

            return;
        }
        presentAmmunition--;

        if (presentAmmunition == 0)
        {
            mag--;
        }

        // Updating UI
        AmmoCount.Instance.UpdateAmmoText(mag);
        AmmoCount.Instance.UpdateMagText(presentAmmunition);

        muzzleSpark.Play();
        audioSource.PlayOneShot(shootingSound);
        RaycastHit hitInfo;

        bool hit = Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange);
        if(hit)
        {
            Debug.Log(hitInfo.transform.name);

            Objects objects = hitInfo.transform.GetComponent<Objects>();
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            EnemyDrone enemyDrone = hitInfo.transform.GetComponent<EnemyDrone>();

            if (objects != null)
            {
                objects.ObjectHitDamage(giveDamageOf);
                GameObject impactGo = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGo, 1f);
            }else if( enemy != null)
            {
                enemy.enemyHitDamage(giveDamageOf);
                GameObject impactGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGo, 2f);
            }else if (enemyDrone != null)
            {
                enemyDrone.enemyDroneHitDamage(giveDamageOf);
                GameObject impactGo = Instantiate(droneEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGo, 2f);
            }
        }
    }

    private void SetAnimation()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }else if(Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }else if(Input.GetButton("Fire1") && Input.GetButton("Fire2"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("Idle", true);
            animator.SetBool("FireWalk", false);
            animator.SetBool("Reloading", false);

        }
    }
    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        setReloading = true;
        Debug.Log("Reloading...");
        animator.SetBool("Reloading", true);
        audioSource.PlayOneShot(reloadingSound);
        yield return new WaitForSeconds(reloadingTime);
        animator.SetBool("Reloading", false);
        presentAmmunition = maxAmmunition;
        player.playerSpeed = 1.9f;
        player.playerSprint = 3.8f;
        setReloading = false;

        
    }

    IEnumerator ShowAmmoOut()
    {
        AmmoOutUI.SetActive(true);
        yield return new WaitForSeconds(timeToShowUI);
        AmmoOutUI.SetActive(false);

    }
}
