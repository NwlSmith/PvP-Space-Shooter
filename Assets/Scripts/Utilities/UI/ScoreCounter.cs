using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: UI Utility for updating and displaying scores.
 */
public class ScoreCounter : MonoBehaviour {

    // Public objects.
    public int playerNum;

    // Private objects.
    private int score = 0;
    private Text text;
    private Animator anim;

    private void Awake()
    {
        // Retrieve relevant UI and animation elements.
        text = GetComponent<Text>();
        anim = GetComponent<Animator>();
        // Make the scores blink initially.
        anim.SetTrigger("Blink");
    }

    /*
     * Set the score to the given value and trigger animation.
     * Animation triggers UpdateText() while the alpha value of the text is 0.
     * Invoked in GameManager.UpdateScoreCounters() and GameManager.ResetValues().
     * newScore: the score that will appear in the UI.
     */
    public void SetScore(int newScore)
    {
        score = newScore;
        anim.SetTrigger("Blink");
    }

    /*
     * Update the text with the current score.
     * Invoked in the "Blink" animation when alpha = 0;
     */
    private void UpdateText()
    {
        string scoreString = score.ToString();
        if (score < 10)
            scoreString = "0" + score.ToString();
        text.text = scoreString;
    }
}
