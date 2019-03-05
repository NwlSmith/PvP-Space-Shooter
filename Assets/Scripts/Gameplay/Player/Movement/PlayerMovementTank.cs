using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// left and right rotate, they don't move. forward makes you go forward.
public class PlayerMovementTank : PlayerMovement {

    public float m_thrust = 7f;
    public float m_turn_thrust = 50f;
    private PlayerTiltTank playerTiltTank;

    protected override void Awake()
    {
        base.Awake();
        playerTiltTank = GetComponentInChildren<PlayerTiltTank>();
    }

    protected override void HandleInput()
    {

        if (Input.GetButtonDown(pm.right))
            MoveRight();
        if (Input.GetButtonDown(pm.left))
            MoveLeft();
        if (Input.GetButtonDown("Forward1"))
            MoveForward();
    }

    public override void MoveRight()
    {
        if (!CanMove())
            return;
        audioSource.pitch = Random.Range(.55f, .58f);
        if (pm.rb.velocity.x < 0f)
            //pm.rb.velocity = Vector2.zero;
            Turn(-m_turn_thrust);
        Turn(-m_turn_thrust);
    }

    public override void MoveLeft()
    {
        if (!CanMove())
            return;
        audioSource.pitch = Random.Range(.53f, .55f);
        if (pm.rb.velocity.x > 0f)
            //pm.rb.velocity = Vector2.zero;
            Turn(m_turn_thrust);
        Turn(m_turn_thrust);
        
    }

    public override void MoveForward()
    {
        Debug.Log("MoveForward");
        if (!CanMove())
            return;
        audioSource.pitch = Random.Range(.53f, .55f);
        Push(m_thrust);
    }

    private void Turn(float thrust)
    {
        audioSource.Play();
        Debug.Log("Angular velocity = " + pm.rb.angularVelocity);
        pm.rb.angularVelocity +=  thrust;
    }

    private void Push(float thrust)
    {
        audioSource.Play();
        //pm.rb.AddForce(Vector2.right * thrust);
        pm.rb.velocity += (Vector2)playerTiltTank.transform.up * thrust;
    }
}
