using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsController : MenuController {

    public static OptionsController instance = null;

    public AudioMixer audioMixer;

    private List<UIMovement> optionsMenuUI = new List<UIMovement>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        foreach (UIMovement uim in GameManager.instance.uIMovements)
            if (uim.tag == "OptionsMenu")
                optionsMenuUI.Add(uim);
    }

    public override void DisplayMenu()
    {
        // Need some way to remove MainMenuUI or PauseUI
        PauseController.instance.RemoveMenu();
        MainMenuController.instance.RemoveMenu();
        DisplayUI(optionsMenuUI);
    }

    public override void RemoveMenu()
    {
        if (GameManager.instance.paused)
            PauseController.instance.DisplayMenu();
        else
            MainMenuController.instance.DisplayMenu();

        RemoveUI(optionsMenuUI);
    }

    public void SetVolMusic(float vol)
    {
        audioMixer.SetFloat("VolMusic", Mathf.Log10(vol) * 20);
    }

    public void SetVolSFX(float vol)
    {
        audioMixer.SetFloat("VolSFX", Mathf.Log10(vol) * 20);
    }
}
