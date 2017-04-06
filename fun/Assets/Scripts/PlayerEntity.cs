using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Player {

    public float currentHealth;
    public float maxHealth;
    public float armorHealth;
    public float maxArmorHealth;

    GameManager gmanager;
    
	// Use this for initialization
	void OnEnable () {
        gmanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentHealth = maxHealth;
        armorHealth = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TakeDamage(float damageAmount)
    {
        if (armorHealth > 0)
        {
            armorHealth -= damageAmount;
            if (armorHealth < 0)
            {
                damageAmount = Mathf.Abs(armorHealth);
                armorHealth = 0;
            }
            else {
                return;
            }
        }
        currentHealth -= damageAmount;
        
        if(currentHealth < 0)
        {
            gmanager.KillPlayer(gameObject);
        }
    }
}
