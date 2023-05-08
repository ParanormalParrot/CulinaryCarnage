using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorType doorType;
    public GameObject wall;
    public GameObject closedDoor;
    public GameObject openDoor;
    public bool isWall;

    
    public enum DoorType
    {
        left,
        right,
        top,
        bottom
    }

    // Устанавливает стену на месте двери
    public void setWall()
    {
        wall.gameObject.SetActive(true);
        closedDoor.gameObject.SetActive(false);
        openDoor.gameObject.SetActive(false);
        isWall = true;
    }
    
    // Устанавливает закрытую дверь
    public void setClosedDoors()
    {
        wall.gameObject.SetActive(false);
        closedDoor.gameObject.SetActive(true);
        openDoor.gameObject.SetActive(false);
    }
    
    // Устанавливает открытую дверь
    public void setOpenedDoors()
    {
        wall.SetActive(false);
        closedDoor.SetActive(false);
        openDoor.SetActive(true);
    }
    
}
