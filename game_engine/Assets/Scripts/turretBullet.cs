using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEngine.UI;

public class turretBullet : MonoBehaviour
{
    #region Variables
    public float movementSpeed;
    private GameObject target;
    public GameObject bullet;
    public GameObject playerObject;    
    public float damage;
    //public float turretFirerate = 100;
    //float turretFirerateTimer = 0;
    //public bool isTurretAutomatic;
    #endregion

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);

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
        if (other.tag == "Player")
        {
            target = other.gameObject;
            Destroy(bullet, 0.0001f);
            playerObject.GetComponent<PlayerManager>().Hit(damage);            
        }
        else if (other.tag == "Wall")
        {
            Destroy(bullet,0.0001f);
        }
    }
}
