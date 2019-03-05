using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTiltTank : PlayerTilt {

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
