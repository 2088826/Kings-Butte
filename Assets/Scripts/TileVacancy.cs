using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVacancy : MonoBehaviour
{

    private bool occupied;
    private bool blocked;

    public bool Occupied
    {
        get { return occupied; }
    }

    public bool Blocked
    {
        get { return blocked; }
    }

    private void Block()
    {
        blocked = true;
        occupied = true;
    }

    private void Unblock()
    {
        blocked = false;
        occupied = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.parent.tag == "Player")
        {
            occupied = true;

            //Debug.Log(gameObject.name + " OCCUPIED");
        } 
        else if (other.gameObject.transform.parent.tag == "Obstacle")
        {
            Block();
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.transform.parent.tag == "Player" || other.gameObject.transform.parent.tag == "Obstacle")
        {
            occupied = false;

            //Debug.Log(gameObject.name + " UNOCCUPIED");
        }
        else if (other.gameObject.transform.parent.tag == "Obstacle")
        {
            Unblock();
        }
    }
}
