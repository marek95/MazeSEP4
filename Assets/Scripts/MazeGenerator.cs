using System;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator
{
    public const int EMPTY = 0, WALL = 1, CORRECT_WAY = 2, CROSSROAD = 3;
    private static int[,] maze;
    public static int[,] CreateMaze(int size, int turnComplexity, int crossroads, int randomWalls)
    {
        int startP = 0;
        int midP = size / 2;
        int endP = size - 1;

        maze = new int[size, size];

        Random rnd = new Random(); // for all randomizing operations

        // generate 'template' maze
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
            {
                if (x == 0 || x == (size - 1)) //side walls
                    maze[x, y] = WALL;
                else
                {
                    if (y == 0 || y == (size - 1)) //front and back walls
                    {
                        if (x == midP)
                            maze[x, y] = CORRECT_WAY;
                        else
                            maze[x, y] = WALL;
                    }
                    else
                    {
                        if (x % 2 == 0 && y % 2 == 0) //'island' walls
                            maze[x, y] = WALL;
                        else
                            maze[x, y] = EMPTY; //empty way
                    }
                }
            }

        int[] horizontalPs = new int[turnComplexity];
        int[] verticalPs = new int[horizontalPs.Length - 1];

        // getting a list of possible horizontal points
        ArrayList possibleHorizontalPs = new ArrayList();
        for (int i = 0; i < size; i++)
            if (i % 2 == 1)
                possibleHorizontalPs.Add(i);

        // choosing random horizontal points
        for (int h = 0; h < horizontalPs.Length; h++)
        {
            int possibleH = (int)possibleHorizontalPs[rnd.Next(possibleHorizontalPs.Count)];
            possibleHorizontalPs.Remove(possibleH);
            horizontalPs[h] = possibleH;
        }
        Array.Sort(horizontalPs);

        // getting list of possible vertical points for each row + choosing random vertical points
        for (int v = 0; v < verticalPs.Length; v++)
        {
            ArrayList possibleVerticalPs = new ArrayList();
            for (int p = 1; p < size - 1; p++)
                if (p % 2 == 1)
                    possibleVerticalPs.Add(p);

            if (v == 0 || v == verticalPs.Length - 1)
                possibleVerticalPs.Remove(midP);

            if (v > 0)
                possibleVerticalPs.Remove(verticalPs[v - 1]);

            int possibleP = (int)possibleVerticalPs[rnd.Next(possibleVerticalPs.Count)];
            verticalPs[v] = possibleP;
        }

        // arrays for coordinate point of correct path
        int[] pathX = new int[verticalPs.Length * 2 + 4];
        int[] pathY = new int[pathX.Length];

        for (int i = 0; i < pathX.Length; i++)
        {
            if (i == 0 || i == pathX.Length - 2)
                pathX[i] = midP;
            else
            {
                if (i % 2 == 1)
                    pathX[i] = pathX[i - 1];
                else
                    pathX[i] = verticalPs[i / 2 - 1];
            }
        }

        for (int i = 0; i < pathY.Length; i++)
        {
            if (i == 0)
                pathY[i] = startP;
            else if (i == pathY.Length - 1)
                pathY[i] = endP;
            else if (i % 2 == 1)
                pathY[i] = horizontalPs[i / 2];
            else
                pathY[i] = pathY[i - 1];
        }

        // marking correct path on the maze
        for (int i = 0; i < pathX.Length - 1; i++)
            PutPath(pathX[i], pathY[i], pathX[i + 1], pathY[i + 1]);

        // put wall where has to be wall (between correct horizontal paths)
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
                if (maze[x, y] == EMPTY && maze[x, y - 1] == CORRECT_WAY && maze[x, y + 1] == CORRECT_WAY)
                    maze[x, y] = WALL;

        // getting all possible crossroad coordinates
        ArrayList possibleCrossroadX = new ArrayList();
        ArrayList possibleCrossroadY = new ArrayList();

        for (int x = 1; x < size - 1; x++)
            for (int y = 1; y < size - 1; y++)
                if (maze[x, y] == CORRECT_WAY)
                    if (maze[x, y - 1] == EMPTY || maze[x, y + 1] == EMPTY || maze[x - 1, y] == EMPTY || maze[x + 1, y] == EMPTY)
                    {
                        possibleCrossroadX.Add(x);
                        possibleCrossroadY.Add(y);
                    }

        // choosing random crossroads
        for (int i = 0; i < crossroads; i++)
        {
            int choosenIndex = rnd.Next(possibleCrossroadX.Count);
            int x = (int)possibleCrossroadX[choosenIndex];
            int y = (int)possibleCrossroadY[choosenIndex];

            if (maze[x, y - 1] == EMPTY)
                maze[x, y - 1] = CROSSROAD;
            if (maze[x, y + 1] == EMPTY)
                maze[x, y + 1] = CROSSROAD;
            if (maze[x - 1, y] == EMPTY)
                maze[x - 1, y] = CROSSROAD;
            if (maze[x + 1, y] == EMPTY)
                maze[x + 1, y] = CROSSROAD;

            possibleCrossroadX.RemoveAt(choosenIndex);
            possibleCrossroadY.RemoveAt(choosenIndex);
        }

        // surround correct path with walls
        for (int x = 1; x < size - 1; x++)
            for (int y = 1; y < size - 1; y++)
                if (maze[x, y] == CORRECT_WAY)
                {
                    if (maze[x, y - 1] == EMPTY)
                        maze[x, y - 1] = WALL;
                    if (maze[x, y + 1] == EMPTY)
                        maze[x, y + 1] = WALL;
                    if (maze[x - 1, y] == EMPTY)
                        maze[x - 1, y] = WALL;
                    if (maze[x + 1, y] == EMPTY)
                        maze[x + 1, y] = WALL;
                    if (maze[x - 1, y - 1] == EMPTY)
                        maze[x - 1, y - 1] = WALL;
                    if (maze[x - 1, y + 1] == EMPTY)
                        maze[x - 1, y + 1] = WALL;
                    if (maze[x + 1, y - 1] == EMPTY)
                        maze[x + 1, y - 1] = WALL;
                    if (maze[x + 1, y + 1] == EMPTY)
                        maze[x + 1, y + 1] = WALL;
                }

        // place some random walls
        for (int i = 0; i < randomWalls; i++)
        {
            ArrayList possibleWallX = new ArrayList();
            ArrayList possibleWallY = new ArrayList();

            for (int x = 1; x < size - 1; x++)
                for (int y = 1; y < size - 1; y++)
                    if (CanPutWall(x, y))
                    {
                        possibleWallX.Add(x);
                        possibleWallY.Add(y);
                    }

            int chosenIndex = rnd.Next(possibleWallX.Count);
            maze[(int)possibleWallX[chosenIndex], (int)possibleWallY[chosenIndex]] = WALL;
        }
        return maze;
    }

    private static bool CanPutWall(int x, int y)
    {
        if (maze[x, y] != EMPTY) // is correct way or crossroad
            return false;
        if (x % 2 == 1 && y % 2 == 1)
            return false;
        if (maze[x, y - 1] == CROSSROAD || maze[x, y + 1] == CROSSROAD || maze[x - 1, y] == CROSSROAD || maze[x + 1, y] == CROSSROAD) // near crossroad
            return false;
        if (maze[x - 1, y] == WALL && maze[x - 1, y - 1] == WALL && maze[x, y - 1] == WALL) // left top corner
            return false;
        if (maze[x + 1, y] == WALL && maze[x + 1, y - 1] == WALL && maze[x, y - 1] == WALL) // right top corner
            return false;
        if (maze[x - 1, y] == WALL && maze[x - 1, y + 1] == WALL && maze[x, y + 1] == WALL) // left bottom corner
            return false;
        if (maze[x + 1, y] == WALL && maze[x + 1, y + 1] == WALL && maze[x, y + 1] == WALL) // right bottm corner
            return false;

        return true;
    }

    private static void PutPath(int x1, int y1, int x2, int y2)
    {
        if (x1 == x2) // vertical
        {
            if (y1 < y2)
                for (int i = y1; i <= y2; i++)
                    maze[x1, i] = CORRECT_WAY;
            else
                for (int i = y2; i <= y1; i++)
                    maze[x1, i] = CORRECT_WAY;
        }
        else if (y1 == y2) // horizontal
        {
            if (x1 < x2)
                for (int i = x1; i <= x2; i++)
                    maze[i, y1] = CORRECT_WAY;
            else
                for (int i = x2; i <= x1; i++)
                    maze[i, y1] = CORRECT_WAY;
        }
    }
}
