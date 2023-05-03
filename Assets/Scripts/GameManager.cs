using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI gameTimer;
    [SerializeField] private float timeLimit = 90f;
    [SerializeField] private TextMeshProUGUI startTimer;

    private bool startGame = true;
    private bool endGame = false;
    private bool first = true;
    private int playerCount = 0;

    public bool IsStart { get { return startGame; } set { startGame = value; } }

    void Start()
    {
        gameTimer.text = (timeLimit).ToString("0");
        StartGame();
    }

    private void Update()
    {
        playerCount = GameObject.FindGameObjectsWithTag("Player").Length;

        if (!startGame && !endGame)
        {
            TimerCountdown();
        }
        else if(endGame && first)
        {
            first = false;
            EndGame();
        }

    }

    // Timer countdown
    private void TimerCountdown()
    {
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;

            gameTimer.text = (timeLimit).ToString("0");

        }
        else if (timeLimit < 0)
        {
            endGame = true;
        }
    }

    // Start the game.
    private void StartGame()
    {
        Invoke("StartTimer", 1.1f);
    }

    // End the game.
    private void EndGame()
    {

    }

    private void StartTimer()
    {
        startTimer.gameObject.SetActive(true);
    }

    private void HandlePause()
    {
        pauseMenu.SetActive(true);
    }

    private void HandleResume()
    {
        pauseMenu.SetActive(false);
    }
}
