using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

public class ItemController : MonoBehaviour
{
    [SerializeField] private AudioClip[] clip;

    private AudioSource sfxSource;
    private Animator anim;


    private void Start()
    {
        sfxSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void SetIdle()
    {
        sfxSource.PlayOneShot(clip[0]);
        anim.SetTrigger("idle");
    }

    public void PickUp()
    {
        sfxSource.PlayOneShot(clip[1]);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
