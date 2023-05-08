using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    
    public Vector3 playerChangePosition;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerFeet"))
        {
            other.GetComponentInParent<Player>().transform.position += playerChangePosition;
        }
    }
}