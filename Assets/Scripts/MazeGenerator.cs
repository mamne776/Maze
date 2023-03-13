using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public Cell[,] maze;  

    public Cell[,] CreateMaze(int height, int width)
    {
        //start with all the walls
        maze = CreateMazeWithOnlyWalls(height, width);

        //Start at a random cell
        int randH = Random.Range(0, height);
        int randW = Random.Range(0, width);
        Cell cell = maze[randH, randW];

        MakePaths(cell);

        return maze;
    }

    public Cell[,] CreateMazeWithRooms(int height, int width)
    {
        //start with all the walls
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
        //allowdNeighbourNumbers consists of the neighbours the cell has. 0 = neighbour to the left, 1 = above
        //2 = to the right and 3 is below
        List<int> allowedNeighbourNumbers = new List<int>();

        //how many neighbours the cell has
        int amountOfNeighbours = 0;

        //mark this cell as checked
        cell.hasBeenChecked = true;

        //set the cells neighbours
        SetNeighbours(cell);

        //check how many neighbours and add the neighbours to the list
        for (int i = 0; i < 4; i++)
        {
            if (cell.neighbours[i] != null)
            {
                allowedNeighbourNumbers.Add(i);
                amountOfNeighbours++;
            }
        }

        //For each neighbor, starting with a randomly selected neighbor:
        //If that neighbor hasn't been visited,
        //remove the wall between this cell and that neighbor,
        //and then recurse with that neighbor as the current cell.

        //for all the neighbours,
        for (int i = 0; i < amountOfNeighbours; i++)
        {
            //choose random neighbour, and remove it from the list
            int randomIndex = Random.Range(0, amountOfNeighbours - i);
            int checkThisNeighbour = allowedNeighbourNumbers[randomIndex];
            allowedNeighbourNumbers.RemoveAt(randomIndex);

            //if not checked yet
            if (!cell.neighbours[checkThisNeighbour].hasBeenChecked)
            {
                //remove the wall between the cells
                RemoveWallBetween(cell, cell.neighbours[checkThisNeighbour]);
                //and recurse with this neighbour
                MakePaths(cell.neighbours[checkThisNeighbour]);
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

        //cells on the w = 0 column have no neighbours at w - 1
        if (w > 0)
        {
            cellToCheck.neighbours[0] = maze[h, w - 1];
        }
        //cells on the h = (maze.height - 1) row have no neighbours at h + 1
        if (h < maze.GetLength(0) - 1)
        {
            cellToCheck.neighbours[1] = maze[h + 1, w];
        }        
        //cells on the w = maze.width column hace no neighbours at w + 1
        if (w < maze.GetLength(1) - 1)
        {
            cellToCheck.neighbours[2] = maze[h, w + 1];
        }
        //cells on the h = 0 row have no neighbours at h - 1
        if (h > 0)
        {
            cellToCheck.neighbours[3] = maze[h - 1, w];
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
