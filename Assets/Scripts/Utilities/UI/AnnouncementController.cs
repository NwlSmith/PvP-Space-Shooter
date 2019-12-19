using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: The Controller for announcements ingame.
 * Controls the displayed text in each announcement and their movement into and out of screen.
 */
public class AnnouncementController : MonoBehaviour
{
    // Public objects.
    [HideInInspector] public Text[] texts;
    
    // Private objects.
    private Animator anim;
    private GameManager gm;

    private void Start()
    {
        // Retrieve needed Components and GameManager.
        texts = GetComponentsInChildren<Text>();
        anim = GetComponent<Animator>();
        gm = GameManager.instance;
        // Set initial text on the Announcement.
        NextStage();
    }

    /*
     * Update text for new stage.
     * Invoked in Start(), and in GameManager in StartGame() and PlayerHit().
     */
    public void NextStage()
    {
        string newText = "Stage " + gm.stage.ToString();
        if (gm.stage == gm.maxStages - 1)
            newText = "FINAL ROUND!";
        UpdateText(newText);
    }

    /*
     * Update the text in each text object.
     * Invoked in NextStage() and EndGame().
     * newText: the text that will be displayed.
     */
    private void UpdateText(string newText)
    {
        foreach (Text text in texts)
            text.text = newText;
    }

    /*
     * Set the text to show the winner of the game.
     * Invoked at the end of the game in GameManager.PlayerHit().
     */
    public void EndGame()
    {
        UpdateText("Player " + (gm.player1Points > gm.player2Points ? "1" : "2") + " wins!!!");
    }

    /*
     * Triggers movement animations.
     * Invoked in GameManager in StartGame() and PlayerHit() when text needs to be displayed.
     */
    public void TriggerMovement()
    {
        anim.SetTrigger("Move");
    }

}
