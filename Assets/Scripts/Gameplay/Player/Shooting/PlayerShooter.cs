using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/7/2018
 * Creator: Nate Smith
 * 
 * Description: The abstract PlayerShooter class.
 * Controls shooting.
 * I wanted to test different shooting schemes,
 * so I made it easier to test different options by making this abstract class.
 * Shoots in direction the player is looking.
 * Used in 1PlayerAsteroids and 1PlayerAsteroidsTank.
 */
public abstract class PlayerShooter : MonoBehaviour {

    // Public objects.
    public GameObject bullet;

    // Private objects.
    protected AudioSource audioSource;
    protected PlayerManager pm;

    private void Awake()
    {
        // Get private references to frequently utilized objects on the Player GameObject.
        audioSource = GetComponent<AudioSource>();
        pm = GetComponentInParent<PlayerManager>();
    }

    /*
     * Detect whether the player will fire based on keyboard input.
     * Used only in testing.
     */
    protected virtual void Update()
    {
        if (Input.GetKeyDown("space"))
            Fire();
    }

    /*
     * Detect whether the player can fire and fire if yes.
     * Called in Update and on the Fire ui button.
     */
    public virtual void Fire()
    {
        // Detect if the player can fire.
        if (!GameManager.instance.paused && pm.canShoot)
        {
            audioSource.pitch = Random.Range(.20f, .25f);
            audioSource.Play();
            InstantiateBullet();
        }
    }

    /*
     * Instantiate a bullet object.
     * Bullets will fire in direction that the ship is pointing.
     */
    protected virtual void InstantiateBullet()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }

}
