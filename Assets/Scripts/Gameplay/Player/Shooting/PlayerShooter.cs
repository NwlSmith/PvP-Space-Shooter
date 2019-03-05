using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerShooter : MonoBehaviour {

    public GameObject bullet;
    protected AudioSource audioSource;
    protected PlayerManager pm;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        pm = GetComponentInParent<PlayerManager>();
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown("space"))
            Fire();
    }

    public virtual void Fire()
    {
        if (!GameManager.instance.paused && pm.canShoot)
        {
            audioSource.pitch = Random.Range(.20f, .25f);
            audioSource.Play();
            InstantiateBullet();
        }
    }

    protected virtual void InstantiateBullet()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }

}
