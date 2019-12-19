using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/3/2018
 * Creator: Nate Smith
 * 
 * Description: The Bullet object.
 * Controls the functionality of the bullets fired by players.
 * Moves directly upward in the direction of its initial rotation.
 * Used in every PlayerShooter class.
 */
public class Bullet : MonoBehaviour {

    // Public objects.
    public AudioClip[] audioClips;
    public float speed = 7.5f;
    public float destructTime = 6f;

    // Private objects.
    private Animator camAnim;
    private AudioSource audioSource;
    private Rigidbody2D rb;

    private void Awake()
    {
        // Get private references to frequently utilized objects on the Bullet GameObject.
        camAnim = FindObjectOfType<Camera>().GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Move upward.
        rb.velocity = transform.up * speed;
        // Destroy this gameobject after some amount of time to ensure no excess bullets.
        Destroy(gameObject, destructTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Do nothing in the case that it enters the ScreenBoundary.
        if (other.tag == "ScreenBoundary")
            return;
        if (other.tag == "Player")
        {
            PlayerManager pm = other.GetComponent<PlayerManager>();
            if (pm.canBeHit)
            {
                // If the colliding object is a player and they can be hit, damage the player and change to other player's color.
                StartCoroutine(pm.Hit());
                GetComponent<SpriteRenderer>().color = pm.color;
            }
            // Do not hit players that cannot be hit.
            else
                return;
        }
        // If the collision is with another bullet, turn purple.
        if (other.tag == "Bullet")
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 1f);
        }
        Explode();
    }

    /*
     * Explode the bullet, play sounds and animations, and delete the gameobject.
     */
    private void Explode()
    {
        // Play a random explosion sound at a random frequency.
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.pitch = Random.Range(.75f, 1.25f);
        audioSource.Play();
        // Perform explosion animation.
        GetComponent<Animator>().SetTrigger("Explode");
        // Shake the Camera.
        camAnim.SetTrigger("Shake");
        // Ensure the bullet will not hit anything else and destroy.
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 2.1f);
    }

    /*
    * Destroy the bullet.
    * Used in GameManager while destroying all bullets.
    */
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
