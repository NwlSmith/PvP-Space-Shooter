using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: The abstract MenuController class.
 * Children control menu/UI movement.
 */
public abstract class MenuController : MonoBehaviour {

    /*
     * Display each UI element with this child's tag.
     * uIMovements: the list of UI elements with the UIMovement script that will be displayed.
     */
    protected void DisplayUI(List<UIMovement> uIMovements)
    {
        foreach (UIMovement uim in uIMovements)
            uim.MoveOnScreen();
    }

    /*
     * Conceal each UI element with this child's tag.
     * uIMovements: the list of UI elements with the UIMovement script that will be concealed.
     */
    protected void RemoveUI(List<UIMovement> uIMovements)
    {
        foreach (UIMovement uim in uIMovements)
            uim.MoveOffScreen();
    }

    /*
     * The common Display and Remove menu functions that will invoke RemoveUI.
     */
    public abstract void DisplayMenu();
    public abstract void RemoveMenu();
}
