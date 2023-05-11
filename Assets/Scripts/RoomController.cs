using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = System.Random;


public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    public List<Room> loadedRooms = new List<Room>();
    private Room curentRoom;
    public Room startingRoom;
    public Room stock;
    public Room bossRoom;
    public List<Room> battleRooms;

    public GameObject foodContainer;
    public GameObject stairs;
    public GameObject chooseStartingWeapon;
    private bool isLoadingRoom;
    // Start is called before the first frame update

    public void SpawnRoom(RoomData roomData, Vector2Int startPosition)
    {
        Room room = new Room();
        if (DoesRoomExists(roomData.position.x - startPosition.x, roomData.position.y - startPosition.y))
        {
            return;
        }

        if (roomData.type == RoomType.Starting)
        {
            room = startingRoom;
        }

        if (roomData.type == RoomType.Battle)
        {
            var rand = new System.Random();
            room = battleRooms[rand.Next(battleRooms.Count)];
        }

        if (roomData.type == RoomType.Stock)
        {
            room = stock;
        }

        if (roomData.type == RoomType.Boss)
        {
            room = bossRoom;
        }

        room.X = roomData.position.x - startPosition.x;
        room.Y = roomData.position.y - startPosition.y;

        room = Instantiate(room, room.GetRoomCenter(), Quaternion.identity);
        loadedRooms.Add(room);
    }

    public void RemoveDoors()
    {
        foreach (var room in loadedRooms)
        {
            room.RemoveUnconnectedDoors();
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public bool DoesRoomExists(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currentRoom = room;
        curentRoom = room;
    }

    public void DeleteRooms()
    {
        foreach (var room in loadedRooms)
        {
            Destroy(room.GameObject());
        }

        loadedRooms = new List<Room>();
    }
}