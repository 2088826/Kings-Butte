using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVacancy : MonoBehaviour
{

    private bool occupied;

    public bool Occupied
    {
        get { return occupied; }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            occupied = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            occupied = false;
        }
    }
}
