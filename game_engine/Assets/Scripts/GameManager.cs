using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

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
        
    public GameObject playerGameObject;
    public float p_current_health=10;
    public float deathPenalty = 50;
    public float currentScore =0 ;
    public Text currentScoreText;     
    
    public GameObject Timer;
    public float currentTime;
    public int startMinutes;
    public Text currentTimeText;
    public bool timerActive = false;

    //Access to other Scripts
    public PlayerManager playerManager;
    public RecoveryBox recoveryBox;
    public TurretManager turretManager;
    public TurretSpawner turretSpawner;
    public WeaponManager weaponManager;

    //UI some of them are not assigned yet
    public GameObject endScreen;
    public float endScore;
    public Text endScoreText;
    public int enemiesKilled;
    public Text enemiesKilledNumber;
    public GameObject pauseMenu;
    #endregion
    void Start()
    {        
        recoveryBox = GameObject.Find("RecoveryBox").GetComponent<RecoveryBox>();        
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawners");        
        recoveryPoints = GameObject.FindGameObjectsWithTag("RecoverySpawners");
        playerObject = (GameObject)(Resources.Load("Player"));
        turretPrefab= (GameObject)(Resources.Load("TurretAI"));        
        playerGameObject = GameObject.FindGameObjectWithTag("Player"); 
        PlayerSpawnPointAtStart = GameObject.FindGameObjectWithTag("PlayerSpawnerAtStart");
        currentTime = startMinutes * 60;
        StartTimer();
    }

    void Update()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        
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
    }        
        public void EndGame()
    {
        //timeManager.stopCountdown();
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
