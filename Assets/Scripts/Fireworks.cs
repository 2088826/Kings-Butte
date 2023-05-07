using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    public AudioSource audioSource;
    public ParticleSystem ps;
    private int currentCount = 0;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        if (ps == null)
            Debug.LogError("Missing ParticleSystem!", this);
    }

    private void Update()
    {
        if (ps.particleCount < currentCount)
        {
            audioSource.Play();
        }


        currentCount = ps.particleCount;
    }
}
