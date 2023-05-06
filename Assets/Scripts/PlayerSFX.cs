using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip[] clip;

    public void MoveSFX()
    {
        sfxSource.PlayOneShot(clip[0]);
    }

    public void JumpSFX()
    {
        sfxSource.PlayOneShot(clip[1]);
    }

    public void LandSFX()
    {
        sfxSource.PlayOneShot(clip[2]);
    }

    public void BasicAttackSFX()
    {
        sfxSource.PlayOneShot(clip[3]);
    }

    public void SlamSFX()
    {
        sfxSource.PlayOneShot(clip[4]);
    }
}

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }