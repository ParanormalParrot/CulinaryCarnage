using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("PlayerFeet"))
        {
            RoomController.instance.DeleteRooms();
            LevelGenerator.instance.GenerateLevel();
            col.transform.GetComponentInParent<Player>().transform.position = Vector3.zero;
        }
    }
}