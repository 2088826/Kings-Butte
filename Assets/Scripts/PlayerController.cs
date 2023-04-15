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
    [SerializeField] GameObject sampleTile;

    List<GameObject> tiles;

    float tileWidth;
    float tileHeight;

    

    void Start()
    {
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

            if (!sampleTile)
            {
                sampleTile = tileSet;
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
                // Set a target according to the tileIndex
                target = tiles[currentTileIndex];
            }

            
            if (target)
            {
                // Start at the target position.
                transform.position = target.transform.position;
            }

            GetTiles2();
        }


        if (tiles.Count > 0)
        {
            target = tiles[currentTileIndex];
        }
        
        if (target)
        {
            MoveToTile(target);
        }
    }

    private void MoveToTile(GameObject target)
    {
        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
    }
    private void FindAdjacentTiles(GameObject tile)
    {
        List<GameObject> adjacentTiles = new List<GameObject>();
        
        if (tileSet != null)
        {
            foreach (GameObject i in tiles)
            {
                Vector2 distance = i.transform.position - gameObject.transform.position;

                if(false)
                {

                }
            }

        }
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

        Collider2D[] xColliders = Physics2D.OverlapBoxAll(transform.position, xBox, 45);
        Collider2D[] yColliders = Physics2D.OverlapBoxAll(transform.position, yBox, 63);

        List<GameObject> xTiles = new List<GameObject>();
        List<GameObject> yTiles = new List<GameObject>();

        Debug.Log("x=" + xColliders.Length.ToString());
        Debug.Log("y=" + yColliders.Length.ToString());

        foreach (Collider2D coll in xColliders)
        {
            xTiles.Add(coll.gameObject);
        }

        foreach (Collider2D coll in yColliders)
        {
            yTiles.Add(coll.gameObject);
        }

        GameObject up = new GameObject();
        GameObject down = new GameObject();
        GameObject right = new GameObject();
        GameObject left = new GameObject();


        up = yTiles[0];
        down = yTiles[0];
        right = xTiles[0];
        left = xTiles[0];

        foreach (GameObject tile in yTiles)
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

        foreach (GameObject tile in xTiles)
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



        Debug.Log("Up=" + up.gameObject.name);
        Debug.Log("Down=" + down.gameObject.name);
        Debug.Log("Right=" + right.gameObject.name);
        Debug.Log("Left=" + left.gameObject.name);

        MarkTile(up, "up");
        MarkTile(down, "down");
        MarkTile(left, "left");
        MarkTile(right, "right");

    }

    private void MarkTile(GameObject tile, string name)
    {
        tile.gameObject.name = name;
    }

}
