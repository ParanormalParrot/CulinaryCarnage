using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using Random = System.Random;

public class Room : MonoBehaviour
{
    // Ширина комнаты
    public int width;

    // Высота комнаты
    public int height;

    // Координаты комнаты
    public int X, Y;

    public RoomType type;

    public Door topDoor;
    public Door rightDoor;
    public Door bottomDoor;
    public Door leftDoor;

    public bool hasPlayer;
    public bool hasEnemies;
    public bool isCleared;
    public EnemySpawner[] enemySpawners;

    public List<Door> doors = new List<Door>();

    public Room()
    {
    }


    void Start()
    {
        isCleared = false;
        enemySpawners = GetComponentsInChildren<EnemySpawner>();
        if (type == RoomType.Starting && LevelGenerator.instance.currentFloorNumber == 1)
        {
            GameObject obj = Instantiate(RoomController.instance.chooseStartingWeapon, new Vector3(0, 4, 0), Quaternion.identity);
            obj.transform.SetParent(transform);
        }
    }

    // Удаление дверей, которые не ведут в другие комнаты
    public void RemoveUnconnectedDoors()
    {
        if (GetTop() == null)
        {
            topDoor.setWall();
            topDoor.isWall = true;
        }
        else
        {
            topDoor.setOpenedDoors();
            GetTop().bottomDoor.setOpenedDoors();
        }

        if (GetRight() == null)
        {
            rightDoor.setWall();
        }
        else
        {
            rightDoor.setOpenedDoors();
            GetRight().leftDoor.setOpenedDoors();
        }

        if (GetLeft() == null)
        {
            leftDoor.setWall();
        }
        else
        {
            leftDoor.setOpenedDoors();
            GetLeft().rightDoor.setOpenedDoors();
        }

        if (GetBottom() == null)
        {
            bottomDoor.setWall();
        }
        else
        {
            bottomDoor.setOpenedDoors();
            GetBottom().topDoor.setOpenedDoors();
        }
    }

    // Полуение верхней соседней комнаты
    public Room GetTop()
    {
        if (RoomController.instance.DoesRoomExists(X, Y + 1))
        {
            return RoomController.instance.FindRoom(X, Y + 1);
        }

        return null;
    }

    // Полуение правой соседней комнаты
    public Room GetRight()
    {
        if (RoomController.instance.DoesRoomExists(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }

        return null;
    }

    // Полуение нижней соседней комнаты
    public Room GetBottom()
    {
        if (RoomController.instance.DoesRoomExists(X, Y - 1))
        {
            return RoomController.instance.FindRoom(X, Y - 1);
        }

        return null;
    }

    // Полуение левой соседней комнаты
    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExists(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }

        return null;
    }

    // Полуение левой соседней комнаты
    public Vector3 GetRoomCenter()
    {
        return new Vector3(X * width, Y * height);
    }

    // Обознаение границ комнаты
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            RoomController.instance.OnPlayerEnterRoom(this);

            if ((type == RoomType.Battle || type == RoomType.Boss) && !isCleared && !hasPlayer)
            {
                hasPlayer = true;
                closeDoors();
                StartCoroutine(CheckEnemies());
            }

            if (!isCleared && !hasEnemies)
            {
                hasEnemies = true;
                if (type == RoomType.Battle)
                {
                    foreach (var spawner in enemySpawners)
                    {
                        spawner.SpawnEnemy();
                    }
                }

                if (type == RoomType.Boss)
                {
                    BossSpawner spawner = FindObjectOfType<BossSpawner>();
                    spawner.SpawnBoss();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayer = false;
        }
    }

    // Выполнение действий после зачистки комнаты от врагов
    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Enemy.numberOfEnemies <= 0);
        openDoors();
        var rand = new Random();
        int r = rand.Next(0, 2);
        if (type == RoomType.Battle)
        {
            GameObject container =
                Instantiate(RoomController.instance.foodContainer, GetRoomCenter(), Quaternion.identity);
            container.transform.SetParent(transform);
        }

        if (type == RoomType.Boss)
        {
            if (LevelGenerator.instance.currentFloorNumber == LevelGenerator.instance.finalFloorNumber)
            {
                UserInterface.instance.victoryMenu.GameObject().SetActive(true);
            }
            else
            {
                GameObject stairs = Instantiate(RoomController.instance.stairs, GetRoomCenter(), Quaternion.identity);
                stairs.transform.SetParent(transform);
            }
        }

        isCleared = true;
    }

    // Открытие всех дверей в комнате
    public void openDoors()
    {
        if (!topDoor.isWall)
        {
            topDoor.setOpenedDoors();
        }

        if (!bottomDoor.isWall)
        {
            bottomDoor.setOpenedDoors();
        }

        if (!leftDoor.isWall)
        {
            leftDoor.setOpenedDoors();
        }

        if (!rightDoor.isWall)
        {
            rightDoor.setOpenedDoors();
        }
    }

    // Закрытие всех дверей 
    public void closeDoors()
    {
        if (!topDoor.isWall)
        {
            topDoor.setClosedDoors();
        }

        if (!bottomDoor.isWall)
        {
            bottomDoor.setClosedDoors();
        }

        if (!leftDoor.isWall)
        {
            leftDoor.setClosedDoors();
        }

        if (!rightDoor.isWall)
        {
            rightDoor.setClosedDoors();
        }
    }
}