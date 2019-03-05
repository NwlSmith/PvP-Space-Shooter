using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    public PlayerManager playerManager;

    public void Left()
    {
        playerManager.playerMovement.MoveLeft();
    }

    public void Forward()
    {
        playerManager.playerMovement.MoveForward();
    }

    public void Right()
    {
        playerManager.playerMovement.MoveRight();
    }

    public void Fire()
    {
        playerManager.playerShooter.Fire();
    }
}
