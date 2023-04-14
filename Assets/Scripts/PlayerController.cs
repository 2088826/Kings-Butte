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

    List<GameObject> tiles;

    float tileWidth;
    float tileHeight;

    
    
    
    
    
    
    void Start()
    {
        tiles = new List<GameObject>();

        if (tileSet != null)
        {
            foreach (Transform tile in tileSet.GetComponentInChildren<Transform>())
            {
                tiles.Add(tile.gameObject);
            }

            tileWidth = tiles[0].GetComponent<SpriteRenderer>().size.x;
            tileHeight = tiles[0].GetComponent<SpriteRenderer>().size.y;
        }

        // Start at the target position.
        if(target)
        {
            transform.position = target.transform.position;
        }

        GetTiles2();
    }

    void Update()
    {
        if (tiles.Count > 0)
        {
            target = tiles[currentTileIndex];
        }
        
        if (target )
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
        Vector2 xBox = new Vector2(tileWidth*2f, 0.1f);
        Vector2 yBox = new Vector2(0.1f, tileHeight*2f);

        Collider2D[] xColliders = Physics2D.OverlapBoxAll(transform.position, xBox, tileSet.transform.rotation.z);
        Collider2D[] yColliders = Physics2D.OverlapBoxAll(transform.position, yBox, tileSet.transform.rotation.z);

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


        up = xTiles[0];
        down = xTiles[0];
        right = yTiles[0];
        left = yTiles[0];

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

    }

}
