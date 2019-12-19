using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: The PlayerShooterHorizontal class.
 * Controls shooting.
 * Shoots either directly upward (according to parent object) or in the direction the ship is pointing..
 * AlternateFireMode is used in the main game.
 */
public class PlayerShooterHorizontal : PlayerShooter {

    public bool useAlternateFireMode = false;

    /*
     * Detect whether the player will fire based on keyboard input.
     * Used only in testing.
     */
    protected override void Update()
    {
        if (Input.GetButtonDown(pm.fire))
            Fire();
    }

    /*
     * Instantiate a bullet object.
     * Bullets will fire directly upward, unless using the alternate firing mode.
     * The alternate firing mode makes the bullets shoot in the direction of the gameobject.
     * Called in Fire.
     */
    protected override void InstantiateBullet()
    {
        if (!useAlternateFireMode)
            Instantiate(bullet, transform.position, pm.transform.localRotation);
        else
            base.InstantiateBullet();
    }
}
