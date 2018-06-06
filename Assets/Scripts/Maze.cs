using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public GameObject wall;
    public GameObject correctWay;
    private int[,] maze;
    public int mazeSize = 11;
    public int turnComplexity = 3;
    public int crossroads = 3;
    public int randomWalls = 3;

    private int difficulty;

    // Use this for initialization
    void Awake()
    {
        difficulty = GameManager.difficulty;

        if (difficulty == 1)
        {
            mazeSize = 11;
            turnComplexity = 4;
            crossroads = 4;
            randomWalls = 6;
        }
        else if (difficulty == 2)
        {
            mazeSize = 15;
            turnComplexity = 6;
            crossroads = 6;
            randomWalls = 12;
        }
        else if (difficulty == 3)
        {
            mazeSize = 19;
            turnComplexity = 7;
            crossroads = 9;
            randomWalls = 20;
        }

        maze = MazeGenerator.CreateMaze(mazeSize, turnComplexity, crossroads, randomWalls);
        GenerateMaze(maze);
    }

    void GenerateMaze(int[,] maze)
    {
        for (int y = 0; y < maze.GetLength(0); y++)
        {
            for (int x = 0; x < maze.GetLength(1); x++)
            {
                if (maze[x, y] == 1)
                    GameObject.Instantiate(wall, new Vector3(x, 1, y), Quaternion.identity);
                if (maze[x, y] == 2)
                    GameObject.Instantiate(correctWay, new Vector3(x, 0, y), Quaternion.identity);
            }
        }
    }

    public Vector2Int GetStartPosition()
    {
        return new Vector2Int(mazeSize / 2, mazeSize - 1);
    }

    public int GetPosition(Vector2Int position)
    {
        if (0 <= position.x && position.x < mazeSize && 0 <= position.y && position.y < mazeSize) // only return if in maze
        {
            return maze[position.x, position.y];
        }
        return 0;
    }

    public int GetMazeSize()
    {
        return mazeSize;
    }
}
