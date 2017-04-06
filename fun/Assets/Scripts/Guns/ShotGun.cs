using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun {

    public ParticleSystem gunFlash;
    public float cooldown;
    bool hasReloaded = true;
    Vector3 barrelEnd;

	// Use this for initialization
	void Start () {
        barrelEnd = GetBarrelEnd();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }
        else
        {
            coolDownTimer = 0;
        }

        if(numberOfBulletsInChamber <= 0 && currentAmmo > 0)
        {
            hasReloaded = false;
            StartCoroutine(ReloadGun());
        }
    }

    Vector3 GetBarrelEnd()
    {
        Bounds colBounds = GetComponent<Collider>().bounds;
        return new Vector3(colBounds.center.x, colBounds.max.y, colBounds.max.z);
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
        Vector3[] aimPoints = new Vector3[6];

        if (numberOfBulletsInChamber > 0 && coolDownTimer <= 0 && hasReloaded)
        {
            numberOfBulletsInChamber--;
            currentAmmo--;
            gunFlash.Play();
            CoolDownTimePerShot();
            for (int i = 0; i < aimPoints.Length; i++)
            {
                aimPoints[i] = Vector3.forward + (Random.insideUnitSphere / 2);
                Debug.DrawRay(barrelEnd, aimPoints[i] * 20f, Color.red);

                if (Physics.Raycast(barrelEnd, currentAim * aimPoints[i] * 20f, out hit))
                {

                    if (hit.collider.tag == "Enemy" || hit.collider.tag == "Player")
                    {
                        hit.collider.gameObject.SendMessage("TakeDamage", damagePerHit);
                    }
                }
            }
        }
    }


    public override void CoolDownTimePerShot()
    {
        coolDownTimer = cooldown; 
    }
}
