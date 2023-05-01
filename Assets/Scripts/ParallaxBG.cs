using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    
    private float startingPosition;
    private float imageWidth;

    // Start is called before the first frame update
    void Start()
    {
        imageWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startingPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (-speed * Time.deltaTime), transform.position.y, transform.position.z);

        if(transform.position.x < startingPosition - imageWidth)
        {
            transform.position = transform.position + new Vector3(startingPosition + imageWidth, 0, 0);
        }
    }
}
