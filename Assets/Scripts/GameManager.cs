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
    [SerializeField] private GameObject winnerScroll;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private InputActionAsset inputAction;
    [SerializeField] private GameObject tieScroll;
    [SerializeField] private GameObject setupScroll;
    [SerializeField] private GameObject fireworks;
    [SerializeField] private GameObject pauseMessage;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip[] music;
    [SerializeField] private AudioClip[] sfx;
    
    // Private Fields
    private InputActionMap uiActionMap;
    private static bool isSetup = true;
    private static bool isStart = false;
    private bool isEnd = false;
    private static bool isPaused = false;
    private bool isFirst = true;
    private static Dictionary<string, GameObject> players;
    private ItemSpawner spawner;

    // Public Properties
    public static bool IsStart { get { return isStart; } set { isStart = value; } }
    public static bool IsPaused { get { return isPaused; } set { isPaused = value; } }

    public static bool IsSetup { get { return IsSetup; } }

    void Start()
    {
        spawner = GetComponent<ItemSpawner>();
        players = new Dictionary<string, GameObject>();
        uiActionMap = inputAction.FindActionMap("UI");
        isStart = false;
        isPaused = false;
        isSetup = true;
        gameTimer.text = timeLimit.ToString("0");
        GameSetup();
        musicSource.clip = music[0];
        musicSource.Play();
    }

    private void Update()
    {
        if (isSetup)
        {
            if(!setupScroll.activeSelf)
            {
                Invoke("SetSetup", 1f);
            }

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
                message.text = "CONTINUE JOINING OR PRESS START WHEN READY";
                spawner.enabled = true;
            }

            if (uiActionMap["Start"].triggered)
            {
                uiActionMap.Disable();
                isSetup = false;
                isFirst = true;
                setupScroll.GetComponent<Animator>().SetTrigger("close");
                musicSource.Stop();
            }
        }
    }

    /// <summary>
    /// Start the Buttening!
    /// </summary>
    private void StartGame()
    {
        gameTimer.gameObject.SetActive(true);

        TimerCountdown();

        if (isFirst)
        {
            isFirst = false;
            musicSource.clip = music[1];
            musicSource.Play();
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
        EnableUI();
        if(players.Count > 1) 
        {
            musicSource.Stop();
            musicSource.clip = music[3];
            musicSource.Play();
            Invoke("SetTie", 2f);
        }
        else
        {
            fireworks.SetActive(true);
            musicSource.Stop();
            musicSource.clip = music[2];
            musicSource.loop = false;
            musicSource.Play();
            Invoke("SetWinner", 2f);
        }
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    private void HandlePause()
    {
        DisablePlayers();
        pauseMessage.SetActive(true);
        uiActionMap.Enable();
        musicSource.Pause();
    }

    /// <summary>
    /// Resume the game.
    /// </summary>
    private void HandleResume()
    {
        isPaused = false;
        EnablePlayers();
        pauseMessage.SetActive(false);
        uiActionMap.Disable();
        musicSource.Play();
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

    private void EnableUI()
    {
        uiActionMap.Enable();
        Debug.Log(uiActionMap.actions);
        Debug.Log(uiActionMap.enabled);
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

    private void SetSetup()
    {
        setupScroll.SetActive(true);
    }
}
