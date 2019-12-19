using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: The MenuController for the Pause Menu.
 * Is a single instance static object - There should only be 1 PauseController.
 * Deals with adding and removing UI elements from the screen, transitioning between menus and the game.
 */
public class PauseController : MenuController
{
    // Static instance of the object.
    public static PauseController instance = null;

    // Private objects.
    // Elements in this UI
    private List<UIMovement> pauseMenuUI = new List<UIMovement>();

    private void Awake()
    {
        // Ensure that there is only one instance of the PauseController.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // Retain object on scene transition.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // For ease of prototyping multiple different pause menus,
        // any active object tagged as "pauseMenu" that use a moving menu item (has the UIMovement script)
        // will be added to the pauseMenuUI.
        foreach (UIMovement uim in GameManager.instance.uIMovements)
            if (uim.tag == "PauseMenu")
                pauseMenuUI.Add(uim);
    }

    /*
     * Display the UI of the pause menu.
     * Invoked when pressing the Pause button.
     */
    public override void DisplayMenu()
    {
        DisplayUI(pauseMenuUI);
    }

    /*
     * Remove the UI of the pause menu.
     * Invoked in various MenuControllers when transitioning between them and the pause menu, and when resuming or quitting the game.
     */
    public override void RemoveMenu()
    {
        RemoveUI(pauseMenuUI);
    }

    /*
     * Pause the game.
     * Display the Pause Menu, tell the GameManager that the game is paused, and stop time.
     */
    public void Pause()
    {
        DisplayMenu();
        PlayerUIController.instance.RemoveMenu();
        GameManager.instance.paused = true;
        StopTime();
    }

    /*
     * Unpause the game.
     * Remove the Pause Menu, tell the GameManager that the game is unpaused, and start time.
     */
    public void UnPause()
    {
        RemoveMenu();
        PlayerUIController.instance.DisplayMenu();
        GameManager.instance.paused = false;
        StartTime();
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
     * Transition to the Main Menu, ending the game.
     * Invoked on the Quit button.
     */
    public void Quit()
    {
        GameManager.instance.StopGame();
        StartTime();
        RemoveMenu();
    }

    /*
     * Start lerp to bring time to a stop.
     * Invoked in Pause.
     */
    public void StopTime()
    {
        StartCoroutine(TimeLerp(0f));
    }

    /*
     * Start lerp to bring time back to normal.
     * Invoked in Unpause and Quit.
     */
    public void StartTime()
    {
        StartCoroutine(TimeLerp(1f));
    }

    /*
     * Lerp time to desired timescale.
     * The timescale is smoothly reset based on:
     * The fraction of how much time has passed since the start of the Lerp
     * Divided by the total duration of the Lerp.
     * Invoked by StartTime and StopTime.
     * targetTimeScale: the target timescale that will be achieved at the end of the lerp.
     */
    private IEnumerator TimeLerp(float targetTimeScale)
    {
        float duration = .75f;
        float elapsedTime = 0f;
        float startTimeScale = Time.timeScale;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.SmoothStep(startTimeScale, targetTimeScale, (elapsedTime / duration));
            yield return null;
        }
        Time.timeScale = targetTimeScale;
    }
}
