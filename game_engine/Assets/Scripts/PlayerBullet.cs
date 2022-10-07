using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    #region Variables
    public GameObject playerBullet;
    public float p_bulletSpeed = 20;
    public GameObject playersTarget;
    public GameObject Enemy;
    
    //public float turretFirerate = 100;
    //float turretFirerateTimer = 0;
    //public bool isTurretAutomatic;
    #endregion

    private void Start()
    {
        Enemy = GameObject.FindGameObjectWithTag("Turret");
    }
    void Update()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime * p_bulletSpeed);

        /*if (turretFirerateTimer > 0)
        {
            turretFirerateTimer = turretFirerateTimer - Time.deltaTime;
        }
        if (turretFirerateTimer <= 0) // && isTurretAutomatic)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
            turretFirerateTimer = 1 / turretFirerate;
        }
        if (turretFirerateTimer <= 0)// && !isTurretAutomatic)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
            turretFirerateTimer = 1 / turretFirerate;
            
        }*/
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Turret")
        {
            playersTarget = other.gameObject;
            Destroy(playerBullet, 0.000001f);
           
        }
        else if (other.tag == "Wall")
        {
            Destroy(playerBullet, 0.0001f);
        }


    }
}
