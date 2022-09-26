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
    public float damage;
    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject;
            Destroy(bullet, 0.0001f);
            player.GetComponent<PlayerManager>().Hit(damage);
            
        }
        else if (other.tag == "Wall")
        {
            Destroy(bullet,0.0001f);
        }
    }
}
