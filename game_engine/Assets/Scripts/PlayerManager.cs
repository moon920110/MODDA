using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DDA
{
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
        public float numberofDeath;
        public Text deathText;

        public GameObject playerGameObject;
        public PlayerSpawnPoints playerSpawnPoints;
        public CanvasGroup deathPanel;

        public GameObject weapon;
        public bool weaponTaken;
        

        public bool equipped;
        public static bool slotFull;

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
            recoveryBox = GameObject.Find("RecoveryBoxObject").GetComponent<RecoveryBox>();

            playerGameObject = GameObject.FindGameObjectWithTag("Player");

            p_current_health = 10; //Set to 10 for testing
            HealthPickup = GetComponent<AudioSource>();
            numberofDeath = 0;
            weapon = GameObject.FindGameObjectWithTag("Weapon");
            weaponTaken = false;
            equipped = false;
        }
        public void Update()
        {
            if (shakeTime < shakeDuration)//
            {
                shakeTime += Time.deltaTime;
                cameraShake();
            }
            else if (playerCamera.transform.localRotation != playerCameraOriginalRotation)
            {
                playerCamera.transform.localRotation = playerCameraOriginalRotation;
            }//
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                weaponSwitch(activeWeaponIndex + 1);
            }
            scoreText.text = Score.ToString();

            if (Input.GetKeyDown(KeyCode.M))//to test out player spawn
            {
                MoveOnDie();
            }
            if (deathPanel.alpha > 0)
            {
                deathPanel.alpha -= Time.deltaTime;
            }
            //weapon = GameObject.FindGameObjectWithTag("Weapon");
            if (weaponTaken)
            {
                Object.Destroy(weapon, 0.05f);
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

                PlayerDied();

            }
            else //collision with turrent bullet, shaking the screen
            {
                shakeTime = 0;
                shakeDuration = 0.8f;
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
            shakeDuration = 0.1f;
            if (shakeTime < shakeDuration)
            {
                shakeTime += Time.deltaTime;
                cameraShake();
            }
            else if (playerCamera.transform.localRotation != playerCameraOriginalRotation)
            {
                playerCamera.transform.localRotation = playerCameraOriginalRotation;
            }
            deathPanel.alpha = 1;      //the screen gets dark to give death effect  
            MoveOnDie();
            numberofDeath = numberofDeath + 1;
            deathText.text = numberofDeath.ToString();
            p_current_health = 20;
            healthNumber.text = p_current_health.ToString();


        }

        private void MoveOnDie()
        {
            characterController.enabled = false;
            playerMovement.enabled = false;
            int selectedIndex = Random.Range(0, playerSpawnPoints.SpawnPoints.Count);
            transform.position = playerSpawnPoints.SpawnPoints[selectedIndex].position;
            characterController.enabled = true;
            playerMovement.enabled = true;
            Debug.Log("moved! 2");
        }

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

        public void weaponAdd()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Weapon")
            {
                weapon = other.gameObject;
                weaponTaken = true;
                equipped = true;
                playerGameObject = GameObject.FindGameObjectWithTag("Player");
                //player.GetComponent<PlayerManager>().Recovered(recoveryAmount);
                //weapon.GetComponent<WeaponManager>().Recovered(recoveryAmount);
            }
        }
    }


}
