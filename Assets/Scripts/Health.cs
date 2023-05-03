using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    int hp = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hp == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Destroy(gameObject);

        gameObject.SetActive(false);
    }

    public void Fall()
    {
        if (transform.position.y > 0.25)
        {
            SpriteRenderer sp = gameObject.GetComponentInChildren<SpriteRenderer>();
            Debug.Log(sp.sortingLayerName);
            sp.sortingLayerName = "Level";
            sp.sortingOrder = -5;
            Debug.Log(sp.sortingLayerName);
        }

        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;

        Invoke("Die", 2);
    }
}
