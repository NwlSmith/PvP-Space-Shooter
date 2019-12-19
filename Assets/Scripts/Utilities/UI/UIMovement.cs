using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: UI utility for moving UI elements on and off the screen.
 * Attached to each UI element.
 */
public class UIMovement : MonoBehaviour {

    // Public objects.
    public bool startOnScreen = false;
    public bool onScreen = false;
    [HideInInspector] public Vector3 onScreenPos;
    public Vector3 offScreenPos;

    // Private objects.
    private RectTransform rectTransform;
   
    private void Start()
    {
        // Retrieve starting information.
        rectTransform = GetComponent<RectTransform>();
        onScreenPos = rectTransform.anchoredPosition;
        onScreen = startOnScreen;
        // If not designated as starting on screen, move offscreen.
        if (!startOnScreen)
        {
            rectTransform.anchoredPosition = offScreenPos;
            gameObject.SetActive(false);
        }
    }

    /*
     * Activate the gameObject, and start the coroutine to move this UI element on screen.
     * Invoked in MenuController.DisplayUI().
     */
    public void MoveOnScreen()
    {
        gameObject.SetActive(true);
        StartCoroutine(MoveToTarget(onScreenPos));
        onScreen = true;
    }

    /*
     * Ensure the gameObject is activated, and start the coroutine to move this UI element off screen.
     * Invoked in MenuController.RemoveUI().
     */
    public void MoveOffScreen()
    {
        gameObject.SetActive(true);
        StartCoroutine(MoveToTarget(offScreenPos));
        onScreen = false;
    }

    /*
     * Smoothly Lerp this UI element to the desired target position.
     * Invoked in MoveOnScreen() and MoveOffScreen().
     * targetPos: the target position.
     */
    private IEnumerator MoveToTarget(Vector3 targetPos)
    {
        // Smoothly lerp to the target.
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
        rectTransform.anchoredPosition = targetPos;
        // If the object is now offscreen, deactivate it to save on cpu power
        if (!onScreen)
            gameObject.SetActive(false);
    }
}
