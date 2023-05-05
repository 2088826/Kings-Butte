using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static UnityEditor.Searcher.SearcherWindow.Alignment;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] GameObject target;

    [SerializeField] float moveCooldown = 1f;
    [SerializeField] float pushedCooldown = 0.15f;


    //bool moving = false; // Not used... yet
    //private bool _isAbility = false;
	bool targetOn = true;

    float nextMoveTime = 0f;

    GameObject tileSet;
    List<GameObject> tiles;

    float tileWidth;
    float tileHeight;
    
    // Holds the adjacent tiles
    GameObject left;
    GameObject right;
    GameObject up;
    GameObject down;

    private PlayerActions input;
    private Health health;
    
    void Start()
    {

        input = gameObject.GetComponent<PlayerActions>();
        health = gameObject.GetComponent<Health>();

        up = new GameObject("AdjacentUp");
        up.gameObject.transform.parent = this.gameObject.transform;
        down = new GameObject("AdjacentDown");
        down.gameObject.transform.parent = this.gameObject.transform;
        right = new GameObject("AdjacentRight");
        right.gameObject.transform.parent = this.gameObject.transform;
        left = new GameObject("AdjacentLeft");
        left.gameObject.transform.parent = this.gameObject.transform;

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
        //Debug.Log(input.Move);
        if (input.Move.inProgress && Time.time > nextMoveTime)
        {
            //GetAdjacentTiles();
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
        left = xTiles[0]; // up
        right = xTiles[0]; // down
        up = yTiles[0]; // right
        down = yTiles[0]; //left

        // Determines the Top and Bottom tiles.
        foreach (GameObject tile in yTiles)
        {
            // Only tile objects count
            if (tile.tag == "Tile")
            {
                if (tile.transform.position.y > left.transform.position.y)
                {
                    left = tile;
                }

                if (tile.transform.position.y < right.transform.position.y)
                {
                    right = tile;
                }
            }

        }

        // Determines the Left and Right tiles.
        foreach (GameObject tile in xTiles)
        {
            // Only tile objects count
            if (tile.tag == "Tile")
            {
                if (tile.transform.position.x > up.transform.position.x)
                {
                    up = tile;
                }

                if (tile.transform.position.x < down.transform.position.x)
                {
                    down = tile;
                }
            }
        }

    }

    /// <summary>
    /// Returns true if the tile is unoccupied.
    /// </summary>
    /// <remarks>
    /// Checks the tile's <see cref="TileVacancy"/> script and returns the value of its Occupied property.
    /// </remarks>
    /// <param name="tile"></param>
    /// <returns></returns>
    private bool CheckForVacancy(GameObject tile)
    {
        try
        {
            TileVacancy vacancyScript = tile.GetComponent<TileVacancy>();

            if (!vacancyScript.Occupied)
            {
                return true;
            }
            else
            {
                return false;
            }


        } catch
        {
            return false;
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
        left = xTiles[0]; // up
        right = xTiles[0]; // down
        up = yTiles[0]; // right
        down = yTiles[0]; //left

        // Determines the Top and Bottom tiles.
        foreach (GameObject tile in yTiles)
        {
            // Only tile objects count
            if (tile.tag == "Tile")
            {
                if (tile.transform.position.y > left.transform.position.y)
                {
                    left = tile;
                }

                if (tile.transform.position.y < right.transform.position.y)
                {
                    right = tile;
                }
            }
        }

        // Determines the Left and Right tiles.
        foreach (GameObject tile in xTiles)
        {
            // Only tile objects count
            if (tile.tag == "Tile")
            {
                if (tile.transform.position.x > up.transform.position.x)
                {
                    up = tile;
                }

                if (tile.transform.position.x < down.transform.position.x)
                {
                    down = tile;
                }
            }
        }

    }

    /// <summary>
    /// Sets the target to an adjacent tile according to the input.
    /// </summary>
    private void MovePlayer()
    {
        GetAdjacentTiles();

        float horizontal = input.Move.ReadValue<Vector2>().x;
        float vertical = input.Move.ReadValue<Vector2>().y;

        if (horizontal != 0)
        {
            if (horizontal > 0)
            {
                // Move Right
                SetTarget(right);
                //Debug.Log("Right");
            }
            else
            {
                // Move Left
                SetTarget(left);
                //Debug.Log("Left");
            }
        }
        else if (vertical != 0)
        {
            if (vertical > 0)
            {
                // Move Up
                SetTarget(up);
                //Debug.Log("Up");
            }
            else
            {
                // Move Down
                SetTarget(down);
                //Debug.Log("Down");
            }
        }
    }
        
    /// <summary>
    /// Moves the player 1 space in the given direction.
    /// </summary>
    public void MovePlayer(string direction)
    {
        GetAdjacentTiles();

        if (direction == "right")
        {
            // Move Right
            SetTarget(right);
            Debug.Log("Right");
        }
        else if (direction == "left")
        {
            // Move Left
            SetTarget(left);
            Debug.Log("Left");
        }
        else if (direction == "up")
        {
            // Move Up
            SetTarget(up);
            Debug.Log("Up");
        }
        else if (direction == "down")
        {
            // Move Down
            SetTarget(down);
            Debug.Log("Down");
        }
    }
    /// <summary>
    /// To be called when the object is pushed.
    /// </summary>
    private void OnPushed()
    {
        targetOn = false;

        SetTarget(GetCurrentTile());
        ActivateTarget();
        
    }

    private void ActivateTarget()
    {
        targetOn = true;
    }

    /// <summary>
    /// Sets the target to the given tile.
    /// </summary>
    /// <param name="tile"></param>
    public void SetTarget(GameObject tile)
    {
        target = tile;

        if (target == null)
        {
            health.Fall();
        }
        else if (!CheckForVacancy(tile))
        {
            target = (GetCurrentTile());
        }
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
            SetTarget(down);
        }
        else if(direction == "North")
        {
            SetTarget(up);
        }
        else if (direction == "East")
        {
            SetTarget(right);
        }
        else if (direction == "West")
        {
            SetTarget(left);
        }

    }

    /// <summary>
    /// Returns the gameObject of the tile the object is currently on.
    /// </summary>
    /// <returns></returns>
    public GameObject GetCurrentTile()
    {
        
        // Initialize as null
        GameObject currentTile = null;
        
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
                Debug.Log("Current tile identified.");
                break;
            }
        }

        return currentTile;

    }


    /// <summary>
    /// The move cooldown changes based on the multiplier.
    /// </summary>
    /// <param name="multiplier"></param>
    public void ChangeMoveCooldown(float multiplier)
    {
        moveCooldown = 1 * multiplier;
    }



    private void OnCollisionExit2D(Collision2D other)
    {

        if(other.gameObject.name.Contains("Player") == true && input.IsAbility == false)
        {
            OnPushed();

            Debug.Log(gameObject.name + " is pushed by " + other.gameObject.name);
        }
    }
}
