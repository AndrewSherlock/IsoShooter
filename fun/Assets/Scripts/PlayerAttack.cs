using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

    public Text[] gunInfo;

    InventorySystem invSystem;

    public GameObject currentGun; // test
    int currentChoice;

    public Gun g; 
	
    
    // Use this for initialization
	void Start () {
        invSystem = GetComponentInParent<InventorySystem>();
        currentChoice = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetButtonDown("Cycle") && invSystem.gunList.Count != 0)
            ChooseGun();

        if (g != null)
            g.ShootGun();

        if (g != null)
        {
            gunInfo[0].text = "Name : " + g.gunName;
            gunInfo[1].text = "Ammo : " + g.currentAmmo + "/" + g.maxAmmo;
            gunInfo[2].text = "Left in Magazine : " + g.numberOfBulletsInChamber;
        }

        if (Input.GetButtonDown("ThrowObject") && currentGun != null)
        {
            ThrowObject(currentGun);
            g = null;
            currentGun = null;
        }


    }

    void ShootWeapon()
    {
        Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 20f, Color.red);
        if (Input.GetAxisRaw("FireButton") == 1)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.rotation * Vector3.forward * 20f, out hit))
            { 
                if (hit.collider)
                {
                    Debug.Log(hit.collider.name);
                }
            }
        }
    }

    void ChooseGun()
    {

        if (currentGun != null && g.currentAmmo > 0)
        {
            Debug.Log("here");
            invSystem.AddGunToList(g);
            Destroy(currentGun);
        }
        else if (currentGun != null && g.currentAmmo <= 0) {
            currentGun.transform.parent = null;
            currentGun.AddComponent<Rigidbody>().AddForce(-Vector3.up * 2f, ForceMode.Impulse);
            currentGun.AddComponent<BoxCollider>();
        }

        if (currentChoice < invSystem.gunList.Count - 1 )
        {
            currentChoice++;
        } else {
            currentChoice = 0;
        }
        currentGun = invSystem.TakeWeaponFromInventory(currentChoice);
        currentGun.transform.parent = transform.parent;
        currentGun.transform.position = transform.position; 
        currentGun.transform.rotation = transform.rotation;
        currentGun.SetActive(true);

        GetGunReference(currentGun);
    }

    void GetGunReference(GameObject gun)
    {
        if (currentGun.GetComponent<Pistol>() != null)
        {
            g = currentGun.GetComponent<Pistol>();
        } else if (currentGun.GetComponent<ShotGun>() != null)
        {
            g = currentGun.GetComponent<ShotGun>();
        }
    }

    void ThrowObject(GameObject objectToThrow)
    {
         objectToThrow.transform.parent = null;
         objectToThrow.AddComponent<Rigidbody>().AddForce(transform.rotation * Vector3.forward * 8f, ForceMode.Impulse);
         objectToThrow.AddComponent<BoxCollider>();
    }
}


