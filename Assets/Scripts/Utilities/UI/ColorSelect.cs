using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: UI Utility that selects and sets the various texts, sprites, and buttons
 * to the selected color at the start of the game.
 * This script is intended to quickly and easily color new UI objects.
 */
public class ColorSelect : MonoBehaviour {

    //Public objects.
    public PlayerManager pm;

    /*
     * If a certain element exists on this UI gameobject,
     * set it to this player's color while preserving the alpha value.
     */
    private void Start()
    {
        Shadow s = GetComponent<Shadow>();
        if (s != null)
            s.effectColor = SetColorNoAlpha(s.effectColor.a);
        Text t = GetComponent<Text>();
        if (t != null)
        {
            t.color = SetColorNoAlpha(t.color.a);
            return;
        }
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = SetColorNoAlpha(sr.color.a);
            return;
        }
        Button b = GetComponent<Button>();
        if (b != null)
        {
            ColorBlock cb = b.colors;
            cb.pressedColor = SetColorNoAlpha(cb.pressedColor.a);
            b.colors = cb;
        }
    }

    /*
     * Return this player's color while preserving alpha.
     * alpha: Preserved alpha.
     */
    private Color SetColorNoAlpha(float alpha)
    {
        return new Color(pm.color.r, pm.color.g, pm.color.b, alpha);
    }
}
