using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 3/3/2019
 * Creator: Nate Smith
 * 
 * Description: The MenuController for the Credits screens.
 * .
 * Is a single instance static object - There should only be 1 CreditsController.
 * Deals with adding and removing UI elements from the screen.
 */
public class CreditsController : MenuController
{
    // Static instance of the object.
    public static CreditsController instance = null;

    // Private objects.
    // Elements in this UI
    private List<UIMovement> creditsMenuUI = new List<UIMovement>();

    private void Awake()
    {
        // Ensure that there is only one instance of the CreditsController.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // Retain object on scene transition.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // For ease of prototyping multiple different credits screens,
        // any active object tagged as "CreditsMenu" that use a moving menu item (has the UIMovement script).
        // will be added to the creditsMenuUI.
        foreach (UIMovement uim in GameManager.instance.uIMovements)
            if (uim.tag == "CreditsMenu")
                creditsMenuUI.Add(uim);
    }

    /*
     * Remove Main Menu (also inherits from MenuController).
     * Display the UI of the Credits screen.
     * Invoked in PauseMenuController and MainMenuController when pressing Options Button.
     */
    public override void DisplayMenu()
    {
        MainMenuController.instance.RemoveMenu();
        DisplayUI(creditsMenuUI);
    }

    /*
     * Display Main Menu (also inherit from MenuController).
     * Remove the UI of the credits screen.
     * Invoked on 'Back' buttons in credits screen.
     */
    public override void RemoveMenu()
    {
        MainMenuController.instance.DisplayMenu();

        RemoveUI(creditsMenuUI);
    }
}
