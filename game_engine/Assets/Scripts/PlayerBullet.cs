using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    #region Variables
    public GameObject playerBullet;
    public float p_bulletSpeed = 20;
    public GameObject playersTarget;
    public GameObject p_bulletPrefab;
    public Transform p_bulletSpawn;
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
       /* p_bulletPrefab = (GameObject)Resources.Load("playerBullet");
        GameObject bullet = Instantiate(p_bulletPrefab);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), p_bulletSpawn.parent.GetComponent<Collider>());
       
        bullet.transform.position = p_bulletSpawn.position;

        Vector3 rotation = bullet.transform.rotation.eulerAngles;

        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

        bullet.GetComponent<Rigidbody>().AddForce(p_bulletSpawn.forward * p_bulletSpeed, ForceMode.Impulse);

        */
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
        Destroy(playerBullet, 0.0001f);
        /*if (other.tag == "Turret")
        {
            playersTarget = other.gameObject;
            Destroy(playerBullet, 0.000001f);
           
        }
        else if (other.tag == "Wall")
        {
            Destroy(playerBullet, 0.0001f);
        }*/


    }
}
