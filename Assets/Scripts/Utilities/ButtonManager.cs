using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: Controls the interactions between individual buttons and player movement/shooting.
 * Found on each PlayerUI Button
 */
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
