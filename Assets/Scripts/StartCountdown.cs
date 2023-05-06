using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartCountdown : MonoBehaviour
{
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioClip[] clips;
    
    private TextMeshProUGUI startTimer;
    private int countdown = 3;

    private void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        startTimer = GetComponent<TextMeshProUGUI>();
    }

    public void Countdown()
    {
        if (countdown > 0)
        {
            sfxSource.PlayOneShot(clips[0]);
            startTimer.text = countdown.ToString();
            countdown--;
        }
        else
        {
            sfxSource.PlayOneShot(clips[1]);
            startTimer.text = "BEGIN";
            GameManager.IsStart = true;
        }
    }

    public void TurnOff()
    {
        this.gameObject.SetActive(false);
    }
}
