using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
/*
 * Date created: 10/20/2018
 * Creator: Nate Smith
 * 
 * Description: An experimental PlayerMovement Class.
 * Controls directional input and movement, movement is free, moves according to virtual joystick.
 * Inherits from PlayerMovement to allow for easier testing.
 * This version is for a future project. This project would be a singleplayer Asteroids-esque action game.
 * Used in the 1PlayerAsteroid Scene
 */
public class PlayerMovementJoystick : PlayerMovement
{

    protected override void Awake()
    {
        base.Awake();
    }

    /*
     * Move the player according to the current control scheme.
     * In this case, input is taken from the joystick.
     * This takes place in FixedUpdate, because the joystick input cannot use MoveLeft/MoveRight.
     */
    protected override void FixedUpdate()
    {
        // Create a vector from the Input from the horizontal and vertical input, max speed, and the timestep.
        Vector2 moveVector = new Vector2(
            CrossPlatformInputManager.GetAxis("Horizontal"),
            CrossPlatformInputManager.GetAxis("Vertical")
            ) * maxSpeed * Time.deltaTime;
        // If there is input, move the player accordingly.
        if (moveVector != Vector2.zero && pm.canMove)
        {
            // Reduce the drag to 1 if necessary.
            if (pm.rb.drag != 1f)
                pm.rb.drag = 1f;
            // Change the velocity.
            pm.rb.velocity += moveVector;
            // Rotate the player over time to face the new move vector's direction.
            Vector2 angleVector = Vector2.MoveTowards(pm.rb.velocity, moveVector, 5f * Time.deltaTime);
            float angle = (Mathf.Atan2(angleVector.y, angleVector.x) * Mathf.Rad2Deg) - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        // If there is no input, increase drag to slow the player to a stop.
        else
            pm.rb.drag = 3f;
    }

    /*
     * Basic movement functions, unused.
     */
    public override void MoveRight() { }
    public override void MoveLeft() { }
    public override void MoveForward() { }
}
