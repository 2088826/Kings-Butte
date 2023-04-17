using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    //[SerializeField] private List<LayerMask> playerLayers;
    [SerializeField] private List<Transform> startingPoints;
    [SerializeField] private Tilemap map;

    private List<Vector3> spawnLocation = new List<Vector3>();
    private List<PlayerInput> players = new List<PlayerInput>();
    private PlayerInputManager playerInputManager;

    // Offsets the worldPosition to the center of the Isometric grid.
    private float yOffset = 0.25f;
    private float xOffset = 0.5f;

    private int count = 1;


    private void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();

        foreach (Transform spawnPoint in startingPoints)
        {
            // Get the cell position of the spawn point.
            Vector3Int cellPosition = map.WorldToCell(spawnPoint.position);

            // Get the world position of the cell position.
            Vector3 worldPosition = map.CellToWorld(cellPosition);

            // Calculate the offset for an isometric tilemap.
            float offsetX = map.cellSize.x / 2f;
            float offsetY = map.cellSize.y / 2f;

            // Add an offset to the world position based on the size of the tiles.
            worldPosition += new Vector3(offsetX - xOffset, offsetY + yOffset, 0f);

            spawnLocation.Add(worldPosition);
        }
    }

    private void OnEnable()
    {
        Debug.Log("Enabled");
        if(count < 5)
        {
            playerInputManager.onPlayerJoined += AddPlayer;
        }
    }

    private void OnDisable()
    {
        Debug.Log("Disabled");
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        Debug.Log("PlayerAdded");
        player.name = "Player " + count++;
        players.Add(player);

        // need to use the parent due to the structure of the prefab
        Transform playerParent = player.transform;
        playerParent.position = spawnLocation[players.Count - 1]; 
    }
}
