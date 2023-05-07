using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] private TextMeshProUGUI gameTimer;
    [SerializeField] private float timeLimit = 90f;
    [SerializeField] private TextMeshProUGUI startTimer;
    [SerializeField] private TextMeshProUGUI[] messages;
    [SerializeField] private InputActionAsset inputAction;
    [SerializeField] private GameObject winnerScroll;
    [SerializeField] private GameObject tieScroll;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip[] music;
    [SerializeField] private AudioClip[] sfx;
    
    // Private Fields
    private InputActionMap uiActionMap;
    private bool isSetup = true;
    private static bool isStart = false;
    private bool isEnd = false;
    private static bool isPaused = false;
    private bool isFirst = true;
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
        else if (isEnd)
        {
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
        if (timeLimit > 0 && !isPaused)
        {
            timeLimit -= Time.deltaTime;

            gameTimer.text = timeLimit.ToString("0");

        }
        else if (timeLimit <= 0)
        {
            isStart = false;
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
            if (isFirst)
            {
                isFirst = false;
                uiActionMap.Enable();
                messages[0].text = "PRESS START TO BEGIN";
                messages[1].gameObject.SetActive(false);
            }

            if (uiActionMap["Start"].triggered)
            {
                uiActionMap.Disable();
                isSetup = false;
                isFirst = true;
                messages[0].gameObject.SetActive(false);
                Invoke("StartTimer", 0.5f);
            }
        }
    }

    /// <summary>
    /// Start the Buttening!
    /// </summary>
    private void StartGame()
    {
        TimerCountdown();

        if (isFirst)
        {
            isFirst = false;
            EnablePlayers();
        }

        if (players.Count == 1)
        {
            isStart = false;
            isEnd = true;
        }
    }

    /// <summary>
    /// End the game.
    /// </summary>
    private void EndGame()
    {
        isEnd = false;
        DisablePlayers();
        if(players.Count > 1) 
        {
            Invoke("SetTie", 2f);
        }
        else
        {
            Invoke("SetWinner", 2f);
        }
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

    private void SetWinner()
    {
        winnerScroll.SetActive(true);
        uiActionMap.Enable();

        GameObject winner = players.SingleOrDefault().Value;

        winnerScroll.transform.Find("VictoryMessage1").GetComponent<TextMeshProUGUI>().text = winner.name;

        SpriteRenderer spriteRenderer = winner.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        Sprite sprite = Sprite.Create(spriteRenderer.sprite.texture, new Rect(0, 0, spriteRenderer.sprite.texture.width, spriteRenderer.sprite.texture.height), Vector2.one / 2f, 100f);
        winnerScroll.transform.Find("PlayerSprite").GetComponent<Image>().sprite = sprite;
    }

    private void SetTie()
    {
        tieScroll.SetActive(true);
        uiActionMap.Enable();

    }
}
