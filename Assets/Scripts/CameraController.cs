using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс управления камерой
public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Room currentRoom;

    public float speed;
    
    void Start()
    {
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if (currentRoom == null)
        {
            return;
        }

        Vector3 targetPosition = GetCameraTargetPosition();
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
    }

    Vector3 GetCameraTargetPosition()
    {
        if (currentRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPosition = currentRoom.GetRoomCenter();
        targetPosition.z = transform.position.z;
        return targetPosition;
    }
}