using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovement : MonoBehaviour {

    public float m_maxSpeed;
    protected PlayerManager pm;
    protected AudioSource audioSource;
    protected Vector3 startPos;

    protected virtual void Awake()
    {
        pm = GetComponent<PlayerManager>();
        audioSource = GetComponent<AudioSource>();
    }

    protected void Start()
    {
        startPos = transform.position;
    }

    protected virtual void Update()
    {
        HandleInput();
    }

    protected virtual void HandleInput()
    {
        if (Input.GetButtonDown(pm.right))
            MoveRight();
        if (Input.GetButtonDown(pm.left))
            MoveLeft();
    }

    protected bool CanMove()
    {
        return !GameManager.instance.paused && pm.canMove;
    }

    public abstract void MoveLeft();
    public abstract void MoveForward();
    public abstract void MoveRight();


    public void ResetValues()
    {
        transform.position = startPos;
        pm.rb.velocity = Vector2.zero;
    }

    public IEnumerator LerpToStartPos()
    {
        float duration = .75f;
        float elapsedTime = 0f;
        Vector2 beforeLerpPos = transform.position;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float timeStep = elapsedTime / duration;
            transform.position = new Vector2(Mathf.SmoothStep(beforeLerpPos.x, startPos.x, timeStep), Mathf.SmoothStep(beforeLerpPos.y, startPos.y, timeStep));
            yield return null;
        }
    }
}
