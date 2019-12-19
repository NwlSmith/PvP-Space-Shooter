using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/7/2018
 * Creator: Nate Smith
 * 
 * Description: An experimental PlayerMovement Class.
 * Controls directional input and movement, left and right rotate, forward moves forward.
 * Inherits from PlayerMovement to allow for easier testing.
 * This version is for a future project. This project would be a singleplayer Asteroids-esque action game.
 * Used in the 1PlayerAsteroidTank Scene
 */
public class PlayerMovementTank : PlayerMovement {

    // Public objects.
    public float thrust = 7f;
    public float turnThrust = 50f;

    // Private objects.
    private PlayerTiltTank playerTiltTank;

    protected override void Awake()
    {
        base.Awake();
        playerTiltTank = GetComponentInChildren<PlayerTiltTank>();
    }

    /*
     * Take input and call the corresponding movement function.
     * Used for testing movement schemes with a keyboard.
     * Invoked in FixedUpdate function to use keyboard movement.
     */
    protected override void HandleInput()
    {
        if (Input.GetButtonDown(pm.right))
            MoveRight();
        if (Input.GetButtonDown(pm.left))
            MoveLeft();
        if (Input.GetButtonDown("Forward1"))
            MoveForward();
    }

    /*
     * Move the player right according to the current control scheme.
     * In this case, pressing the right button on the UI makes the player turn right.
     */
    public override void MoveRight()
    {
        // Ensure the player can move.
        if (!CanMove())
            return;
        // Give the movement sound a random high pitch.
        audioSource.pitch = Random.Range(.55f, .58f);
        // If the player is changing directions, apply double turning thrust right.
        if (pm.rb.angularVelocity < 0f)
            Turn(-turnThrust);
        // Apply turning thrust right.
        Turn(-turnThrust);
    }

    /*
     * Move the player left according to the current control scheme.
     * In this case, pressing the left button on the UI makes the player turn left.
     */
    public override void MoveLeft()
    {
        // Ensure the player can move.
        if (!CanMove())
            return;
        // Give the movement sound a random low pitch.
        audioSource.pitch = Random.Range(.53f, .55f);
        // If the player is changing directions, apply double turning thrust left.
        if (pm.rb.angularVelocity > 0f)
            Turn(turnThrust);
        // Apply turning thrust left.
        Turn(turnThrust);
        
    }

    /*
     * Move the player forward according to the current control scheme.
     * In this case, pressing the up button on the UI makes the player move forward.
     */
    public override void MoveForward()
    {
        // Ensure the player can move.
        if (!CanMove())
            return;
        // Give the movement sound a random very low pitch.
        audioSource.pitch = Random.Range(.50f, .53f);
        // Apply thrust forward.
        Push(thrust);
    }

    /*
     * Rotate the player according to thrust value.
     * thrust: the direction and speed that the rotation will utilize.
     */
    private void Turn(float thrust)
    {
        audioSource.Play();
        pm.rb.angularVelocity +=  thrust;
    }

    /*
     * Move the player forward according to thrust value.
     * thrust: the speed that will be added to the player.
     */
    private void Push(float thrust)
    {
        audioSource.Play();
        pm.rb.velocity += (Vector2)playerTiltTank.transform.up * thrust;
    }
}
