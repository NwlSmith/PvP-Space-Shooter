using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHorizontal : PlayerMovement {

    private float m_thrust = 7f;

    private Vector2 maxVelocity = new Vector2(20f, 0f);
    

    protected override void Update () {
        base.Update();
        if (pm.rb.velocity.x > maxVelocity.x)
        {
            //Debug.Log("Hit max speed! speed = " + pm.rb.velocity.x.ToString());
            pm.rb.velocity = maxVelocity;
        }
        if (pm.rb.velocity.x < - maxVelocity.x)
        {
            //Debug.Log("Hit max negative speed! speed = " + pm.rb.velocity.x.ToString());
            pm.rb.velocity =  - maxVelocity;
        }
    }

    public override void MoveRight()
    {
        if (!CanMove())
            return;
        audioSource.pitch = Random.Range(.55f, .58f);
        if (pm.rb.velocity.x < 0f)
            //pm.rb.velocity = Vector2.zero;
            Push(m_thrust);
        Push(m_thrust);
    }

    public override void MoveLeft()
    {
        if (!CanMove())
            return;
        audioSource.pitch = Random.Range(.53f, .55f);
        if (pm.rb.velocity.x > 0f)
            //pm.rb.velocity = Vector2.zero;
            Push(-m_thrust);
        Push(-m_thrust);
    }

    public override void MoveForward()
    {
        throw new System.NotImplementedException();
    }

    private void Push(float thrust)
    {
        audioSource.Play();
        //pm.rb.AddForce(Vector2.right * thrust);
        pm.rb.velocity += Vector2.right * thrust;
    }
}
