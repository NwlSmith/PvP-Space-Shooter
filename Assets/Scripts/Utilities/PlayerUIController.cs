using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MenuController {

    public static PlayerUIController instance = null;

    private List<UIMovement> playerButtonsUI = new List<UIMovement>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach (UIMovement uim in GameManager.instance.uIMovements)
            if (uim.tag == "PlayButtonsUI")
                playerButtonsUI.Add(uim);
    }

    public override void DisplayMenu()
    {
        DisplayUI(playerButtonsUI);
    }

    public override void RemoveMenu()
    {
        RemoveUI(playerButtonsUI);
    }

    public void Pause()
    {
        PauseController.instance.Pause();
    }
}
