using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{

    public ParticleSystem gunFlash;
    public float cooldown;
    bool hasReloaded = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }
        else
        {
            coolDownTimer = 0;
        }

        if (numberOfBulletsInChamber <= 0 && currentAmmo > 0)
        {
            hasReloaded = false;
            StartCoroutine(ReloadGun());
        }
    }

    public override IEnumerator ReloadGun()
    {
        while (numberOfBulletsInChamber < maxClipBullets && numberOfBulletsInChamber < currentAmmo)
        {
            numberOfBulletsInChamber++;
            yield return new WaitForSeconds(reloadTime / maxClipBullets);
        }
        hasReloaded = true;
    }

    public override void ShootGun(Quaternion currentAim)
    {
        RaycastHit hit;

        if (numberOfBulletsInChamber > 0 && coolDownTimer <= 0 && hasReloaded)
        {
            numberOfBulletsInChamber--;
            currentAmmo--;
            gunFlash.Play();
            CoolDownTimePerShot();
            
            if (Physics.Raycast(transform.position, currentAim * Vector3.forward * 20f, out hit))
            {

                if (hit.collider.tag == "Enemy" || hit.collider.tag == "Player")
                {
                    hit.collider.gameObject.SendMessage("TakeDamage", damagePerHit);
                    Debug.Log(hit.collider.name);
                }
            }

        }
    }

    public override void CoolDownTimePerShot()
    {
      
        coolDownTimer = cooldown;
    }
}
