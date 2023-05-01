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
        Destroy(gameObject);
    }
}
