using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI gameTimer;
    [SerializeField] private float timeLimit = 90f;
    [SerializeField] private TextMeshProUGUI startTimer;
    [SerializeField] private InputSystemUIInputModule uiModule;

    private bool isSetup = true;
    private static bool isStart = false;
    private bool isEnd = false;
    private static bool isPaused = false;
    private bool first = true;
    private static int playerCount = 0;

    public static int PlayerCount {get { return playerCount;} set { playerCount = value; } }

    public static bool IsStart { get { return isStart; } set { isStart = value; } }

    public static bool IsPaused { get { return isPaused; } set { isPaused = value; } }

    void Start()
    {
        playerCount = 0;
        isStart = false;
        isPaused = false;
        gameTimer.text = timeLimit.ToString("0");
        GameSetup();
    }

    private void Update()
    {
        if (isSetup)
        {
            Debug.Log("Setting up game...");

            if (playerCount >= 2)
            {
                uiModule.enabled = true;
            }

            if (InputSystem.FindControl("Enter").IsPressed())
            {
                Debug.Log("Yes");
            }
        }
        else if (isStart)
        {
            isStart = false;
            playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
            Debug.Log("Game Start!");
            TimerCountdown();



        }
        else if(isEnd || playerCount == 1)
        {
            isEnd = false;
            Debug.Log("Game End!");
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
            isEnd = true;
        }
    }

    // Start the game.
    private void GameSetup()
    {
        
    }

    private void StartGame()
    {
        Invoke("StartTimer", 1.1f);
    }

    // End the game.
    private void EndGame()
    {
        Debug.Log("FINISHED!");
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player != null)
            {
                
            }
        }

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
