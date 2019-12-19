using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 3/3/2019
 * Creator: Nate Smith
 * 
 * Description: The MenuController for the Instructions Screen.
 * Is a single instance static object - There should only be 1 InstructionsController.
 * Deals with adding and removing UI elements from the screen.
 */
public class InstructionsController : MenuController
{
    // Static instance of the object.
    public static InstructionsController instance = null;

    // Private objects.
    // Elements in this UI
    private List<UIMovement> instructionsMenuUI = new List<UIMovement>();

    private void Awake()
    {
        // Ensure that there is only one instance of the InstructionsController.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // Retain object on scene transition.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // For ease of prototyping multiple different options menus,
        // any active object tagged as "InstructionsMenu" that use a moving menu item (has the UIMovement script).
        // will be added to the instructionsMenuUI.
        foreach (UIMovement uim in GameManager.instance.uIMovements)
            if (uim.tag == "InstructionsMenu")
                instructionsMenuUI.Add(uim);
    }

    /*
     * Remove Main Menu (also inherits from MenuController).
     * Display the UI of the options menu.
     * Invoked in MainMenuController when pressing Instructions Button.
     */
    public override void DisplayMenu()
    {
        MainMenuController.instance.RemoveMenu();
        DisplayUI(instructionsMenuUI);
    }

    /*
     * Display Main Menu (also inherits from MenuController).
     * Remove the UI of the Instructions menu.
     * Invoked on 'Back' buttons in Instructions menu.
     */
    public override void RemoveMenu()
    {
        MainMenuController.instance.DisplayMenu();

        RemoveUI(instructionsMenuUI);
    }
}
