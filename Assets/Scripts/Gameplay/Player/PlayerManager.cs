using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: The primary manager for each Player Function.
 * Holds important gameobjects, animations, player information, controls, and booleans.
 */
public class PlayerManager : MonoBehaviour {

    // Public objects.
    public int playerNum = 0;
    public Color color;

    // Public Hidden variables.
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

    // Private variables.
    private Animator[] anims;
    private GameManager gameManager;

    private void Awake()
    {
        // Get private references to frequently utilized objects on the Player GameObject.
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponentInChildren<PlayerShooter>();
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        if (playerNum != 1 && playerNum != 2)
            Debug.Log("Error: Player has not been given a valid number");

        // Set up the strings that will be used for each player's inputs.
        right = "Right" + playerNum.ToString();
        left = "Left" + playerNum.ToString();
        fire = "Fire" + playerNum.ToString();
        anims = GetComponentsInChildren<Animator>();
    }

    /*
     * Controls the consequences of a hit for the player.
     * Called when a player is hit by a bullet or other hazard.
     */
    public IEnumerator Hit()
    {
        // Prevent movement, shooting, and make player invulnerable.
        canMove = false;
        canShoot = false;
        canBeHit = false;
        // Cause the spin animation in the children of the ship.
        foreach (Animator anim in anims)
            anim.SetTrigger("Spin");
        // Report to the GameManager that this player was hit.
        gameManager.PlayerHit(playerNum);
        yield return new WaitForSeconds(1f);
        // Allow the player to move after 1 second.
        canMove = true;
        yield return new WaitForSeconds(1f);
        // Allow the player to shoot and be shot after another 1 second.
        canBeHit = true;
        canShoot = true;
    }

    /*
     * Resets the values in this player. Sets the player back to their start position and enables movement, shooting, and the ability to be hit.
     */
    public void ResetValues()
    {
        StartCoroutine(playerMovement.LerpToStartPos());
        canMove = true;
        canShoot = true;
        canBeHit = true;
    }
}
