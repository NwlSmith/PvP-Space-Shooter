using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMovement : MonoBehaviour {

    public bool startOnScreen = false;
    public bool onScreen = false;
    [HideInInspector] public Vector3 onScreenPos;
    public Vector3 offScreenPos;
    private RectTransform rectTransform; //getting reference to this component 
   
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        onScreenPos = rectTransform.anchoredPosition;
        onScreen = startOnScreen;
        if (!startOnScreen)
        {
            rectTransform.anchoredPosition = offScreenPos;
            gameObject.SetActive(false);
        }
    }

    public void MoveOnScreen()
    {
        gameObject.SetActive(true);
        StartCoroutine(MoveToTarget(onScreenPos));
        onScreen = true;
    }

    public void MoveOffScreen()
    {
        gameObject.SetActive(true);
        StartCoroutine(MoveToTarget(offScreenPos));
        onScreen = false;
    }

    private IEnumerator MoveToTarget(Vector3 targetPos)
    {
        float duration = .75f;
        float elapsedTime = 0f;
        Vector3 startPos = rectTransform.anchoredPosition;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float timeStep = elapsedTime / duration;
            rectTransform.anchoredPosition = new Vector2(Mathf.SmoothStep(startPos.x, targetPos.x, timeStep), Mathf.SmoothStep(startPos.y, targetPos.y, timeStep));
            yield return null;
        }
        if (!onScreen)
            gameObject.SetActive(false);
    }
}
