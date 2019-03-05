using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MenuController {

    public static MainMenuController instance = null;

    private List<UIMovement> mainMenuUI = new List<UIMovement>();
    private List<UIMovement> playButtonsUI = new List<UIMovement>();

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
            if (uim.tag == "MainMenu")
                mainMenuUI.Add(uim);
        StartCoroutine(MoveUIUp());
    }

    private IEnumerator MoveUIUp()
    {
        yield return new WaitForSeconds(1f);
        DisplayMenu();
    }

    public override void DisplayMenu()
    {
        //animations
        DisplayUI(mainMenuUI);
    }

    public override void RemoveMenu()
    {
        RemoveUI(mainMenuUI);
    }

    public void StartGame()
    {
        RemoveMenu();
        GameManager.instance.StartGame();
    }

    public void Options()
    {
        OptionsController.instance.DisplayMenu();
    }

    public void Instructions()
    {
        InstructionsController.instance.DisplayMenu();
    }

    public void Credits()
    {
        CreditsController.instance.DisplayMenu();
    }

    public void Logo()
    {
        Application.OpenURL("http://www.nwlsmith.com");
    }
}
