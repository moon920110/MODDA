using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    #region Variables
    public GameObject playerCam;
    public int range = 100;
    public int damage = 25;
    public Animator playerAnimator;
    public ParticleSystem muzzleFlash;
    public GameObject hitParticles;
    public AudioClip gunshot;
    public WeaponSway weaponSway;
    float swaySensitivity;
    public GameObject playerBullet;
    
    public GameObject p_bulletSpawner;
    public GameObject crosshair;
    public GameObject nonTargetHitParticles;
    public float firerate = 10;
    float firerateTimer = 0;
    public float numberofDestroyed;

    //Player's Bullet
    /*public GameObject p_bulletPrefab;
    public Transform p_bulletSpawn;
    public float bulletSpeed = 30;
    public float lifeTime = 3;*/

    //public bool isAutomatic;
    public string weaponType;
    public PlayerManager playerManager;
    public GameManager gameManager;
    public AudioSource MachineGun;

    #endregion
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        swaySensitivity = weaponSway.swaySensitivity;
        MachineGun = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        numberofDestroyed = 0;
    }

    private void OnEnable()
    {
        //playerAnimator.SetTrigger(weaponType);
    }

    /*private void OnDisable()
    {
        //playerAnimator.SetBool("isReloading", false);
        //isReloading = false;
        //Debug.Log("Reload Interupted");
    }*/


    void Update()
    {
        if (playerAnimator.GetBool("isShooting"))
        {
            playerAnimator.SetBool("isShooting", false);

        }
        if (firerateTimer > 0)
        {
            firerateTimer = firerateTimer - Time.deltaTime;
        }
        if (Input.GetButton("Fire1") && firerateTimer <= 0)// && isAutomatic)
        {
            Shoot();
            firerateTimer = 1 / firerate;
        }
        if (Input.GetButtonDown("Fire1") && firerateTimer <= 0)// && !isAutomatic)
        {
            Shoot();
            firerateTimer = 1 / firerate;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Aim();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            /*if (playerAnimator.GetBool("isAiming"))
            {
                playerAnimator.SetBool("isAiming", false);
            }*/
            weaponSway.swaySensitivity = swaySensitivity;
            crosshair.SetActive(true);
        }
        
    }

    void Shoot()
    {
        muzzleFlash.Play();
        MachineGun.Play();
        playerAnimator.SetBool("isShooting", true);
        //GameObject.Instantiate(playerBullet.transform, p_bulletSpawner.transform.position, p_bulletSpawner.transform.rotation);
        StartCoroutine(bulletFirerate());
        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            TurretManager turretManager = hit.transform.GetComponent<TurretManager>();
            StartCoroutine(bulletFirerate());
            if (turretManager != null)
            {
                turretManager.Hit(damage);
                if (turretManager.t_current_health <= 0)
                {
                    playerManager.Score += turretManager.scorePoints; //adding score to the player                    
                    numberofDestroyed = numberofDestroyed + 1;
                    //Debug.Log("Number of destroyed on weapon script: " + numberofDestroyed);

                }
                GameObject InstParticles = Instantiate(hitParticles, hit.point, Quaternion.LookRotation(hit.normal));
                InstParticles.transform.parent = hit.transform;
                Destroy(InstParticles, 0.005f);
            }
            else
            {
                GameObject InstParticles = Instantiate(nonTargetHitParticles, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(InstParticles, 0.032f);
            }
        }           
    }
    IEnumerator bulletFirerate() 
    {
        GameObject.Instantiate(playerBullet.transform, p_bulletSpawner.transform.position, p_bulletSpawner.transform.rotation);
        yield return new WaitForSeconds(firerate);

    }

    void Aim()
    {
        //playerAnimator.SetBool("isAiming", true);
        weaponSway.swaySensitivity = swaySensitivity / 3;
        //crosshair.SetActive(false);
    } 
}
