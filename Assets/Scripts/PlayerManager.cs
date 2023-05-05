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
    [SerializeField] private GameObject[] banners;

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

            worldPosition = new Vector3(offsetX, offsetY, 0f);


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
        if(players.Count < sprites.Count)
        {

            banners[count - 1].SetActive(true);
            player.name = "Player" + count++;
            players.Add(player);

            Sprite sprite = Sprite.Create(sprites[players.Count - 1], new Rect(0, 0, sprites[players.Count - 1].width, sprites[players.Count - 1].height), new Vector2(0.5f, 0.5f));
            SpriteRenderer playerSprite = player.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            playerSprite.sprite = sprite;
            player.transform.position = spawnLocation[players.Count - 1];
            GameManager.AddPlayers(player.gameObject);
        }
        else
        {
            Destroy(player.gameObject);
        }
    }
}
