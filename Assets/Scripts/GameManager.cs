using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class GameManager : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] private TextMeshProUGUI gameTimer;
    [SerializeField] private float timeLimit = 90f;
    [SerializeField] private TextMeshProUGUI startTimer;
    [SerializeField] private TextMeshProUGUI[] messages;
    [SerializeField] private InputActionAsset inputAction;
    
    // Private Fields
    private InputActionMap uiActionMap;
    private bool isSetup = true;
    private static bool isStart = false;
    private bool isEnd = false;
    private static bool isPaused = false;
    private bool first = true;
    private static Dictionary<string, GameObject> players;

    // Public Properties
    public static bool IsStart { get { return isStart; } set { isStart = value; } }
    public static bool IsPaused { get { return isPaused; } set { isPaused = value; } }

    void Start()
    {
        players = new Dictionary<string, GameObject>();
        uiActionMap = inputAction.FindActionMap("UI");
        isStart = false;
        isPaused = false;
        gameTimer.text = timeLimit.ToString("0");
        GameSetup();
    }

    private void Update()
    {
        if (isSetup)
        {
            GameSetup();
        }
        else if (isStart)
        {
            StartGame();
        }
        else if (isEnd || players.Count == 1)
        {
            Debug.Log("Ending Game...");
            EndGame();
        }

        if (isPaused)
        {
            HandlePause();

            if (uiActionMap["Start"].triggered)
            {
                HandleResume();
            }
        }
    }

    /// <summary>
    /// Game time countdown.
    /// </summary>
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

    /// <summary>
    /// Setup up the game allowing players to join. *Minimum 2 players to start*
    /// </summary>
    private void GameSetup()
    {
        if (players.Count >= 2)
        {
            if (first)
            {
                first = false;
                uiActionMap.Enable();
                messages[0].text = "PRESS START TO BEGIN";
                messages[1].gameObject.SetActive(false);
            }

            if (uiActionMap["Start"].triggered)
            {
                uiActionMap.Disable();
                isSetup = false;
                first = true;
                messages[0].gameObject.SetActive(false);
                StartTimer();
            }
        }
    }

    /// <summary>
    /// Start the Buttening!
    /// </summary>
    private void StartGame()
    {
        TimerCountdown();

        if (first)
        {
            first = false;
            EnablePlayers();
        }
    }

    /// <summary>
    /// End the game.
    /// </summary>
    private void EndGame()
    {
        first = false;
        isStart = false;
        isEnd = false;
        DisablePlayers();
        messages[0].gameObject.SetActive(true);
        messages[0].text = "Game Over";
    }

    /// <summary>
    /// Start game timer.
    /// </summary>
    private void StartTimer()
    {
        startTimer.gameObject.SetActive(true);
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    private void HandlePause()
    {
        DisablePlayers();
        messages[0].gameObject.SetActive(true);
        messages[0].text = "Paused";
        uiActionMap.Enable();
    }

    /// <summary>
    /// Resume the game.
    /// </summary>
    private void HandleResume()
    {
        isPaused = false;
        EnablePlayers();
        messages[0].gameObject.SetActive(false);
        uiActionMap.Disable();
    }

    /// <summary>
    /// Disable all players inputs.
    /// </summary>
    private void DisablePlayers()
    {
        foreach (KeyValuePair<string, GameObject> player in players)
        {
            player.Value.GetComponent<PlayerActions>().ToggleDisable();
        }
    }

    /// <summary>
    /// Enable all players inputs.
    /// </summary>
    private void EnablePlayers()
    {
        foreach (KeyValuePair<string, GameObject> player in players)
        {
            player.Value.GetComponent<PlayerActions>().TogglePlayer();
        }
    }

    /// <summary>
    /// Add Players to the roster
    /// </summary>
    /// <param name="player">Player GameObject to be add to the list.</param>
    public static void AddPlayers(GameObject player)
    {
        players.Add(player.name, player);
    }

    /// <summary>
    /// Remove Players from the roster
    /// </summary>
    /// <param name="player">Player GameObject to be removed from the list.</param>
    public static void RemovePlayers(GameObject player)
    {
        players.Remove(player.name);
    }
}
