using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public Cell[,] maze;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int[,] CreateTestMaze()
    {
        int[,] testMaze = new int[2, 3];
        testMaze[0, 0] = 0;
        testMaze[0, 1] = 1;
        testMaze[0, 2] = 2;
        testMaze[1, 0] = 0;
        testMaze[1, 1] = 1;
        testMaze[1, 2] = 2;

        return testMaze;
    }

    public Cell[,] CreateMaze(int height, int width)
    {
        maze = CreateMazeWithOnlyWalls(height, width);

        //Start at a random cell
        int randH = Random.Range(0, height);
        int randW = Random.Range(0, width);
        Cell cell = maze[randH, randW];

        MakePaths(cell);

        return maze;
    }

    private void MakePaths(Cell cell)
    {
        //mark this cell as checked
        cell.hasBeenChecked = true;

        //get a list of its neighbours
        SetNeighbours(cell);

        //For each neighbor, starting with a randomly selected neighbor:
        //If that neighbor hasn't been visited,
        //remove the wall between this cell and that neighbor,
        //and then recurse with that neighbor as the current cell.        

        List<int> neighbourNumbers = new List<int>();

        int amountOfNeighbours = 0;
        for (int i = 0; i < 4; i++)
        {
            if (cell.neighbours[i] != null)
            {
                amountOfNeighbours++;
                //neighbournumbers consists of the neighbours the cell has. 0 = neighbour to the left, 1 = above
                //2 = to the right and 3 is below
                neighbourNumbers.Add(i);
            }
        }

        //for each neighbour, starting with randomly selected neighbour
        int randomIndexForChoosingFirst;

        randomIndexForChoosingFirst = Random.Range(0, amountOfNeighbours);

        //pick first neighbour
        int first = neighbourNumbers[randomIndexForChoosingFirst];

        //remove the orderNumber from the list
        neighbourNumbers.RemoveAt(randomIndexForChoosingFirst);


        if (!cell.neighbours[first].hasBeenChecked/* && cell.neighbours[first] != null*/)
        {
            //remove the wall between cell and neighbour
            RemoveWallBetween(cell, cell.neighbours[first]);
            MakePaths(cell.neighbours[first]);
        }

        //pick second
        int randomForSecond = Random.Range(0, amountOfNeighbours - 1);
        int second = neighbourNumbers[randomForSecond];
        neighbourNumbers.RemoveAt(randomForSecond);

        if (!cell.neighbours[second].hasBeenChecked)
        {
            //remove the wall between cell and neighbour
            RemoveWallBetween(cell, cell.neighbours[second]);
            MakePaths(cell.neighbours[second]);
        }

        if (amountOfNeighbours > 2)
        {
            //pick third
            int randomForThird = Random.Range(0, amountOfNeighbours - 2);
            int third = neighbourNumbers[randomForThird];

            neighbourNumbers.RemoveAt(randomForThird);
            if (!cell.neighbours[third].hasBeenChecked)
            {
                //remove the wall between cell and neighbour
                RemoveWallBetween(cell, cell.neighbours[third]);
                MakePaths(cell.neighbours[third]);
            }
        }

        if (amountOfNeighbours > 3)
        {
            //pick last
            int fourth = neighbourNumbers[0];

            if (!cell.neighbours[fourth].hasBeenChecked)
            {
                //remove the wall between cell and neighbour
                RemoveWallBetween(cell, cell.neighbours[fourth]);
                MakePaths(cell.neighbours[fourth]);
            }
        }
    }

    //remove the wall between two given cells
    private void RemoveWallBetween(Cell cell, Cell neighbourCell)
    {
        if (cell.heightPos < neighbourCell.heightPos)
        {
            cell.walls[1] = false;
            cell.surroundingWalls--;
            neighbourCell.walls[3] = false;
            neighbourCell.surroundingWalls--;
            return;
        }

        if (cell.heightPos > neighbourCell.heightPos)
        {
            cell.walls[3] = false;
            cell.surroundingWalls--;
            neighbourCell.walls[1] = false;
            neighbourCell.surroundingWalls--;
            return;
        }

        if (cell.widthPos < neighbourCell.widthPos)
        {
            cell.walls[2] = false;
            cell.surroundingWalls--;
            neighbourCell.walls[0] = false;
            neighbourCell.surroundingWalls--;
            return;
        }

        if (cell.widthPos > neighbourCell.widthPos)
        {
            cell.walls[0] = false;
            cell.surroundingWalls--;
            neighbourCell.walls[2] = false;
            neighbourCell.surroundingWalls--;
            return;
        }
    }

    //set the neighbours for a given cell
    private void SetNeighbours(Cell cellToCheck)
    {
        int h = cellToCheck.heightPos;
        int w = cellToCheck.widthPos;

        //neighbours[0] means the neighbour to the left, [1] is the one above, [2] to the right and [4] below

        //cells on the h = 0 row have no neighbours at h - 1
        if (h > 0)
        {
            cellToCheck.neighbours[3] = maze[h - 1, w];
        }
        //cells on the h = (maze.height - 1) row have no neighbours at h + 1
        if (h < maze.GetLength(0) - 1)
        {
            cellToCheck.neighbours[1] = maze[h + 1, w];
        }
        //cells on the w = 0 column have no neighbours at w - 1
        if (w > 0)
        {
            cellToCheck.neighbours[0] = maze[h, w - 1];
        }
        //cells on the w = maze.width column hace no neighbours at w + 1
        if (w < maze.GetLength(1) - 1)
        {
            cellToCheck.neighbours[2] = maze[h, w + 1];
        }
    }

    //create a maze with all the walls intact, ie. just walls
    private Cell[,] CreateMazeWithOnlyWalls(int h, int w) //h = height, w = width
    {
        Cell[,] maze = new Cell[h, w];
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                Cell cell = new Cell() { heightPos = i, widthPos = j };
                maze[i, j] = cell;
            }
        }
        return maze;
    }
}
