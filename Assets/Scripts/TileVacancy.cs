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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.parent.tag == "Player")
        {
            occupied = true;

            //Debug.Log(gameObject.name + " OCCUPIED");
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.transform.parent.tag == "Player")
        {
            occupied = false;

            //Debug.Log(gameObject.name + " UNOCCUPIED");
        }
    }
}
