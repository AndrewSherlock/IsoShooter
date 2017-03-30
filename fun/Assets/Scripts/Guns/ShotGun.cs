using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override IEnumerator ReloadGun()
    {
        return null;
    }

    public override void ShootGun()
    {

    }

    public override void CoolDownTimePerShot()
    {
    }
}
