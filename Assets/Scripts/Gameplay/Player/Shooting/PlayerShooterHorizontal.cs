using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooterHorizontal : PlayerShooter {

    public bool useAlternateFireMode = false;

    protected override void Update()
    {
        if (Input.GetButtonDown(pm.fire))
            Fire();
    }

    protected override void InstantiateBullet()
    {
        if (!useAlternateFireMode)
            Instantiate(bullet, transform.position, pm.transform.localRotation);
        else
            base.InstantiateBullet();
    }
}
