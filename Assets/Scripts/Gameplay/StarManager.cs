using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour {

    public PlayerManager pm;
    private ParticleSystem stars;
    private ParticleSystem.MainModule main;

    private void Awake()
    {
        stars = GetComponent<ParticleSystem>();
        main = stars.main;
    }

    private void FixedUpdate()
    {
        if (pm.rb.velocity != Vector2.zero)
        {
            stars.Play();
            AdjustSpeed();
        }
        else if (pm.rb.velocity == Vector2.zero && stars.isPlaying)
        {
            stars.Pause();
            main.simulationSpeed = 0f;
        }

    }

    private void AdjustSpeed()
    {
        main.simulationSpeed = pm.rb.velocity.magnitude / pm.playerMovement.m_maxSpeed; ;
    }
}
