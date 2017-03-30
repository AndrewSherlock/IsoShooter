using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public GameObject gunObject;
    Gun gun;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(gun.currentAmmo > 0 && gun.numberOfBulletsInChamber > 0)
        {
            AttackPlayer();
        }
        Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 20f, Color.magenta);
	}

    void AttackPlayer()
    {
        float randomizedHit = Random.Range(-.3f, .3f);

        GameObject target = GameObject.FindGameObjectWithTag("Player");
        Quaternion aimDir = Quaternion.LookRotation(target.transform.position - transform.position);
        aimDir.y = aimDir.y + randomizedHit;
        transform.parent.transform.rotation = Quaternion.Slerp(transform.rotation, aimDir, .1f);



    }

    void EquipGun()
    {
        GetGunType();
        gunObject.transform.position = transform.position;
        gunObject.transform.parent = transform.parent;
    }

    void GetGunType()
    {
        if(gunObject.GetComponent<Pistol>() != null)
        {
            gun = gunObject.GetComponent<Pistol>();
        }

        if (gunObject.GetComponent<ShotGun>() != null)
        {
            gun = gunObject.GetComponent<ShotGun>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            gunObject = other.gameObject;
            EquipGun();
            //Destroy(other.gameObject);
        }
    }
}
