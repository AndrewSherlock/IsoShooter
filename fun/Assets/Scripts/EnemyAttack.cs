using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public GameObject gunObject;
    Gun gun;
   
    bool playerInFront;

    float throwTimer = 0;
   
    Quaternion[] targetAim = new Quaternion[5];
    Enemy enemyScript;
    // Use this for initialization
    void Start () {
        enemyScript = transform.parent.GetComponent<Enemy>();
	}

	// Update is called once per frame
	void Update () {

        playerInFront = CheckForPlayerInFront();
        Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 20f, Color.red);

        if (gun != null)
        {
           // gunObject.transform.rotation = transform.rotation;

            if (gun.currentAmmo > 0 && gun.numberOfBulletsInChamber > 0)
            {
                AttackPlayer();
                int choice = Random.Range(0, targetAim.Length);
                Debug.DrawRay(transform.position, targetAim[choice] * Vector3.forward * 20f, Color.magenta);
                if(playerInFront)
                    gun.ShootGun(targetAim[choice]);
                
            }
            else
            {
                if (gunObject != null && throwTimer <= 0)
                {
                    throwTimer = 2f;
                    gun = null;
                    ThrowObject(gunObject);
                    gunObject = null;
                }
            }
        }
       
        if (throwTimer > 0)
        {
            throwTimer -= Time.deltaTime;
        }
        else
        {
            throwTimer = 0;
        }
	}
   
    void AttackPlayer()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");

        if (target == null)
            return;

        for (int i = 0; i < targetAim.Length; i++)
        {
            int wayToAim = (int)(Mathf.Sign(Random.Range(-.5f, .5f)));
            Vector3 temp  = (new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z + ((i/2) * wayToAim)));
            targetAim[i] = Quaternion.LookRotation(temp - transform.position);
        } 
       
        transform.parent.transform.rotation = Quaternion.Slerp(transform.rotation, targetAim[0], .1f);
    }

    void EquipGun()
    {
        GetGunType();
        gunObject.transform.position = transform.position;
        gunObject.transform.rotation = transform.rotation;
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

    bool CheckForPlayerInFront()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.rotation * Vector3.forward * 20f, out hit))
        {
            if (hit.collider.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            gunObject = other.gameObject;
            EquipGun();
        }
    }

    void ThrowObject(GameObject objectToThrow) // still broken
    {
        if (objectToThrow.GetComponent<Rigidbody>() == null)
        {
            objectToThrow.AddComponent<Rigidbody>().AddForce(Vector3.forward * 1f, ForceMode.Acceleration);
        }
        objectToThrow.AddComponent<BoxCollider>();
        objectToThrow.transform.parent = null;
    }

    
}
