using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject tileSet;
    [SerializeField] float speed = 2;

    int currentTileIndex = 7;
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveToTile(GameObject target)
    {
        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
    }



}
