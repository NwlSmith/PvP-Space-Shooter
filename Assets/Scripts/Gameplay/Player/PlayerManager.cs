using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public int playerNum = 0;

    public Color color;

    [HideInInspector] public int score = 0;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool canShoot = true;
    [HideInInspector] public bool canBeHit = true;
    [HideInInspector] public string right;
    [HideInInspector] public string left;
    [HideInInspector] public string fire;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerShooter playerShooter;
    private Animator[] anims;
    private GameManager gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponentInChildren<PlayerShooter>();
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        if (playerNum != 1 && playerNum != 2)
            Debug.Log("Error: Player has not been given a valid number");

        right = "Right" + playerNum.ToString();
        left = "Left" + playerNum.ToString();
        fire = "Fire" + playerNum.ToString();
        anims = GetComponentsInChildren<Animator>();
    }

    public IEnumerator Hit()
    {
        canMove = false;
        canShoot = false;
        foreach (Animator anim in anims)
            anim.SetTrigger("Spin");
        canBeHit = false;
        gameManager.PlayerHit(playerNum);
        yield return new WaitForSeconds(1f);
        canMove = true;
        yield return new WaitForSeconds(1f);
        canBeHit = true;
        canShoot = true;
    }

    public void ResetValues()
    {
        StartCoroutine(playerMovement.LerpToStartPos());
        canMove = true;
        canShoot = true;
        canBeHit = true;
    }
}
