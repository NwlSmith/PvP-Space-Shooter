using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MenuController
{

    public static CreditsController instance = null;

    private List<UIMovement> creditsMenuUI = new List<UIMovement>();

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
            if (uim.tag == "CreditsMenu")
                creditsMenuUI.Add(uim);
    }

    public override void DisplayMenu()
    {
        MainMenuController.instance.RemoveMenu();
        DisplayUI(creditsMenuUI);
    }

    public override void RemoveMenu()
    {
        MainMenuController.instance.DisplayMenu();

        RemoveUI(creditsMenuUI);
    }
}
