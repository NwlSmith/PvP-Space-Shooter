using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: Controls the rotation of the players ship sprites.
 * Used on the SpriteHolder of ships with PlayerMovementHorizontal.
 */
public class PlayerTilt : MonoBehaviour {

    protected PlayerManager pm;

    protected void Awake()
    {
        pm = GetComponentInParent<PlayerManager>();
    }

    protected void FixedUpdate()
    {
        if (pm.canMove)
            CalculateAngle();
    }

    /*
     * Calculates the angle of the player sprite, based on their velocity and their player number.
     * If the player is not moving, they should point straight up or down.
     * Rotate in the direction of their movement.
     * Player 2 is flipped 180 degrees.
     */
    protected virtual void CalculateAngle()
    {
        float z = 0;
        if (pm.playerNum == 1)
            z = pm.rb.velocity.x;
        else if (pm.playerNum == 2)
            z = 180f - pm.rb.velocity.x;
        else
            Debug.Log("Invalid PlayerNum: " + pm.playerNum);

        Quaternion angle = Quaternion.Euler(new Vector3(0f, 0f, -z));

        transform.rotation = angle;
    }
}
