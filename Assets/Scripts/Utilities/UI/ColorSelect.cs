using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelect : MonoBehaviour {

    public PlayerManager pm;

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

    private Color SetColorNoAlpha(float alpha)
    {
        return new Color(pm.color.r, pm.color.g, pm.color.b, alpha);
    }
}
