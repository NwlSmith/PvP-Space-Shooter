using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementJoystick : PlayerMovement
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update() { }

    private void FixedUpdate()
    {
        Vector2 moveVector = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) * m_maxSpeed * Time.deltaTime;
        if (moveVector != Vector2.zero && pm.canMove)
        {
            if (pm.rb.drag != 0f)
                pm.rb.drag = 1f;
            pm.rb.velocity += moveVector;
            Vector2 angleVector = Vector2.MoveTowards(pm.rb.velocity, moveVector, 5f * Time.deltaTime);
            float angle = (Mathf.Atan2(angleVector.y, angleVector.x) * Mathf.Rad2Deg) - 90f;
            
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
            pm.rb.drag = 3f;
    }

    public override void MoveRight() { }

    public override void MoveLeft() { }

    public override void MoveForward() { }
}
