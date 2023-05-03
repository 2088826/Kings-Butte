using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartCountdown : MonoBehaviour
{
    private TextMeshProUGUI startTimer;
    private int countdown = 3;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        startTimer = GetComponent<TextMeshProUGUI>();
    }

    public void Countdown()
    {
        if (countdown > 0)
        {
            startTimer.text = countdown.ToString();
            countdown--;
        }
        else
        {
            startTimer.text = "Start";
        }
    }

    public void TurnOff()
    {
        gameManager.IsStart = false;
        this.gameObject.SetActive(false);
    }
}
