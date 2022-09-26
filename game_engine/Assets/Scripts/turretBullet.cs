using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turretBullet : MonoBehaviour
{
    #region Variables
    public float movementSpeed;
    private GameObject target;
    public GameObject bullet;
    public GameObject player;    
    public float damage = 5f;
    #endregion
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject;            
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerManager>().Hit(damage);
        }
        else if (other.tag == "Wall")
        {
            Destroy(bullet, 0.00001f);
        }
    }
}
