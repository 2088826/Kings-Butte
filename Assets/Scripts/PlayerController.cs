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

        int count = 0;

        if (tileSet != null)
        {
            foreach (Transform tile in tileSet.GetComponentInChildren<Transform>())
            {
                count++;
                //tiles.Add(tile.gameObject);
                //Debug.Log(count);
            }

        }

        // Start at the target position.
        if(target)
        {
            transform.position = target.transform.position;
        }

        
    }

    void Update()
    {

        // Placed in update because it does not work on start
        // Get the tiles and put them in the tiles List
        if (tiles.Count <= 0 && tileSet != null)
        {

            foreach (Transform tile in tileSet.GetComponentInChildren<Transform>())
            {
                
                tiles.Add(tile.gameObject);
            }

            
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

            //GetTiles2();
        }


        // Changes the target to reflect the currentTileIndex
        /*if (tiles.Count > 0)
        {
            target = tiles[currentTileIndex];
        }*/

        // Move to the target
        if (target)
        {
            MoveToTile(target);
        }

        //GetTiles2();
    }

    private void FixedUpdate()
    {
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Horizontal") != 0) && Time.time > nextMoveTime)
        {
            GetTiles2();

            MovePlayer();

            nextMoveTime = Time.time + moveCooldown;

        }
    }

    private void MoveToTile(GameObject target)
    {
        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
    }

    private void GetTiles()
    {
        Collider2D[] adjacentColliders = Physics2D.OverlapCircleAll(transform.position, tileWidth);

        List<GameObject> adjacentTiles = new List<GameObject>();

        Debug.Log(adjacentColliders.Length.ToString());

        foreach (Collider2D coll in adjacentColliders)
        {
            adjacentTiles.Add(coll.gameObject);
        }

        foreach (GameObject tile in adjacentTiles)
        {
            Debug.Log(tile.transform.position.ToString());
        }

    }

    private void GetTiles2()
    {
        Vector2 xBox = new Vector2(tileWidth, tileHeight * 0.0001f);
        Vector2 yBox = new Vector2(tileWidth * 0.0001f, tileHeight);

        // Gets colliders
        Collider2D[] xColliders = Physics2D.OverlapBoxAll(transform.position, xBox, 45);
        Collider2D[] yColliders = Physics2D.OverlapBoxAll(transform.position, yBox, 63);

        List<GameObject> xTiles = new List<GameObject>();
        List<GameObject> yTiles = new List<GameObject>();

        //Debug.Log("x=" + xColliders.Length.ToString());
        //Debug.Log("y=" + yColliders.Length.ToString());

        foreach (Collider2D coll in xColliders)
        {
            xTiles.Add(coll.gameObject);
        }

        foreach (Collider2D coll in yColliders)
        {
            yTiles.Add(coll.gameObject);
        }

        


        up = yTiles[0];
        down = yTiles[0];
        right = xTiles[0];
        left = xTiles[0];

        foreach (GameObject tile in yTiles)
        {
            // TODO: Condition that the tile must have a tile tag.
            if(true)
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

        foreach (GameObject tile in xTiles)
        {
            // TODO: Condition that the tile must have a tile tag.
            if (true)
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



        MarkTile(up, "up");
        MarkTile(down, "down");
        MarkTile(left, "left");
        MarkTile(right, "right");

    }

    private void MarkTile(GameObject tile, string name)
    {
        tile.gameObject.name = name;
    }

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
