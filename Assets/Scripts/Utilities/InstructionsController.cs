using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsController : MenuController
{

    public static InstructionsController instance = null;

    private List<UIMovement> instructionsMenuUI = new List<UIMovement>();

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
            if (uim.tag == "InstructionsMenu")
                instructionsMenuUI.Add(uim);
    }

    public override void DisplayMenu()
    {
        MainMenuController.instance.RemoveMenu();
        DisplayUI(instructionsMenuUI);
    }

    public override void RemoveMenu()
    {
        MainMenuController.instance.DisplayMenu();

        RemoveUI(instructionsMenuUI);
    }
}
