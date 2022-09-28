using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerManager : MonoBehaviour
{
    #region Variables
    public float p_current_health;
    public GameManager gameManager;
    public PlayerMovement playerMovement;
    public CharacterController characterController;
    public GameObject playerCamera;
    public RecoveryBox recoveryBox;
    private Quaternion playerCameraOriginalRotation;
    private float shakeTime;
    private float shakeDuration;    
    public AudioSource HealthPickup;
    //Weapons
    public GameObject weaponHolder;
    int activeWeaponIndex;
    GameObject activeWeapon;
      
    //UI
    public Text healthNumber;
    public float Score;
    public Text scoreText;
    public GameObject playerGameObject;
    public PlayerSpawnPoints playerSpawnPoints;
    
    //public Transform Destination;
    //public Transform playerLocation;
    #endregion

    void Start()
    {
        playerSpawnPoints = FindObjectOfType<PlayerSpawnPoints>();
        
        playerCameraOriginalRotation = playerCamera.transform.localRotation;
        
        healthNumber.text = p_current_health.ToString();
        weaponSwitch(0);
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        recoveryBox = GameObject.Find("RecoveryBox").GetComponent<RecoveryBox>();
        
        playerGameObject = GameObject.FindGameObjectWithTag("Player");

        p_current_health = 10 ; //Set to 10 for testing
        HealthPickup = GetComponent<AudioSource>();        
    }
    public void Update()
    {
        /*if (shakeTime < shakeDuration)
        {
            shakeTime += Time.deltaTime;
            cameraShake();
        }
        else if (playerCamera.transform.localRotation != playerCameraOriginalRotation)
        {
            playerCamera.transform.localRotation = playerCameraOriginalRotation;
        }*/
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            weaponSwitch(activeWeaponIndex + 1);
        }        
        scoreText.text = Score.ToString();

        if (Input.GetKeyDown(KeyCode.M))//to test out player spawn
        {
            MoveOnDie();
        }
    }
    public void Recovered(float recoveryAmount)
    {
        HealthPickup.Play();
        if (p_current_health <= 80)
        {
            p_current_health = p_current_health + recoveryAmount;
            healthNumber.text = p_current_health.ToString();
        }
        else
        {
            p_current_health = 100;
            healthNumber.text = p_current_health.ToString();
        }
    }
    public void Hit(float damage) //Take Damage
    {
        p_current_health -= damage;
        healthNumber.text = p_current_health.ToString();
        

        if (p_current_health <= 0)
        {
            Score = Score - gameManager.deathPenalty;
            scoreText.text = Score.ToString();            
            //p_current_health = 100;            
            
            PlayerDied();
            
        }
        else //int collision with turrent bullet
        {
            shakeTime = 0;
            shakeDuration = 0.6f;
            if (shakeTime < shakeDuration)
            {
                shakeTime += Time.deltaTime;
                cameraShake();
            }
            else if (playerCamera.transform.localRotation != playerCameraOriginalRotation)
            {
                playerCamera.transform.localRotation = playerCameraOriginalRotation;
            }
        }
    }
    public void PlayerDied()
    {
        shakeTime = 0;
        shakeDuration = 0.2f;
        if (shakeTime < shakeDuration)
        {
            shakeTime += Time.deltaTime;
            cameraShake();
            ///p_current_health = 100;
        }
        else if (playerCamera.transform.localRotation != playerCameraOriginalRotation)
        {
            playerCamera.transform.localRotation = playerCameraOriginalRotation;
        }
        //p_current_health = 100;
        MoveOnDie();
        //StartCoroutine(PlayerSpawn());
        //////p_current_health = 100;
    }

    private void MoveOnDie()
    {
        p_current_health = 100;
        characterController.enabled = false;
        playerMovement.enabled = false;
        int selectedIndex = Random.Range(0, playerSpawnPoints.SpawnPoints.Count);
        transform.position = playerSpawnPoints.SpawnPoints[selectedIndex].position;
        characterController.enabled = true;
        playerMovement.enabled = true;
        
        Debug.Log("moved! 2");
        
    }
    
    
    /*public void PlayerSpawner()
    {        
        playerGameObject.transform.position = PlayerSpawnPointAtStart.transform.position;
        shakeTime = 0;
        shakeDuration = 2f;        
    }*/

    /*IEnumerator PlayerSpawn()
    {
        playerGameObject.GetComponent<PlayerMovement>().enabled = false;
        yield return null;
        //playerGameObject.transform.position = PlayerSpawnPointAtStart.transform.position;
        playerGameObject.GetComponent<PlayerMovement>().enabled = true;
        Debug.Log("moved!");        
        yield return new WaitForSeconds(1);
    }*/
    
    public void cameraShake()
    {
        playerCamera.transform.localRotation = Quaternion.Euler(Random.Range(-2f, 2f), 0, 0);
    }

    public void weaponSwitch(int weaponIndex)
    {
        int index = 0;
        int amountOfWeapons = weaponHolder.transform.childCount;

        if (weaponIndex > amountOfWeapons - 1)
        {
            weaponIndex = 0;
        }
        foreach (Transform child in weaponHolder.transform)
        {
            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
            if (index == weaponIndex)
            {
                child.gameObject.SetActive(true);
                activeWeapon = child.gameObject;
            }
            index++;
        }
        activeWeaponIndex = weaponIndex;
    } 
    }


