using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour {

    InventorySystem invSystem;
    float pickUpCooldown;
    UISystem ui_System;
    public Sprite[] buttonImages;

    private void Start()
    {
        invSystem = GetComponent<InventorySystem>();
        ui_System = Camera.main.GetComponent<UISystem>();
    }

    private void Update()
    {
        if(pickUpCooldown > 0)
        {
            pickUpCooldown -= Time.deltaTime;
        } else
        {
            pickUpCooldown = 0;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Weapon")
        {
            ui_System.playerPrompts("Press", "to pick up.", buttonImages[0]);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Weapon")
        {
            if (Input.GetButtonDown("Interact") && pickUpCooldown <= 0)
            {
                pickUpCooldown = 1f;
                invSystem.AddGunToList(col.GetComponent<Gun>());
                Destroy(col.gameObject);
            }
        }
    }

    void MatchGunToType(GameObject gun)
    {
        if (gun.GetComponent<Pistol>() != null)
        {
            invSystem.AddGunToList(gun.GetComponent<Pistol>());
        }
    }
}
