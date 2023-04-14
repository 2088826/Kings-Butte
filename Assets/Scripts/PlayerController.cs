using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
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
    }

    void Update()
    {
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

}
