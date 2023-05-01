using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] float timeLimit = 90f;

    private bool startGame = true;
    private bool endGame = false;
    private bool first = true;


    void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (!startGame && !endGame && first)
        {
            Countdown();
        }
        else
        {
            first = false;
            EndGame();
        }

    }

    // Timer countdown
    private void Countdown()
    {
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;

            timer.text = (timeLimit).ToString("0");

        }
        else if (timeLimit < 0)
        {
            endGame = true;
        }
    }

    // Start the game.
    private void StartGame()
    {

    }

    // End the game.
    private void EndGame()
    {

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
