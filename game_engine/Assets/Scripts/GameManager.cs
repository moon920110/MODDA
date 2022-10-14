using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace DDA
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public int numberOfTurrets;
        public GameObject[] spawnPoints;
        public GameObject turretPrefab;
        //public Animator blackScreenAnimator;
        public GameObject PlayerSpawnPointAtStart;
        public Transform PlayerSpawnPoint;
        public RecoverySpawner recoverySpawner;
        //Spawn variables
        public GameObject[] recoveryPoints;
        public Transform Player_prefab;
        public GameObject[] determinedLocation;
        public GameObject playerObject;

        public string PlayerSpawnPointTag = "PlayerSpawners";
        public string PlayerAtStartTag = "PlayerSpawnerAtStart";

        public float p_current_health = 10;
        public float deathPenalty = 50;
        public float currentScore = 0;
        public Text currentScoreText;

        public GameObject Timer;
        public float currentTime;
        public int startMinutes;
        public Text currentTimeText;
        public bool timerActive; //= false;

        //Access to other Scripts
        public PlayerManager playerManager;
        public RecoveryBox recoveryBox;
        public TurretManager turretManager;
        public TurretSpawner turretSpawner;
        public WeaponManager weaponManager;
        //public GameObject TurretAI;

        //UI some of them are not assigned yet
        public GameObject endScreen;
        public float endScore;
        public Text endScoreText;
        public Text destroyedText;
        //public int enemiesKilled;
        //public Text enemiesKilledNumber;
        public GameObject pauseMenu;


        #endregion

        private void Awake()
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
        }
        void Start()
        {
            recoveryBox = GameObject.Find("RecoveryBoxObject").GetComponent<RecoveryBox>();
            spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawners");
            recoveryPoints = GameObject.FindGameObjectsWithTag("RecoverySpawners");
            //playerObject = (GameObject)(Resources.Load("Player"));
            turretPrefab = (GameObject)(Resources.Load("TurretAI"));
            PlayerSpawnPointAtStart = GameObject.FindGameObjectWithTag("PlayerSpawnerAtStart");
            currentTime = startMinutes * 60;
            StartTimer();

            turretManager = GameObject.FindWithTag("Turret").GetComponent<TurretManager>();



        }

        void Update()
        {
            //GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");

            #region Countdown Timer
            if (timerActive == true)
            {
                currentTime = currentTime - Time.deltaTime;
                if (currentTime < 0)
                {
                    timerActive = false;
                    //Start();
                }
            }
            TimeSpan time = TimeSpan.FromSeconds(currentTime);

            currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();

            if (timerActive == false)
            {
                Debug.Log("game over");
                EndGame();
            }
            #endregion

            //variable access control, temporary part

            //Debug.Log("Turret health");
            //Debug.Log(turretManager.t_current_health);
            //Debug.Log(deathPenalty);
            if (Input.GetKeyDown(KeyCode.L))
            {
                GameObject playerObject = GameObject.Find("Player");
                PlayerManager playerManager = playerObject.GetComponent<PlayerManager>();
                Debug.Log("Player health: " + playerManager.p_current_health);
                Debug.Log("Number of deaths: " + playerManager.numberofDeath);

            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                //Access to TurretManager script
                GameObject TurretAI = GameObject.FindGameObjectWithTag("Turret");
                TurretManager turretManager = TurretAI.GetComponent<TurretManager>();

                //Access to TurretSpawner script
                GameObject EnemySpawner = GameObject.Find("EnemySpawner");
                TurretSpawner turretSpawner = EnemySpawner.GetComponent<TurretSpawner>();
                //Access to RecoveryBox script
                GameObject RecoveryBox = GameObject.Find("RecoveryBoxObject");
                RecoveryBox recoveryBox = RecoveryBox.GetComponent<RecoveryBox>();
                //Access to RecoverySpawner script
                GameObject RecoverySpawner = GameObject.Find("RecoverySpawnPoints");
                RecoverySpawner recoverySpawner = RecoverySpawner.GetComponent<RecoverySpawner>();
            }
            //Access to WeaponManager script
            GameObject WeaponHolder = GameObject.FindGameObjectWithTag("Weapon"); //pistol shotgun rifle
            WeaponManager weaponManager = WeaponHolder.GetComponent<WeaponManager>();
            destroyedText.text = weaponManager.numberofDestroyed.ToString();

        }
        public void EndGame()
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            endScreen.SetActive(true);
            endScore = playerManager.Score;
            endScoreText.text = endScore.ToString();
            Application.Quit();
        }

        public void StartTimer()
        {
            timerActive = true;
        }
        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
        public void Pause()
        {
            Cursor.lockState = CursorLockMode.None;
            AudioListener.volume = 0;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        public void Continue()
        {
            Cursor.lockState = CursorLockMode.Locked;
            AudioListener.volume = 1;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        public void BackToMainMenu()
        {
            Time.timeScale = 1;
            //blackScreenAnimator.SetTrigger("FadeIn");
            Invoke("LoadMainMenuScene", 0.4f);
        }
        public void LoadMainMenuScene()
        {
            AudioListener.volume = 1;
            SceneManager.LoadScene(0);
        }
    }
}