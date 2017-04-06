using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

    public Text[] gunInfo;

    InventorySystem invSystem;

    public GameObject currentGun; // test
    int currentChoice;

    public Gun gunEntity;
    UISystem ui_System;
	
    
    // Use this for initialization
	void Start () {
        invSystem = GetComponentInParent<InventorySystem>();
        currentChoice = 0;
        ui_System = Camera.main.GetComponent<UISystem>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetButtonDown("Cycle") && invSystem.gunList.Count != 0)
            ChooseGun();

        if (gunEntity != null)
            gunEntity.ShootGun(Quaternion.Euler(0,0,0));

        if (Input.GetButtonDown("ThrowObject") && currentGun != null)
        {
            ThrowObject(currentGun);
            gunEntity = null;
            currentGun = null;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.rotation * Vector3.forward * 20f, out hit))
        {
            //ui_System.DrawTargetOnObject(hit.transform.position);
            if (hit.collider.tag == "Enemy")
            {
                Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 20f, Color.red);
                ui_System.DrawTargetOnObject(hit.transform.position);
            }             
        }
        
    }

    void ShootWeapon()
    {
        Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 20f, Color.red);
        if (Input.GetAxisRaw("FireButton") == 1)
        {
            gunEntity.ShootGun(Quaternion.Euler(transform.rotation * Vector3.forward * 20f));
        }
    }

    void ChooseGun()
    {

        if (currentGun != null && gunEntity.currentAmmo > 0)
        {
            Debug.Log("here");
            invSystem.AddGunToList(gunEntity);
            Destroy(currentGun);
        }
        else if (currentGun != null && gunEntity.currentAmmo <= 0) {
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
            gunEntity = currentGun.GetComponent<Pistol>();
        } else if (currentGun.GetComponent<ShotGun>() != null)
        {
            gunEntity = currentGun.GetComponent<ShotGun>();
        }
    }

    void ThrowObject(GameObject objectToThrow)
    {
         objectToThrow.transform.parent = null;
         objectToThrow.AddComponent<Rigidbody>().AddForce(transform.rotation * Vector3.forward * 8f, ForceMode.Impulse);
         objectToThrow.AddComponent<BoxCollider>();
    }
}


