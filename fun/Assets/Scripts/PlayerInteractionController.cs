using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour {

    InventorySystem invSystem;
    float pickUpCooldown;

    private void Start()
    {
        invSystem = GetComponent<InventorySystem>();
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
            Debug.Log("Press Y to pick up");
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Weapon")
        {
            if (Input.GetButtonDown("Interact") && pickUpCooldown <= 0)
            {
                // MatchGunToType(col.gameObject);
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
