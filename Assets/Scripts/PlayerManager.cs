using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    //[SerializeField] private List<LayerMask> playerLayers;
    [SerializeField] private List<Transform> startingPoints;
    [SerializeField] private Tilemap map;
    [SerializeField] private List<Texture2D> sprites;
    [SerializeField] private GameObject[] banners;
    [SerializeField] private Image[] setupSprites;
    [SerializeField] private TextMeshProUGUI[] setupLabels;

    private List<Vector3> spawnLocation = new List<Vector3>();
    private List<PlayerInput> players = new List<PlayerInput>();
    private PlayerInputManager playerInputManager;
    private string scene;


    // Offsets the worldPosition to the center of the Isometric grid.
    private float yOffset = 0.25f;
    //private float xOffset = 0.5f;

    private int count = 1;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene().name;
        playerInputManager = FindObjectOfType<PlayerInputManager>();

        // Loop through spawnpoints
        foreach (Transform spawnPoint in startingPoints)
        {
            // Get the cell position of the spawn point.
            Vector3Int cellPosition = map.WorldToCell(spawnPoint.position);

            // Get the world position of the cell position.
            Vector3 worldPosition = map.CellToWorld(cellPosition);

            // Spawning alignment
            float offsetX = worldPosition.x;
            float offsetY = worldPosition.y + yOffset;

            worldPosition = new Vector3(offsetX, offsetY, 1f);

            spawnLocation.Add(worldPosition);
        }
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        if(scene == "Tutorial" && players.Count < 1)
        {
            player.name = "Player" + count++;
            players.Add(player);

            // Set Player spawn point.
            player.transform.position = spawnLocation[players.Count - 1];
        }
        else if(players.Count < sprites.Count && scene != "Tutorial")
        {
            // Activate Player banner.
            banners[count - 1].SetActive(true);

            player.name = "Player" + count++;
            players.Add(player);

            // Change Player sprite.
            Sprite sprite = Sprite.Create(sprites[players.Count - 1], new Rect(0, 0, sprites[players.Count - 1].width, sprites[players.Count - 1].height), new Vector2(0.5f, 0.5f));
            SpriteRenderer playerSprite = player.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            playerSprite.sprite = sprite;

            // Set Player spawn point.
            player.transform.position = spawnLocation[players.Count - 1];
            GameManager.AddPlayers(player.gameObject);

            // Adjusting SetupScroll alpha values
            Color currentColor = setupSprites[players.Count - 1].color;
            currentColor.a = 1f;
            setupSprites[players.Count - 1].color = currentColor;

            currentColor = setupLabels[players.Count - 1].color;
            currentColor.a = 1f;
            setupLabels[players.Count - 1].color = currentColor;
        }
        else
        {
            Destroy(player.gameObject);
        }
    }
}
