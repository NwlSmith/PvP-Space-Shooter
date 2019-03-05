using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected virtual void CalculateAngle()
    {
        float z = 0;
        if (pm.playerNum == 1)
            z = -pm.rb.velocity.x;
        else if (pm.playerNum == 2)
            z = -pm.rb.velocity.x + 180f;
        else
            Debug.Log("Invalid PlayerNum: " + pm.playerNum);

        Quaternion angle = Quaternion.Euler(new Vector3(0f, 0f, -z));

        transform.rotation = angle;
    }
}
