using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class IceTile : TileVacancy
{

    float tileWidth;
    float tileHeight;

    GameObject left;
    GameObject right;
    GameObject up;
    GameObject down;



    private void Start()
    {
        tileWidth = GetComponent<SpriteRenderer>().size.x;
        tileHeight = GetComponent<SpriteRenderer>().size.y;

        up = new GameObject("AdjacentUp");
        up.gameObject.transform.parent = this.gameObject.transform;
        down = new GameObject("AdjacentDown");
        down.gameObject.transform.parent = this.gameObject.transform;
        right = new GameObject("AdjacentRight");
        right.gameObject.transform.parent = this.gameObject.transform;
        left = new GameObject("AdjacentLeft");
        left.gameObject.transform.parent = this.gameObject.transform;
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

    private void SlidePlayer(Collider2D other)
    {
        GetAdjacentTiles();

        Vector3 otherSpot = other.transform.position;
        bool higher = false;
        bool righter = false;

        GameObject target;


        if (transform.position.y < otherSpot.y)
        {
            higher = true;
        }

        if (transform.position.x < otherSpot.x)
        {
            righter = true;
        }

        if (righter && higher)
        {
            // From Up
            target = down;
        }
        else if (righter && !higher)
        {
            // From Right
            target = left;
        }
        else if (!righter && higher)
        {
            // From left
            target = right;
        }
        else
        {
            // From Down
            target = up;
        }

        if (target.GetComponent<TileVacancy>().Occupied)
        {
            target = gameObject;
        }
        else if (!target.name.Contains("Tile") || gameObject)
        {
            // Sets the target to null for pushing off the edge.
            target = null;
        }

        other.gameObject.transform.parent.GetComponent<PlayerController>().SetTarget(target);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.parent.tag == "Player")
        {
            SlidePlayer(other);
        }

    }

}
