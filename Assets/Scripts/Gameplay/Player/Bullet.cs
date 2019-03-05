using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public AudioClip[] audioClips;
    private Animator camAnim;
    private AudioSource audioSource;
    private Rigidbody2D rb;

    private void Awake()
    {
        camAnim = FindObjectOfType<Camera>().GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = transform.up * 7.5f;
        Destroy(gameObject, 6f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "ScreenBoundary")
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ScreenBoundary")
            return;
        if (other.tag == "Player")
        {
            PlayerManager pm = other.GetComponent<PlayerManager>();
            if (pm.canBeHit)
            {
                StartCoroutine(pm.Hit());
                GetComponent<SpriteRenderer>().color = pm.color;
            }
            else
                return;
        }
        if (other.tag == "Bullet")
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 1f);
        }
        Explode();
    }

    private void Explode()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.pitch = Random.Range(.75f, 1.25f);
        audioSource.Play();
        GetComponent<Animator>().SetTrigger("Explode");
        camAnim.SetTrigger("Shake");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 2.1f);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
