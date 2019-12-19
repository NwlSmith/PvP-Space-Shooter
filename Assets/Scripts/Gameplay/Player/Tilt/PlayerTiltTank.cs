using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/20/2018
 * Creator: Nate Smith
 * 
 * Description: Controls the rotation of the players ship sprites.
 * Used on the SpriteHolder of ships with PlayerMovementTank.
 */
public class PlayerTiltTank : PlayerTilt {

    /*
     * Calculates the angle of the player sprite, based on their velocity and their player number.
     * If the player is not moving, they should point in the same direction as their movement.
     * Player 2 is flipped 180 degrees.
     */
    protected override void CalculateAngle()
    {
        float z = 0;
        if (pm.playerNum == 1)
            z = -pm.rb.angularVelocity / 6;
        else if (pm.playerNum == 2)
            z = (-pm.rb.angularVelocity / 6) + 180f;
        else
            Debug.Log("Invalid PlayerNum: " + pm.playerNum);

        Quaternion angle = Quaternion.Euler(new Vector3(0f, 0f, -z));
        

        transform.localRotation = angle;
    }
}
