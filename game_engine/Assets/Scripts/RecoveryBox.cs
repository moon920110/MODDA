using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryBox : MonoBehaviour
{

    #region Variables
    public PlayerManager playerManager;
    public GameManager gameManager; 

    public float recoveryAmount = 30f;    
    public GameObject recovery;
    public bool recoveryTaken;
    public GameObject player;
    #endregion
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();        
    }
    void Update()
    {
        if (recoveryTaken)
        {
            Object.Destroy(gameObject, 0.05f);            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            recovery = other.gameObject;
            recoveryTaken = true;
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerManager>().Recovered(recoveryAmount);
        }
    }
}
