using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    [SerializeField] private float cooldown1 = 4f;
    [SerializeField] private float cooldown2 = 8f;
    [SerializeField] private ParticleSystem dust;

    private Health health;
    
    private Transform playerBanner;
    private Image abilityImage1;
    private Image abilityImage2;
    private Animator bannerAnim;
    private bool isCooldown1 = false;
    private bool isCooldown2 = false;
    private bool isAbility1 = false;
    private bool isAbility2 = false;

    public bool IsAbility1 { get { return isAbility1; } set { isAbility1 = value; } }
    public bool IsAbility2 { get { return isAbility2; } set { isAbility2 = value; } }

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        playerBanner = GameObject.Find(this.gameObject.name + "Banner").transform;

        bannerAnim = playerBanner.GetComponent<Animator>();

        abilityImage1 = playerBanner.Find("Ability1").Find("Cooldown1").GetComponent<Image>();
        abilityImage1.fillAmount = 0;

        abilityImage2 = playerBanner.Find("Ability2").Find("Cooldown2").GetComponent<Image>();
        abilityImage2.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.IsDefeated)
        {
            bannerAnim.SetTrigger("defeated");
        }
        Cooldown1();
        Cooldown2();
    }

    // Cool down animation for ability 1
    private void Cooldown1()
    {
        if (abilityImage1 != null)
        {
            if (isAbility1 && !isCooldown1)
            {
                isCooldown1 = true;
                abilityImage1.fillAmount = 1;
            }

            if (isCooldown1)
            {
                abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

                if (abilityImage1.fillAmount <= 0)
                {
                    abilityImage1.fillAmount = 0;
                    isCooldown1 = false;
                    isAbility1 = false;
                }
            }
        }
    }

    // Cooldown animation for ability 2
    private void Cooldown2()
    {
        if (abilityImage2 != null)
        {
            if (isAbility2 && !isCooldown2)
            {
                isCooldown2 = true;
                abilityImage2.fillAmount = 1;
            }

            if (isCooldown2)
            {
                abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

                if (abilityImage2.fillAmount <= 0)
                {
                    abilityImage2.fillAmount = 0;
                    isCooldown2 = false;
                    isAbility2 = false;
                }
            }
        }
    }

    // Reset Cooldowns
    public void ResetCooldowns()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
    }

    // Play dust
    public void PlayDust()
    {
        dust.Play();
    }
}
