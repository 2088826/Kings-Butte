using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] GameObject target;
    [SerializeField] int currentTileIndex = 7;

    [SerializeField] float moveCooldown = 1f;
    [SerializeField] float pushedCooldown = 0.15f;


    bool moving = false; // Not used... yet
    private bool _isAbility = false;
	bool targetOn = true;

    float nextMoveTime = 0f;

    GameObject tileSet;
    List<GameObject> tiles;

    float tileWidth;
    float tileHeight;
    
    // Holds the adjacent tiles
    GameObject up;
    GameObject down;
    GameObject right;
    GameObject left;

    private InputManager input;
    
    void Start()
    {

        input = gameObject.GetComponent<InputManager>();

        up = new GameObject();
        down = new GameObject();
        right = new GameObject();
        left = new GameObject();

        tileSet = GameObject.Find("TileContainer");
        tiles = new List<GameObject>();
        
        if (tiles.Count <= 0 && tileSet != null)
        {

            // Adds all the tiles in the tileSet to the Tiles List.
            foreach (Transform tile in tileSet.GetComponentInChildren<Transform>())
            {
                tiles.Add(tile.gameObject);
            }


            // Gets the Width and Length of the tiles
            // *potentially replaceable*
            if (tiles.Count > 0)
            {
                tileWidth = tiles[0].GetComponent<SpriteRenderer>().size.x;
                tileHeight = tiles[0].GetComponent<SpriteRenderer>().size.y;
            }

            //if (!target)
            //{
            //    // Initialize a target according to the tileIndex
            //    target = tiles[currentTileIndex];
            //}


            //if (target)
            //{
            //    // Start at the target position.
            //    transform.position = target.transform.position;
            //}

            //GetAdjacentTiles();
        }


    }

    void Update()
    {

        // Changes the target to reflect the currentTileIndex
        /*if (tiles.Count > 0)
        {
            target = tiles[currentTileIndex];
        }*/


        // TODO: Make a "moving" bool... maybe
        if (targetOn == true)
        {
            // Move to the target
            if (target)
            {
                MoveToTile(target);
            }
        }

        //GetAdjacentTiles();
    }

    private void FixedUpdate()
    {
        Debug.Log(input.Move);
        if (input.Move != null && Time.time > nextMoveTime)
        {
            GetAdjacentTiles();
            //GetAdjacentTilesX2();

            MovePlayer();

            nextMoveTime = Time.time + moveCooldown;

        }
    }

    // Moves the player in the direction of the target.
    private void MoveToTile(GameObject target)
    {
        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
    }

    /// <summary>
    /// Identifies the adjacent tiles.
    /// </summary>
    /// <remarks>The angles are hardcoded.</remarks>
    private void GetAdjacentTiles()
    {
        // Dimensions of the OverlapBox
        Vector2 xBox = new Vector2(tileWidth, tileHeight * 0.0001f);
        Vector2 yBox = new Vector2(tileWidth * 0.0001f, tileHeight);

        // Gets colliders (HARDCODED ANGLES)
        Collider2D[] xColliders = Physics2D.OverlapBoxAll(transform.position, xBox, 45);
        Collider2D[] yColliders = Physics2D.OverlapBoxAll(transform.position, yBox, 63);

        // Lists to hold Horizontal and Vertical tile GameObjects.
        List<GameObject> xTiles = new List<GameObject>();
        List<GameObject> yTiles = new List<GameObject>();


        // Adds the GameObjects of the colliders to the list.
        foreach (Collider2D coll in xColliders)
        {
            xTiles.Add(coll.gameObject);
        }

        foreach (Collider2D coll in yColliders)
        {
            yTiles.Add(coll.gameObject);
        }

        // Initializes the values of the adjacent tiles.
        up = xTiles[0]; // up
        down = xTiles[0]; // down
        right = yTiles[0]; // right
        left = yTiles[0]; //left

        // Determines the Top and Bottom tiles.
        foreach (GameObject tile in yTiles)
        {
            // Only tile objects count
            if (tile.tag == "Tile")
            {
                if (tile.transform.position.y > up.transform.position.y)
                {
                    up = tile;
                }

                if (tile.transform.position.y < down.transform.position.y)
                {
                    down = tile;
                }
            }
        }

        // Determines the Left and Right tiles.
        foreach (GameObject tile in xTiles)
        {
            // Only tile objects count
            if (tile.tag == "Tile")
            {
                if (tile.transform.position.x > right.transform.position.x)
                {
                    right = tile;
                }

                if (tile.transform.position.x < left.transform.position.x)
                {
                    left = tile;
                }
            }
        }

    }

    /// <summary>
    /// Identifies the adjacent tiles 2 spaces away.
    /// </summary>
    /// <remarks>The angles are hardcoded.</remarks>
    private void GetAdjacentTilesX2()
    {
        // Dimensions of the OverlapBox
        Vector2 xBox = new Vector2(tileWidth*2, tileHeight * 0.0001f);
        Vector2 yBox = new Vector2(tileWidth * 0.0001f, tileHeight*4f);

        // Gets colliders (HARDCODED ANGLES)
        Collider2D[] xColliders = Physics2D.OverlapBoxAll(transform.position, xBox, 45);
        Collider2D[] yColliders = Physics2D.OverlapBoxAll(transform.position, yBox, 63);

        // Lists to hold Horizontal and Vertical tile GameObjects.
        List<GameObject> xTiles = new List<GameObject>();
        List<GameObject> yTiles = new List<GameObject>();


        // Adds the GameObjects of the colliders to the list.
        foreach (Collider2D coll in xColliders)
        {
            xTiles.Add(coll.gameObject);
        }

        foreach (Collider2D coll in yColliders)
        {
            yTiles.Add(coll.gameObject);
        }

        // Initializes the values of the adjacent tiles.
        up = xTiles[0]; // up
        down = xTiles[0]; // down
        right = yTiles[0]; // right
        left = yTiles[0]; //left

        // Determines the Top and Bottom tiles.
        foreach (GameObject tile in yTiles)
        {
            // Only tile objects count
            if (tile.tag == "Tile")
            {
                if (tile.transform.position.y > up.transform.position.y)
                {
                    up = tile;
                }

                if (tile.transform.position.y < down.transform.position.y)
                {
                    down = tile;
                }
            }
        }

        // Determines the Left and Right tiles.
        foreach (GameObject tile in xTiles)
        {
            // Only tile objects count
            if (tile.tag == "Tile")
            {
                if (tile.transform.position.x > right.transform.position.x)
                {
                    right = tile;
                }

                if (tile.transform.position.x < left.transform.position.x)
                {
                    left = tile;
                }
            }
        }

    }

    /// <summary>
    /// Sets the target to an adjacent tile according to the input.
    /// </summary>
    private void MovePlayer()
    {
        float horizontal = input.Move.ReadValue<Vector2>().x;
        float vertical = input.Move.ReadValue<Vector2>().y;

        if (horizontal != 0)
        {
            if (horizontal > 0)
            {
                // Move Right
                target = down;
                Debug.Log("Right");
            }
            else
            {
                // Move Left
                target = up;
                Debug.Log("Left");
            }
        }
        else if (vertical != 0)
        {
            if (vertical > 0)
            {
                // Move Up
                target = right;
                Debug.Log("Up");
            }
            else
            {
                // Move Down
                target = left;
                Debug.Log("Down");
            }
        }
    }
        
    /// <summary>
    /// To be called when the object is pushed.
    /// </summary>
    private void OnPushed()
    {
        targetOn = false;

        SetTargetToCurrentTile();
        ActivateTarget();


        //Invoke("SetTargetToCurrentTile", pushedCooldown);
        //Invoke("ActivateTarget", pushedCooldown);

        //target = GetCurrentTile();
        
    }

    private void ActivateTarget()
    {
        targetOn = true;
    }

    private void SetTargetToCurrentTile()
    {
        target = GetCurrentTile();
    }

    /// <summary>
    /// Moves the player 2 spaces in an adjacent direction
    /// </summary>
    /// <remarks>Repurposed. Formerly GetPushed.</remarks>
    /// <param name="transform"></param>
    public void Move2Adjacent(string direction)
    {
        GetAdjacentTilesX2();

        if (direction == "South")
        {
            target = down;
        }
        else if(direction == "North")
        {
            target = up;
        }
        else if (direction == "East")
        {
            target = right;
        }
        else if (direction == "West")
        {
            target = left;
        }

    }

    public GameObject GetCurrentTile()
    {
        
        // Initialize with the target
        GameObject currentTile = target;
        
        // Gets colliders
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.0001f);

        // Lists to hold Horizontal and Vertical tile GameObjects.
        List<GameObject> tile = new List<GameObject>();


        // Adds the GameObjects of the colliders to the list.
        foreach (Collider2D coll in colliders)
        {
           if (coll.gameObject.tag == "Tile")
            {
                currentTile = coll.gameObject;
                Debug.Log("Target reassigned");
                break;
            }
        }

        return currentTile;

    }

    private void OnCollisionExit2D(Collision2D other)
    {

        if(other.gameObject.name.Contains("Player") == true && input.IsAbility == false)
        {
            OnPushed();

            Debug.Log(gameObject.name + " detects " + other.gameObject.name);
        }
    }
}
