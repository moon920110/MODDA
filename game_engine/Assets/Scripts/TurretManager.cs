using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TurretManager : MonoBehaviour
{
    #region Variables
    private GameObject target;
    private bool targetLocked;
    public GameObject bulletSpawner;
    public GameObject turretTop;
    public GameObject bullet;
    public float fireTimer = 0.5f;//
    private bool shootReady;
    public float turret_range = 8;
    public GameObject player;
    public float damage = 20f;    
    public GameManager gameManager;
    public PlayerManager playerManager;    
    
    //public LayerMask canBeShot;
    public LayerMask Ground, Player;
    public NavMeshAgent agent;
    public Transform playerlocation;
    //Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    //Attack
    bool alreadyAttacked;
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;    
    public float t_current_health = 100; // Turret's updated health
    public int scorePoints = 20;  // When a turret is destroyed, the player gets 20 points   

    //UI
    public Slider slider;
    #endregion


    public void Awake()
    {
        playerlocation = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        slider.maxValue = t_current_health;
        slider.value = t_current_health;
        shootReady = true;
    }
    void Update()
    {
        //detecting player        
        player = GameObject.FindGameObjectWithTag("Player");
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!playerInSightRange && !playerInAttackRange)
        {
            if (t_current_health > 0) 
            {
                Patroling();
            }
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            if (targetLocked)
            {
                turretTop.transform.LookAt(target.transform);
            }
            if (t_current_health > 0)
            {
                ChasePlayer();
            }
        }
        if (playerInSightRange && playerInAttackRange)
        {
            if (targetLocked)
            {
                turretTop.transform.LookAt(target.transform);
            }
            if (t_current_health > 0)
            {
                AttackPlayer();
            }
        }        
    }
    public void Shoot()
    {
        Transform _bullet = Instantiate(bullet.transform, bulletSpawner.transform.position, Quaternion.identity);
        _bullet.transform.rotation = bulletSpawner.transform.rotation;
        RaycastHit t_hit = new RaycastHit(); //don't mind this warning                
        shootReady = false;
        StartCoroutine(FireRate());
    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(fireTimer);
        shootReady = true;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject;
            targetLocked = true;
        }
        else
        {
            target = other.gameObject;
            targetLocked = false;
        }
    }

    private void AttackPlayer()
    {
        
        if (t_current_health > 0)
        {
            agent.SetDestination(transform.position);
            transform.LookAt(playerlocation);
        }
        if (!alreadyAttacked)
        {
            if (targetLocked)
            {
                turretTop.transform.LookAt(target.transform);
                turretTop.transform.Rotate(4, 0, 0);
                
                if (shootReady)
                { Shoot(); }
            }
        }
    }
    public void Hit(float damage)
    {
        t_current_health -= damage;        
        slider.value = t_current_health;
        if (t_current_health <= 0)
        {
            Destroy(gameObject, 0.0001f);
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(GetComponent<TurretManager>());
            Destroy(GetComponent<CapsuleCollider>());
        }        
    }

    public void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    public void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
        {
            walkPointSet = true;//
        }
    }


    public void ChasePlayer()
    {
        if (playerInSightRange) //&& playerInAttackRange
        {
            agent.SetDestination(playerlocation.position);
        }
    }
   
    public void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
