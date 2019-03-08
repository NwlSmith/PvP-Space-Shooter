using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public int player1Points = 0;
    public int player2Points = 0;
    public int stage = 1;
    public int maxStages = 6;
    public int stageLength = 4;
    public bool musicOn = true;
    public bool paused = false;
    [HideInInspector] public UIMovement[] uIMovements;

    public ScoreCounter[] scoreCounters;
    private AnnouncementController announcementController;
    private PauseController pauseController;
    private PlayerManager[] playerManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        uIMovements = FindObjectsOfType<UIMovement>();

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        scoreCounters = FindObjectsOfType<ScoreCounter>();
        announcementController = FindObjectOfType<AnnouncementController>();
        pauseController = FindObjectOfType<PauseController>();
        playerManager = FindObjectsOfType<PlayerManager>();
        //uIMovements = FindObjectsOfType<UIMovement>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !paused)
            pauseController.Pause();
        else if (Input.GetButtonDown("Pause") && paused)
            pauseController.UnPause();
    }

    public void PlayerHit(int hitPlayerNum)
    {
        if (hitPlayerNum == 1)
            player2Points++;
        else if (hitPlayerNum == 2)
            player1Points++;
        else
            Debug.Log("Error: invalid player number: " + hitPlayerNum);
        UpdateScoreCounters(hitPlayerNum);

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

    public void UpdateScoreCounters(int hitPlayerNum)
    {
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

    private IEnumerator PUIMovementDelayed()
    {
        yield return new WaitForSeconds(1.5f);
        PlayerUIController.instance.DisplayMenu();
        scoreCounters = FindObjectsOfType<ScoreCounter>();
    }

    public void ResetValues()
    {
        paused = false;
        player1Points = player2Points = 0;
        foreach (ScoreCounter sc in scoreCounters)
            sc.SetScore(0);
        foreach(PlayerManager pm in playerManager)
            pm.ResetValues();
    }

    public void StopGame()
    {
        MainMenuController.instance.DisplayMenu();
        DestroyBullets();
        //move players back to start
        foreach (PlayerManager pm in playerManager)
            pm.ResetValues();
        //MusicManager.instance.RestartMusic();
    }

    private void DestroyBullets()
    {
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach (Bullet b in bullets)
            b.DestroyMe();
    }

    private void EndGame()
    {
        StopGame();
        PauseController.instance.RemoveMenu();
    }
}
