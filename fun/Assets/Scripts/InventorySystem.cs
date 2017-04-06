using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour {

    public List<Gun> gunList = new List<Gun>();
    public GameObject[] gunTypes;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Gun g in gunList)
            {
                GetGunDetails(g);
            }
        }
    }

    public void AddGunToList(Gun pickedUpGun)
    {
        Debug.Log("Picked gun");
        gunList.Add(pickedUpGun);
    }

    void GetGunDetails(Gun gun)
    {
        // im not sure what this method needs, i might need to make setters and getters in the gun class.
        Debug.Log(gun.gunName);
        Debug.Log(gun.maxAmmo);
        Debug.Log(gun.numberOfBulletsInChamber);
        Debug.Log(gun.typeOfGun);
    }

    public GameObject TakeWeaponFromInventory(int gunChoice)
    {
        Gun gun = gunList[gunChoice];
        GameObject go = Instantiate(GetMesh(gun.typeOfGun), transform.position,Quaternion.identity);
        go.SetActive(false);

        go.GetComponent<Gun>().currentAmmo = gun.currentAmmo;
        go.GetComponent<Gun>().maxClipBullets = gun.maxClipBullets;
        go.GetComponent<Gun>().reloadTime = gun.reloadTime;

        gunList.RemoveAt(gunChoice);

        return go;
    }

    public GameObject GetMesh(Gun.gunType type)
    {
        switch (type)
        {
            case Gun.gunType.pistol:
                return gunTypes[0];
            case Gun.gunType.shotGun:
                return gunTypes[1];
            default:
                return null;
        }
    }
}
