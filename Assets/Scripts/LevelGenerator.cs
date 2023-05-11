using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public enum RoomType
{
    Starting,
    Battle,
    Stock,
    Boss,
    Default
}

public class RoomData
{
    public Vector2Int position;
    public RoomType type = RoomType.Default;


    public RoomData(Vector2Int position, RoomType type)
    {
        this.position = position;
        this.type = type;
    }
}

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;
    public int planSizeX, planSizeY;
    public Vector2Int startingRoomPosition;
    public int numberOfRooms;
    public int currentFloorNumber;
    public int finalFloorNumber = 2;

    // План уровня
    public RoomData[,] levelPlan;

    // Занятые позиции 
    private List<Vector2Int> positionsVisited;

    private List<Vector2Int> deadEndPositions;

    public List<Boss> bosses;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        positionsVisited = new List<Vector2Int>();
        deadEndPositions = new List<Vector2Int>();
        GenerateLevel();
        AstarPath.active.Scan();
    }


    public void GenerateLevel()
    {
        currentFloorNumber++;
        UserInterface.instance.StartTransition();
        numberOfRooms = 2 * currentFloorNumber + 8;
        Enemy.numberOfEnemies = 0;
        GeneratePlan();
        SpawnRooms();
    }


    int GetNumberOfNeighbors(Vector2Int position)
    {
        int numberOfNeighbours = 0;
        if (positionsVisited.Contains(position + Vector2Int.right))
        {
            numberOfNeighbours++;
        }

        if (positionsVisited.Contains(position + Vector2Int.left))
        {
            numberOfNeighbours++;
        }

        if (positionsVisited.Contains(position + Vector2Int.up))
        {
            numberOfNeighbours++;
        }

        if (positionsVisited.Contains(position + Vector2Int.down))
        {
            numberOfNeighbours++;
        }

        return numberOfNeighbours;
    }

    Vector2Int GetNewPosition()
    {
        int x, y;
        Vector2Int checkingPos = Vector2Int.zero;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (positionsVisited.Count - 1));
            x = positionsVisited[index].x;
            y = positionsVisited[index].y;
            float randomValue = Random.value;
            if (randomValue < 0.25f)
            {
                x += 1;
            }

            else if (randomValue >= 0.25f && randomValue < 0.5)
            {
                x -= 1;
            }
            else if (randomValue >= 0.5f && randomValue < 0.75f)
            {
                y += 1;
            }
            else
            {
                y -= 1;
            }

            checkingPos = new Vector2Int(x, y);
        } while (positionsVisited.Contains(checkingPos) || x >= planSizeX || x < 0 || y >= planSizeY ||
                 y < 0);

        return checkingPos;
    }

    Vector2Int GetPositionWithOneNeighbour()
    {
        int index = 0, inc = 0;
        int x = 0, y = 0;
        Vector2Int checkingPos = Vector2Int.zero;
        do
        {
            inc = 0;
            do
            {
                index = Mathf.RoundToInt(Random.value * (positionsVisited.Count - 1));
                inc++;
            } while (GetNumberOfNeighbors(positionsVisited[index]) > 1 && inc < 100);

            x = positionsVisited[index].x;
            y = positionsVisited[index].y;
            float randomValue = Random.value;
            if (randomValue < 0.25f)
            {
                x += 1;
            }

            else if (randomValue >= 0.25f && randomValue < 0.5)
            {
                x -= 1;
            }
            else if (randomValue >= 0.5f && randomValue < 0.75f)
            {
                y += 1;
            }
            else
            {
                y -= 1;
            }

            checkingPos = new Vector2Int(x, y);
        } while (positionsVisited.Contains(checkingPos) || x >= planSizeX || x < 0 || y >= planSizeY || y < 0);

        if (inc >= 100)
        {
            print("Error: could not find position with only one neighbor");
        }

        return checkingPos;
    }

    void FindDeadEnds()
    {
        foreach (var position in positionsVisited)
        {
            if (levelPlan[position.x, position.y].type != RoomType.Starting)
            {
                if (GetNumberOfNeighbors(position) == 1)
                {
                    deadEndPositions.Add(position);
                }
            }
        }
    }


    void GeneratePlan()
    {
        numberOfRooms = 2 * currentFloorNumber + 8;
        Enemy.numberOfEnemies = 0;
        positionsVisited = new List<Vector2Int>();
        deadEndPositions = new List<Vector2Int>();
        levelPlan = new RoomData[planSizeX, planSizeY];
        levelPlan[startingRoomPosition.x, startingRoomPosition.y] =
            new RoomData(startingRoomPosition, RoomType.Starting);
        float randomCompare, randomCompareStart = 0.2f, randomCompareEnd = 0.8f;
        positionsVisited.Add(startingRoomPosition);
        Vector2Int checkPosition = Vector2Int.zero;
        for (int i = 0; i < numberOfRooms - 1; i++)
        {
            randomCompare = randomCompareStart + ((randomCompareEnd - randomCompareEnd) / (numberOfRooms - 1)) * i;
            checkPosition = GetNewPosition();
            if (GetNumberOfNeighbors(checkPosition) > 1 && randomCompare > Random.value)
            {
                int iterations = 0;
                do
                {
                    checkPosition = GetPositionWithOneNeighbour();
                    iterations++;
                } while (GetNumberOfNeighbors(checkPosition) > 1 && iterations < 100);

                if (iterations >= 50)
                    print("error: could not create with fewer neighbors than : " + GetNumberOfNeighbors(checkPosition));
            }

            levelPlan[(int)checkPosition.x, (int)checkPosition.y] = new RoomData(checkPosition, RoomType.Battle);
            positionsVisited.Add(checkPosition);
        }

        FindDeadEnds();
        Vector2Int maxPosition = startingRoomPosition;
        foreach (var position in deadEndPositions)
        {
            if (Math.Sqrt((position.x - startingRoomPosition.x) * (position.x - startingRoomPosition.x) +
                          (position.y - startingRoomPosition.y) * (position.y - startingRoomPosition.y)) >
                Math.Sqrt((maxPosition.x - startingRoomPosition.x) * (maxPosition.x - startingRoomPosition.x) +
                          (maxPosition.y - startingRoomPosition.y) * (maxPosition.y - startingRoomPosition.y)))
            {
                maxPosition = position;
            }
        }

        if (deadEndPositions.Count < 2)
        {
            GeneratePlan();
        }

        if (levelPlan[maxPosition.x, maxPosition.y].type != RoomType.Starting)
        {
            levelPlan[maxPosition.x, maxPosition.y].type = RoomType.Boss;
        }
        else
        {
            GeneratePlan();
        }

        foreach (var position in deadEndPositions)
        {
            if (position != maxPosition)
            {
                levelPlan[position.x, position.y].type = RoomType.Stock;
                break;
            }
        }
    }

    private void SpawnRooms()
    {
        foreach (var position in positionsVisited)
        {
            RoomController.instance.SpawnRoom(levelPlan[position.x, position.y], startingRoomPosition);
        }

        RoomController.instance.RemoveDoors();
        AstarPath.active.Scan();
        Player player = FindObjectOfType<Player>();
        player.transform.position = Vector3.zero;
    }
}