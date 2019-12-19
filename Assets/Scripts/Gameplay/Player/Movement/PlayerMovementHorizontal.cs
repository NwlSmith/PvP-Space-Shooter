using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: The original PlayerMovement class.
 * Controls directional input and movement, movement is only right and left, buttons are tapped.
 * Inherits from PlayerMovement to allow for easier testing.
 * This is the class that is used for the current version of this game.
 */
public class PlayerMovementHorizontal : PlayerMovement {

    // Public objects
    public float thrust = 7f;

    protected override void FixedUpdate () {
        base.FixedUpdate(); // handle input as usual.
        // Clamp the velocity to ensure the player is not moving faster than desired.
        pm.rb.velocity = new Vector2(
            Mathf.Clamp(
                pm.rb.velocity.x,
                -maxSpeed,
                maxSpeed),
            0f); 
    }

    /*
     * Move the player right according to the current control scheme.
     * In this case, pressing the right button on the UI adds velocity right-ward to the player.
     */
    public override void MoveRight()
    {
        // Ensure the player can move.
        if (!CanMove())
            return;
        // Give the movement sound a random high pitch.
        audioSource.pitch = Random.Range(.55f, .58f);
        // If the player is changing directions, apply double thrust right.
        if (pm.rb.velocity.x < 0f)
            Push(thrust);
        // Apply thrust right
        Push(thrust);
    }

    /*
     * Move the player left according to the current control scheme.
     * In this case, pressing the left button on the UI adds velocity left-ward to the player.
     */
    public override void MoveLeft()
    {
        // Ensure the player can move.
        if (!CanMove())
            return;
        // Give the movement sound a random low pitch.
        audioSource.pitch = Random.Range(.53f, .55f);
        // If the player is changing directions, apply double thrust left.
        if (pm.rb.velocity.x > 0f)
            Push(-thrust);
        // Apply thrust left
        Push(-thrust);
    }

    /*
     * Move the player according to the current control scheme.
     * Unused, throws an error if the player inputs a forward command.
     */
    public override void MoveForward()
    {
        throw new System.NotImplementedException();
    }

    /*
     * Speed up the player according to the thrust value.
     * thrust: the speed and direction of the thrust.
     */
    private void Push(float thrust)
    {
        audioSource.Play();
        pm.rb.velocity += Vector2.right * thrust;
    }
}
