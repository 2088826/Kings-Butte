using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    //[SerializeField] private List<LayerMask> playerLayers;
    [SerializeField] private List<Transform> startingPoints;
    [SerializeField] private Tilemap map;
    [SerializeField] private List<Texture2D> sprites;

    private List<Vector3> spawnLocation = new List<Vector3>();
    private List<PlayerInput> players = new List<PlayerInput>();
    private PlayerInputManager playerInputManager;


    // Offsets the worldPosition to the center of the Isometric grid.
    private float yOffset = 0.25f;
    //private float xOffset = 0.5f;

    private int count = 1;


    private void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();

        // Get
        foreach (Transform spawnPoint in startingPoints)
        {
            // Get the cell position of the spawn point.
            Vector3Int cellPosition = map.WorldToCell(spawnPoint.position);

            Debug.Log(cellPosition);
            // Get the world position of the cell position.
            Vector3 worldPosition = map.CellToWorld(cellPosition);
            Debug.Log(worldPosition);

            // Spawning alignment
            float offsetX = worldPosition.x;
            float offsetY = worldPosition.y + yOffset;

            worldPosition = new Vector3(offsetX, offsetY, 0f);


            spawnLocation.Add(worldPosition);
        }
    }

    private void OnEnable()
    {
        Debug.Log("Enabled");
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        Debug.Log("Disabled");
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        if(players.Count < sprites.Count)
        {
            Debug.Log(players.Count);
            Debug.Log(sprites.Count);
            SpriteRenderer playerSprite = player.transform.Find("Sprite").GetComponent<SpriteRenderer>();

            player.name = "Player " + count++;
            players.Add(player);
            Sprite sprite = Sprite.Create(sprites[players.Count - 1], new Rect(0, 0, sprites[players.Count - 1].width, sprites[players.Count - 1].height), new Vector2(0.5f, 0.5f));
            playerSprite.sprite = sprite;
            player.transform.position = spawnLocation[players.Count - 1];
            Debug.Log("PlayerAdded");
        }
        else
        {
            Destroy(player.gameObject);
        }
    }
}