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
        if (other.gameObject.transform.parent.tag == "Player" || other.gameObject.transform.parent.tag == "Obstacle")
        {
            occupied = true;

            //Debug.Log(gameObject.name + " OCCUPIED");
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.transform.parent.tag == "Player" || other.gameObject.transform.parent.tag == "Obstacle")
        {
            occupied = false;

            //Debug.Log(gameObject.name + " UNOCCUPIED");
        }
    }
}
