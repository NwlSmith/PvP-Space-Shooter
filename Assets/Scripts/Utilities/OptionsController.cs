using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: The MenuController for the Options Menu.
 * Is a single instance static object - There should only be 1 OptionsController.
 * Deals with adding and removing UI elements from the screen, and controlling volumes.
 */
public class OptionsController : MenuController
{
    // Static instance of the object.
    public static OptionsController instance = null; 

    // Public objects.
    public AudioMixer audioMixer;

    // Private objects.
    // Elements in this UI
    private List<UIMovement> optionsMenuUI = new List<UIMovement>();

    private void Awake()
    {
        // Ensure that there is only one instance of the OptionsController.
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
        // any active object tagged as "OptionsMenu" that use a moving menu item (has the UIMovement script)
        // will be added to the optionsMenuUI.
        foreach (UIMovement uim in GameManager.instance.uIMovements)
            if (uim.tag == "OptionsMenu")
                optionsMenuUI.Add(uim);
    }

    /*
     * Remove Pause or Main Menus (also inherit from MenuController).
     * Display the UI of the options menu.
     * Invoked in PauseMenuController and MainMenuController when pressing Options Button.
     */
    public override void DisplayMenu()
    {
        if (GameManager.instance.paused)
            PauseController.instance.RemoveMenu();
        else
            MainMenuController.instance.RemoveMenu();
        DisplayUI(optionsMenuUI);
    }

    /*
     * Display Pause or Main Menus (also inherit from MenuController).
     * Remove the UI of the options menu.
     * Invoked on 'Back' buttons in options menu.
     */
    public override void RemoveMenu()
    {
        if (GameManager.instance.paused)
            PauseController.instance.DisplayMenu();
        else
            MainMenuController.instance.DisplayMenu();

        RemoveUI(optionsMenuUI);
    }

    /*
     * Set the volume of the music element of the AudioMixer to the assigned value.
     * Invoked on VolMusic slider.
     * vol: the assigned value for the music volume.
     */
    public void SetVolMusic(float vol)
    {
        audioMixer.SetFloat("VolMusic", Mathf.Log10(vol) * 20);
    }

    /*
     * Set the volume of the effects element of the AudioMixer to the assigned value.
     * Invoked on VolSFX slider.
     * vol: the assigned value for the effects volume.
     */
    public void SetVolSFX(float vol)
    {
        audioMixer.SetFloat("VolSFX", Mathf.Log10(vol) * 20);
    }
}
