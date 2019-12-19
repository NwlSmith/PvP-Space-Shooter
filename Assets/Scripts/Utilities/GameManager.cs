using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: The overall manager for the game.
 * Is a single instance static object - There should only be 1 GameManager.
 * Deals with all elements of the gameplay, music, stages, points, ui movements, player hits, and scene management.
 * Coordinates between various controllers.
 */
public class GameManager : MonoBehaviour {

    // Static instance of the object.
    public static GameManager instance = null;

    // Public objects.
    public int player1Points = 0;
    public int player2Points = 0;
    public int stage = 1;
    public int maxStages = 6;
    public int stageLength = 4;
    public bool musicOn = true;
    public bool paused = false;
    [HideInInspector] public UIMovement[] uIMovements;
    public ScoreCounter[] scoreCounters;

    // Private objects.
    private AnnouncementController announcementController;
    private PauseController pauseController;
    private PlayerManager[] playerManagers;

    private void Awake()
    {
        // Ensure that there is only one instance of the GameManager.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        // Gather every UI Element in the scene for MenuControllers to draw from.
        uIMovements = FindObjectsOfType<UIMovement>();
        // Retain object on scene transition.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Gather the Managers and Controllers of the score counter, the announcement system, the pause screen, and the players.
        scoreCounters = FindObjectsOfType<ScoreCounter>();
        announcementController = FindObjectOfType<AnnouncementController>();
        pauseController = FindObjectOfType<PauseController>();
        playerManagers = FindObjectsOfType<PlayerManager>();
    }

    /*
     * Intake input for the pause menu. For testing purposes. Normally pause() and Unpause() are called from buttons.
     */
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!paused)
                pauseController.Pause();
            else
                pauseController.UnPause();
        }
    }

    /*
     * Utility function to add score, update score counters, and calculate stage transitions.
     * Invoked when enemies are hit, in the PlayerManager class.
     */
    public void PlayerHit(int hitPlayerNum)
    {
        if (hitPlayerNum == 1)
            player2Points++;
        else if (hitPlayerNum == 2)
            player1Points++;
        else
            Debug.Log("Error: invalid player number: " + hitPlayerNum);

        UpdateScoreCounters(hitPlayerNum);

        // Determine if the stage needs to be advanced, or if the game is over.
        int max = Mathf.Max(player1Points, player2Points);
        if (max % stageLength == 0 && max/stageLength == stage)
        {
            stage++;
            if (stage == maxStages)
            {
                PlayerUIController.instance.RemoveMenu();
                Invoke("EndGame", 4f);
                announcementController.EndGame();
                MusicManager.instance.songNum = -1;
            }
            else
            {
                announcementController.NextStage();
            }
            if (musicOn)
                MusicManager.instance.Transition();
            announcementController.TriggerMovement();
        }
    }

    /*
     * Update the score in the ScoreCounter UI.
     * Invoked in Player Hit.
     */
    public void UpdateScoreCounters(int hitPlayerNum)
    {
        // For testing purposes, there can be any number of score counters in the scene.
        foreach (ScoreCounter sc in scoreCounters)
        {
            if (hitPlayerNum == 2 && sc.playerNum == 1)
                sc.SetScore(player1Points);
            else if (hitPlayerNum == 1 && sc.playerNum == 2)
                sc.SetScore(player2Points);
            else if (sc.playerNum != 1 && sc.playerNum != 2)
                Debug.Log("Error: ScoreCounter named: " + sc.gameObject.name + " has an invalid player number: " + sc.playerNum);
        }
    }

    /*
     * Start the game, triggering music transitions, stage announcement movement, and presenting the PlayerUI.
     * Invoked upon pressing the "Play" button.
     */
    public void StartGame()
    {
        stage = 1;
        if (musicOn) // this won't work
            MusicManager.instance.Transition();
        announcementController.NextStage();
        announcementController.TriggerMovement();
        StartCoroutine(PUIMovementDelayed());
        ResetValues();
    }

    /*
     * After a delay, display the each player's user interface.
     * Invoked in StartGame.
     */
    private IEnumerator PUIMovementDelayed()
    {
        yield return new WaitForSeconds(1.5f);
        PlayerUIController.instance.DisplayMenu();
        scoreCounters = FindObjectsOfType<ScoreCounter>();
    }

    /*
     * Reset all values in the game, including points, player positions, and score displays.
     * Invoked in StartGame().
     */
    public void ResetValues()
    {
        paused = false;
        player1Points = player2Points = 0;
        foreach (ScoreCounter sc in scoreCounters)
            sc.SetScore(0);
        foreach(PlayerManager pm in playerManagers)
            pm.ResetValues();
    }

    /*
     * Stop the current game and return to the main menu.
     * Invoked in the EndGame function
     */
    public void StopGame()
    {
        MainMenuController.instance.DisplayMenu();
        DestroyBullets();

        //move players back to start
        foreach (PlayerManager pm in playerManagers)
            pm.ResetValues();
        MusicManager.instance.RestartMusic();
    }

    /*
     * Immediately destroys all bullet gameobjects in the scene.
     */
    private void DestroyBullets()
    {
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach (Bullet b in bullets)
            b.DestroyMe();
    }
}
