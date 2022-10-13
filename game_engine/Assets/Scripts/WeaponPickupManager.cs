using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupManager : MonoBehaviour
{
    public WeaponManager weaponManager;
    public Rigidbody rb;
    public BoxCollider coll;
    public GameObject player, weaponHolder, fpsCam;

    public bool equipped;
    public static bool slotFull;

    public float pickupRange;
    public float dropForwardForce, dropUpwardForce;

    private void Start()
    {
        GameObject fpsCam = GameObject.FindGameObjectWithTag("MainCamera");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
        if (!equipped)
        {
            //rb.isKinematic = false;
            //coll.isTrigger = false;
            weaponManager.enabled = false;
        }
        if (equipped)
        {
            //rb.isKinematic = true;
            //coll.isTrigger = true;
            weaponManager.enabled = true;
            slotFull = true;
        }

    }

    private void Update()
    {
        GameObject fpsCam = GameObject.FindGameObjectWithTag("MainCamera");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject weaponHolder = GameObject.FindGameObjectWithTag("WeaponHolder");
        Vector3 distanceToPlayer = player.transform.position - transform.position;
        //if (!equipped && distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && !slotFull) Pickup();

        //if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();

        
    }
    public void Pickup()
    {
        equipped = true;
        slotFull = true;

        //rb.isKinematic = true;
        //coll.isTrigger = true;
        weaponManager.enabled = true;
        transform.SetParent(weaponHolder.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            Pickup();
        }
    }
    // Update is called once per frame
    public void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);
        rb.isKinematic = false;
        coll.isTrigger = false;
        
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(fpsCam.transform.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.transform.up * dropUpwardForce, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
        weaponManager.enabled = false;


    }
}
