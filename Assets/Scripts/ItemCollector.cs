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
    [SerializeField] private float hasteMultiplier = 0.075f;

    private int speedCounter = 0;
    private float speedBuffTimer = 0;
    private AbilityCooldown cooldowns;
    private PlayerController controller;
    private float timeSinceLastBuffDecrease;
    private bool isFirst = true;

    private void Start()
    {
        cooldowns = GetComponentInParent<AbilityCooldown>();
        controller = GetComponentInParent<PlayerController>();
        timeSinceLastBuffDecrease = 0;
    }

    private void Update()
    {
        if(!GameManager.IsPaused)
        {
            if (speedBuffTimer > 0.01)
            {
                speedBuffTimer -= Time.deltaTime;

                timeSinceLastBuffDecrease += Time.deltaTime;

                if (timeSinceLastBuffDecrease >= hasteDuration)
                {
                    Debug.Log("Debuff decrease");
                    speedCounter--;
                    timeSinceLastBuffDecrease = 0f;
                }

                hasteParticles.Play();
                controller.ChangeMoveCooldown(speedCounter * hasteMultiplier);
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
        if (other.gameObject.CompareTag("Cooldown"))
        {
            other.gameObject.GetComponentInParent<Animator>().SetTrigger("pickup");
            cooldownParticles.Play();
            cooldowns.ResetCooldowns();
        }

        if (other.gameObject.CompareTag("Haste"))
        {
            Debug.Log("Faster");
            other.gameObject.GetComponentInParent<Animator>().SetTrigger("pickup");
            speedBuffTimer += hasteDuration;
            speedCounter++;
        }
    }
}
