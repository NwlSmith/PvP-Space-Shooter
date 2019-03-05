using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnouncementController : MonoBehaviour {

    //[HideInInspector] public float startSpacing = 880f;
    //[HideInInspector] public float endSpacing = -100f;
    //[HideInInspector] public Vector3 targetPos = Vector3.zero;
    [HideInInspector] public Text[] texts;
    private Animator anim;
    private GameManager gm;

    private void Start()
    {
        texts = GetComponentsInChildren<Text>();
        anim = GetComponent<Animator>();
        gm = GameManager.instance;
        NextStage();
    }

    private void UpdateText(string newText)
    {
        foreach (Text text in texts)
            text.text = newText;
    }

    public void NextStage()
    {
        string newText = "Stage " + gm.stage.ToString();
        if (gm.stage == gm.maxStages - 1)
            newText = "FINAL ROUND!";
        UpdateText(newText);
    }

    public void EndGame()
    {
        UpdateText("Player " + (gm.player1Points > gm.player2Points ? "1" : "2") + " wins!!!");
    }

    public void TriggerMovement()
    {
        anim.SetTrigger("Move");
    }

}
