using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject tileSet;
    [SerializeField] float speed = 2;
    [SerializeField] GameObject target;
    [SerializeField] int currentTileIndex = 7;

    [SerializeField] float moveCooldown = 1f;
    
    bool moving = false; // Not used... yet

    float nextMoveTime = 0f;

    List<GameObject> tiles;

    float tileWidth;
    float tileHeight;


    // Holds the adjacent tiles
    GameObject up;
    GameObject down;
    GameObject right;
    GameObject left;


    void Start()
    {

        up = new GameObject();
        down = new GameObject();
        right = new GameObject();
        left = new GameObject();

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

            if (!target)
            {
                // Initialize a target according to the tileIndex
                target = tiles[currentTileIndex];
            }


            if (target)
            {
                // Start at the target position.
                transform.position = target.transform.position;
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
        if (true)
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
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && Time.time > nextMoveTime)
        {
            GetAdjacentTiles();

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
        up = yTiles[0];
        down = yTiles[0];
        right = xTiles[0];
        left = xTiles[0];

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


        // Used to Identify adjacent tiles from editor. No longer needed.
        /*MarkTile(up, "up");
        MarkTile(down, "down");
        MarkTile(left, "left");
        MarkTile(right, "right");*/

    }

    private void MarkTile(GameObject tile, string name)
    {
        tile.gameObject.name = name;
    }

    /// <summary>
    /// Sets the target to an adjacent tile according to the input.
    /// </summary>
    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0)
        {
            if(horizontal > 0)
            {
                // Move Right
                target = right;
                Debug.Log("RIGHT");
            }
            else
            {
                // Move Left
                target = left;
                Debug.Log("LEFT");
            }
        }
        else if(vertical != 0)
        {
            if (vertical > 0)
            {
                // Move Up
                target = up;
                Debug.Log("UP");
            }
            else
            {
                // Move Down
                target = down;
                Debug.Log("DOWN");
            }
        }
    }

}
