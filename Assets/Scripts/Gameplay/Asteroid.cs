using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Hazard {

    public AudioClip[] audioClips;

    private AudioSource audioSource;
    private Animator anim;
    private ParticleSystem explosionParticles;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.pitch = Random.Range(.5f, .55f);
        anim = GetComponent<Animator>();
        explosionParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        float scale = Random.Range(.25f, 1f);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerManager pm = other.GetComponent<PlayerManager>();
            if (pm.canBeHit)
                StartCoroutine(pm.Hit());
            else
                return;
        }
        Explode();
    }

    private void Explode()
    {
        audioSource.Play();
        Debug.Log("Explode");
        anim.SetTrigger("Explode");
        explosionParticles.Emit(100);
        col2D.enabled = false;
        Destroy(gameObject, 2.1f);
    }
}
