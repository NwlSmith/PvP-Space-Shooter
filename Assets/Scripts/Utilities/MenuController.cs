using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuController : MonoBehaviour {


    protected void DisplayUI(List<UIMovement> uIMovements)
    {
        foreach (UIMovement uim in uIMovements)
            uim.MoveOnScreen();
    }

    protected void RemoveUI(List<UIMovement> uIMovements)
    {
        foreach (UIMovement uim in uIMovements)
            uim.MoveOffScreen();
    }

    public abstract void DisplayMenu();

    public abstract void RemoveMenu();
}
