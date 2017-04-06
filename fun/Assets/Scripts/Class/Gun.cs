using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : MonoBehaviour {

    public string gunName;
    public Sprite gunImage;

    public int reloadTime;
    public float damagePerHit;
    public int numberOfBulletsInChamber;
    public int currentAmmo;
    public int maxClipBullets;

    protected float coolDownTimer;

    public int maxAmmo;
    public gunType typeOfGun;

    public abstract IEnumerator ReloadGun(); // anoother that maybe implemented in this class
    public abstract void ShootGun(Quaternion currentAim);
    public abstract void CoolDownTimePerShot(); // maybe implemented in this class. im not to sure

    public enum gunType{
        pistol, revolver, machineGun, shotGun,grenadeLauncher
    }
}
