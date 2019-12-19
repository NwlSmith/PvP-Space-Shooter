using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: The MenuController for the Player UI.
 * Is a single instance static object - There should only be 1 PlayerUIController.
 * Deals with adding and removing the player control UI elements from the screen, and transitioning between the game and the pause menu.
 */
public class PlayerUIController : MenuController {

    // Static instance of the object.
    public static PlayerUIController instance = null;

    // Private objects.
    // Elements in this UI
    private List<UIMovement> playerButtonsUI = new List<UIMovement>();

    private void Awake()
    {
        // Ensure that there is only one instance of the PlayerUIController.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        // This controller can be destroyed on a sceen transition.
    }

    private void Start()
    {
        // For ease of prototyping multiple different main menus,
        // any active object tagged as "playerUIMenu" that use a moving menu item (has the UIMovement script)
        // will be added to the mainMenuUI.
        foreach (UIMovement uim in GameManager.instance.uIMovements)
            if (uim.tag == "PlayButtonsUI")
                playerButtonsUI.Add(uim);
    }

    /*
     * Display the UI of the player controls.
     * Invoked on Play and Unpause buttons.
     */
    public override void DisplayMenu()
    {
        DisplayUI(playerButtonsUI);
    }

    /*
     * Display the UI of the player controls.
     * Invoked on the Pause button.
     */
    public override void RemoveMenu()
    {
        RemoveUI(playerButtonsUI);
    }

    /*
     * Transition to the Pause Menu.
     * Invoked on the Pause button.
     */
    public void Pause()
    {
        PauseController.instance.Pause();
    }
}
