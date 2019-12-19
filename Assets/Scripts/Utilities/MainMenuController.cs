using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: The MenuController for the Main Menu.
 * Is a single instance static object - There should only be 1 MainMenuController.
 * Deals with adding and removing UI elements from the screen, and transitioning between menus and the game.
 */
public class MainMenuController : MenuController {

    // Static instance of the object.
    public static MainMenuController instance = null;

    // Private objects.
    // Elements in this UI
    private List<UIMovement> mainMenuUI = new List<UIMovement>();
    private List<UIMovement> playButtonsUI = new List<UIMovement>();

    private void Awake()
    {
        // Ensure that there is only one instance of the MainMenuController.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // Retain object on scene transition.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // For ease of prototyping multiple different main menus,
        // any active object tagged as "mainMenu" that use a moving menu item (has the UIMovement script)
        // will be added to the mainMenuUI.
        foreach (UIMovement uim in GameManager.instance.uIMovements)
            if (uim.tag == "MainMenu")
                mainMenuUI.Add(uim);
        // Display main menu.
        StartCoroutine(MoveUIUp());
    }

    /*
     * Display the UI of the main menu after 1 second delay.
     * Invoked in Start.
     */
    private IEnumerator MoveUIUp()
    {
        yield return new WaitForSeconds(1f);
        DisplayMenu();
    }

    /*
     * Display the UI of the main menu.
     * Invoked after delay in MoveUIUp.
     */
    public override void DisplayMenu()
    {
        DisplayUI(mainMenuUI);
    }

    /*
     * Remove the UI of the main menu.
     * Invoked in various MenuControllers when transitioning between them and the main menu, and when starting the game.
     */
    public override void RemoveMenu()
    {
        RemoveUI(mainMenuUI);
    }

    /*
     * Remove the UI of the main menu and start the game.
     * Invoked on the Start button.
     */
    public void StartGame()
    {
        RemoveMenu();
        GameManager.instance.StartGame();
    }

    /*
     * Transition to the Options Menu.
     * Invoked on the Options button.
     */
    public void Options()
    {
        OptionsController.instance.DisplayMenu();
    }

    /*
     * Transition to the Instructions screen.
     * Invoked on the Instructions button.
     */
    public void Instructions()
    {
        InstructionsController.instance.DisplayMenu();
    }

    /*
     * Transition to the Credits screen.
     * Invoked on the Credits button.
     */
    public void Credits()
    {
        CreditsController.instance.DisplayMenu();
    }

    /*
     * Bring players to my website.
     * Invoked on the Start button.
     */
    public void Logo()
    {
        Application.OpenURL("http://www.nwlsmith.com");
    }
}
