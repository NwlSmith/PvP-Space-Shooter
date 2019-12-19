using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/7/2018
 * Creator: Nate Smith
 * 
 * Description: The abstract PlayerMovement class.
 * Controls directional input and movement.
 * Initially the PlayerMovement was only PlayerMovementHorizontal,
 * but I wanted to test different movement/control schemes,
 * so I made it easier to test different options by making this abstract class.
 */
public abstract class PlayerMovement : MonoBehaviour
{

    // Public objects.
    public float maxSpeed;

    // Protected objects.
    protected PlayerManager pm;
    protected AudioSource audioSource;
    protected Vector3 startPos;

    protected virtual void Awake()
    {
        // Get private references to frequently utilized objects on the Player GameObject.
        pm = GetComponent<PlayerManager>();
        audioSource = GetComponent<AudioSource>();
    }

    protected void Start()
    {
        // Will return to here on GameOver.
        startPos = transform.position; 
    }

    /*
     * Call HandleInput every physics frame.
     */
    protected virtual void FixedUpdate()
    {
        HandleInput();
    }

    /*
     * Take input and call the corresponding movement function.
     * Used for testing movement schemes with a keyboard.
     * Because each control scheme will handle input differently, this class needs a virtual method.
     * Invoked in FixedUpdate function to use keyboard movement.
     */
    protected virtual void HandleInput()
    {
        if (Input.GetButtonDown(pm.right))
            MoveRight();
        if (Input.GetButtonDown(pm.left))
            MoveLeft();
    }
 
    /*
     * Checks if the player is able to move.
     * Overrides HandleInput functions.
     */
    protected bool CanMove()
    {
        return !GameManager.instance.paused && pm.canMove;
    }

    /*
     * Basic movement functions for each implementation.
     * Called in ButtonManagers for each direction.
     */
    public abstract void MoveLeft();
    public abstract void MoveForward();
    public abstract void MoveRight();

    /*
     * Reset position back to start and zero velocity.
     * Obsolete, values are now SmoothStepped back to their initial values.
     */
    public void ResetValues()
    {
        transform.position = startPos;
        pm.rb.velocity = Vector2.zero;
    }

    /*
     * Lerp position back to start.
     * The position is smoothly reset based on:
     * The fraction of how much time has passed since the start of the Lerp
     * Divided by the total duration of the Lerp.
     * Replaced ResetValues.
     * Called in PlayerManager in ResetValues for the entire player.
     */
    public IEnumerator LerpToStartPos()
    {
        float duration = .75f;
        float elapsedTime = 0f;
        Vector2 beforeLerpPos = transform.position;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float timeStep = elapsedTime / duration;
            transform.position = new Vector2(Mathf.SmoothStep(beforeLerpPos.x, startPos.x, timeStep), Mathf.SmoothStep(beforeLerpPos.y, startPos.y, timeStep));
            yield return null;
        }
        transform.position = startPos;
    }
}
