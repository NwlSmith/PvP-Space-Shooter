using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    public int playerNum;
    private int score = 0;
    private Text text;
    private Animator anim;

    private void Awake()
    {
        text = GetComponent<Text>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("Blink");
    }

    public void AddPoint()
    {
        score++;
        anim.SetTrigger("Blink");
    }

    public void SetScore(int newScore)
    {
        score = newScore;
        anim.SetTrigger("Blink");
    }

    private void UpdateText()
    {
        string scoreString = score.ToString();
        if (score < 10)
            scoreString = "0" + score.ToString();
        text.text = scoreString;
    }

}
