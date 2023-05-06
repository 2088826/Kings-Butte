using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField]private ParticleSystem cooldownParticles;
    [SerializeField] private ParticleSystem hasteParticles;
    
    private AbilityCooldown cooldowns;
    private PlayerController controller;

    private void Start()
    {
        cooldowns = GetComponentInParent<AbilityCooldown>();
        controller = GetComponentInParent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cooldown"))
        {
            //other.gameObject.GetComponent<Animator>().SetTrigger("Pickup");
            cooldownParticles.Play();
            cooldowns.ResetCooldowns();
        }

        if (other.gameObject.CompareTag("Haste"))
        {
            //other.gameObject.GetComponent<Animator>().SetTrigger("Pickup");
            hasteParticles.Play();

            //TODO HASTE FOR X AMOUNT OF TIME
        }
    }
}