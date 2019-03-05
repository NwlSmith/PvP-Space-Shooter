using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MenuController
{

    public static PauseController instance = null;

    private List<UIMovement> pauseMenuUI = new List<UIMovement>();

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
            if (uim.tag == "PauseMenu")
                pauseMenuUI.Add(uim);
    }

    public override void DisplayMenu()
    {
        DisplayUI(pauseMenuUI);
    }

    public override void RemoveMenu()
    {
        RemoveUI(pauseMenuUI);
    }

    public void Pause()
    {
        Debug.Log("Pausing");
        DisplayMenu();
        PlayerUIController.instance.RemoveMenu();
        GameManager.instance.paused = true;
        StopTime();
    }

    public void UnPause()
    {
        Debug.Log("Unpausing");
        RemoveMenu();
        PlayerUIController.instance.DisplayMenu();
        GameManager.instance.paused = false;
        StartTime();
    }

    public void Options()
    {
        OptionsController.instance.DisplayMenu();
    }

    public void Quit()
    {
        GameManager.instance.StopGame();
        StartTime();
        RemoveMenu();
    }

    public void StopTime()
    {
        StartCoroutine(TimeLerp(0f));
    }

    public void StartTime()
    {
        StartCoroutine(TimeLerp(1f));
    }

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
    }
}
