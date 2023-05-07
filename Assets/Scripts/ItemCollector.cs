using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private ParticleSystem cooldownParticles;
    [SerializeField] private ParticleSystem hasteParticles;
    [SerializeField] private float hasteDuration = 10f;
    [SerializeField] private float multiplier = 0.05f;

    private int speedCounter = 0;
    private float speedBuffTimer = 0;
    private AbilityCooldown cooldowns;
    private PlayerController controller;
    private PlayerActions actions;
    private float timeSinceLastBuffDecrease;

    private HashSet<Collider2D> triggeredColliders = new HashSet<Collider2D>();

    private void Start()
    {
        cooldowns = GetComponentInParent<AbilityCooldown>();
        controller = GetComponentInParent<PlayerController>();
        actions = GetComponentInParent<PlayerActions>();
        timeSinceLastBuffDecrease = 0;
    }

    private void Update()
    {
        if(!GameManager.IsPaused)
        {
            if (speedBuffTimer > 0.01)
            {
                controller.ChangeMoveCooldown(speedCounter * multiplier);
                actions.ChangeMoveCooldown(speedCounter * multiplier);

                speedBuffTimer -= Time.deltaTime;

                timeSinceLastBuffDecrease += Time.deltaTime;

                if (timeSinceLastBuffDecrease >= hasteDuration)
                {
                    speedCounter--;
                    timeSinceLastBuffDecrease = 0f;
                }
            }
            else
            {
                speedBuffTimer = 0;
                speedCounter = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggeredColliders.Contains(other))
        {
            // Adds triggered colliders to a Hashset to prevent the same collider from being triggered twice.
            triggeredColliders.Add(other);

            if (other.gameObject.CompareTag("Cooldown"))
            {
                other.gameObject.GetComponentInParent<Animator>().SetTrigger("pickup");
                cooldownParticles.Play();
                cooldowns.ResetCooldowns();
            }

            if (other.gameObject.CompareTag("Haste"))
            {
                other.gameObject.GetComponentInParent<Animator>().SetTrigger("pickup");
                speedBuffTimer += hasteDuration;
                speedCounter++;
                hasteParticles.Play();
            }
        }
    }
}
