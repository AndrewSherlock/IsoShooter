using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun {

    float cooldown;
    public ParticleSystem gunFlash;
    public Light gunLight;


    // Use this for initialization
    void Start () {
        coolDownTimer = .5f;
    }
	
	// Update is called once per frame
	void Update () {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = 0;
        }

        if (numberOfBulletsInChamber <= 0)
        {
            if(currentAmmo > 0 && Input.GetButtonDown("Interact"))
            {
                StartCoroutine(ReloadGun());
            }

            if (currentAmmo == 0)
            {

            }
        }
	}

    public override IEnumerator ReloadGun()
    {
        Debug.Log("Got");
        while (numberOfBulletsInChamber < maxClipBullets && numberOfBulletsInChamber < currentAmmo)
        {
            
            if(Input.GetAxisRaw("FireButton") < 0 && numberOfBulletsInChamber > 0) // not running 100%
            {
                yield break;
            }
            numberOfBulletsInChamber++;
            yield return new WaitForSeconds(reloadTime / maxClipBullets);
        }

        yield return null;
    }

    public override void ShootGun()
    {
        Debug.DrawRay(transform.position, transform.rotation * Vector3.forward * 20f, Color.red);
        if (Input.GetAxisRaw("FireButton") < 0 && cooldown <= 0)
        {
            RaycastHit hit;
            if (numberOfBulletsInChamber > 0)
            {
                numberOfBulletsInChamber--;
                currentAmmo--;
                gunFlash.Play();
                CoolDownTimePerShot();
                if (Physics.Raycast(transform.position, transform.rotation * Vector3.forward * 20f, out hit))
                {  
                    if (hit.collider.tag == "Enemy")
                    {
                        hit.collider.gameObject.SendMessage("TakeDamage", damagePerHit);
                        Debug.Log(hit.collider.name);
                    }
                }

            }
        }
    }

    public override void CoolDownTimePerShot()
    {
        Debug.Log(coolDownTimer);
        cooldown = coolDownTimer;
    }
}
