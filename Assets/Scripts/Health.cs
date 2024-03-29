using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject hitbox;
    int hp = 1;

    private bool isDefeated = false;

    public bool IsDefeated { get { return isDefeated; } }

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
        SpriteRenderer sp = gameObject.GetComponentInChildren<SpriteRenderer>();

        if (transform.position.y > 0.25)
        {
            sp.sortingLayerName = "Level";
            sp.sortingOrder = -1;
            
        }
        else
        {
            sp.sortingLayerName = "Level";
            sp.sortingOrder = 1;
        }

        GameManager.RemovePlayers(this.gameObject);
        isDefeated = true;

        hitbox.GetComponent<BoxCollider2D>().enabled = false;

        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;

        Invoke("Die", 2);
    }
}
