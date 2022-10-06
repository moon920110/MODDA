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
    public float bulletMovementSpeed;
    public float fireTimer = 1f;//
    private bool shootReady;
    
    public GameObject playerObject;
    //public float damage = 50f;    
    public GameManager gameManager;
    public PlayerManager playerManager;    
    
    //public LayerMask canBeShot;
    public LayerMask Ground, Player;
    public NavMeshAgent agent;
    public Transform playerLocation;
    //Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 3;
    //Attack
    bool alreadyAttacked;
    //States
    public float sightRange = 10, attackRange = 5;
    public bool playerInSightRange, playerInAttackRange;    
    public float t_current_health = 100; // Turret's updated health
    public int scorePoints = 20;  // When a turret is destroyed, the player gets 20 points   

    //UI
    public Slider slider;
    #endregion


    public void Awake()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        target = playerObject;
    }

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();        
        slider.maxValue = t_current_health;
        slider.value = t_current_health;
        shootReady = true;
    }
    void Update()
    {
        //detecting player        
        //playerObject = GameObject.FindGameObjectWithTag("Player");
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);
        playerObject = GameObject.FindGameObjectWithTag("Player");
        slider.gameObject.transform.LookAt(playerObject.transform.position);

        if (!playerInSightRange && !playerInAttackRange)
        {
            if (t_current_health > 0) 
            {
                Patroling();
            }
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            targetLocked = true;
            if (targetLocked && playerInSightRange)
            {
                float p_current_health = playerObject.GetComponent<PlayerManager>().p_current_health;
                /*if (p_current_health > 0)
                {
                    Debug.Log(p_current_health);
                    turretTop.transform.LookAt(target.transform);
                }*/

            }
            if (t_current_health > 0)
            {
                ChasePlayer();
            }
        }
        if (playerInSightRange && playerInAttackRange)
        {            
            /*if (targetLocked)
            {
                turretTop.transform.LookAt(target.transform);
            }*/
            if (t_current_health > 0)
            {
                AttackPlayer();
            }
        }        
    }
    public void Shoot()
    {       
        //RaycastHit t_hit = new RaycastHit(); //don't mind this warning                
        //shootReady = false;
        StartCoroutine(FireRate());
        //shootReady = false;
    }

    IEnumerator FireRate()
    {
        //Transform _bullet = 
         GameObject.Instantiate(bullet.transform, bulletSpawner.transform.position, bulletSpawner.transform.rotation);// Quaternion.identity);
        //Rigidbody _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
        //_bullet.transform.rotation = bulletSpawner.transform.rotation;
        //_bulletRigidBody.AddForce(_bulletRigidBody.transform.forward * bulletMovementSpeed);
        //RaycastHit t_hit = new RaycastHit();
        yield return new WaitForSeconds(2);//(fireTimer);
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
            transform.LookAt(playerLocation);
        }
        if (!alreadyAttacked)
        {
            //targetLocked = true;
            if (targetLocked)
            {
                turretTop.transform.LookAt(playerObject.transform);
                turretTop.transform.Rotate(4, 0, 0);                
                if (shootReady)
                { Shoot(); }
            }
        }
    }
    public void Hit(float damage) //When the Turret Takes Damage
    {
        t_current_health -= damage;        
        slider.value = t_current_health;
        if (t_current_health <= 0)
        {
            Destroy(this.gameObject, 0.0001f);
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
            agent.SetDestination(playerLocation.position);
        }
    }
   
    public void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
