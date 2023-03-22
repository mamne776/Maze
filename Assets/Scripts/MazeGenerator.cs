using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public Cell[,] maze;  

    public Cell[,] CreateMaze(int givenWidth, int givenHeight)
    {
        //start with all the walls
        Debug.Log("GivenWidth: " + givenWidth + " GivenHeight: " + givenHeight);
        maze = CreateMazeWithOnlyWalls(givenWidth, givenHeight);

        //Start at a random cell
        int randX = Random.Range(0, givenWidth);
        int randY = Random.Range(0, givenHeight);
        Cell cell = maze[randX, randY];

        MakePaths(cell);

        return maze;
    }

    public Cell[,] CreateMazeWithRooms(int width, int height)
    {
        //start with all the walls
        maze = CreateMazeWithOnlyWalls(width, height);

        //Start at a random cell
        int randX = Random.Range(0, width);
        int randY = Random.Range(0, height);
        Cell cell = maze[randX, randY];

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
                Debug.Log("cell.xPos: " + cell.xPos + "cell.yPos: " + cell.yPos);
                RemoveWallBetween(cell, cell.neighbours[checkThisNeighbour]);
                //and recurse with this neighbour
                MakePaths(cell.neighbours[checkThisNeighbour]);
            }
        }
    }

    //remove the wall between two given cells
    public void RemoveWallBetween(Cell cell, Cell neighbourCell)
    {
        //Debug.Log(cell.xPos + ", " + cell.yPos);
        //when not on the top row
        if (cell.yPos < neighbourCell.yPos)
        {
            //Debug.Log("cell.yPos: " + cell.yPos);            
            Debug.Log("removing wall from the top");

            //remove wall from the top
            cell.walls[1] = false;
            cell.surroundingWalls--;
            neighbourCell.walls[3] = false;
            neighbourCell.surroundingWalls--;
            return;
        }

        if (cell.yPos > neighbourCell.yPos)
        {
            Debug.Log("removing wall from the bottom");
            cell.walls[3] = false;
            cell.surroundingWalls--;
            neighbourCell.walls[1] = false;
            neighbourCell.surroundingWalls--;
            return;
        }

        if (cell.xPos < neighbourCell.xPos)
        {
            Debug.Log("removing wall from the right");
            cell.walls[2] = false;
            cell.surroundingWalls--;
            neighbourCell.walls[0] = false;
            neighbourCell.surroundingWalls--;
            return;
        }

        if (cell.xPos > neighbourCell.xPos)
        {
            Debug.Log("removing wall from the left");
            cell.walls[0] = false;
            cell.surroundingWalls--;
            neighbourCell.walls[2] = false;
            neighbourCell.surroundingWalls--;
            return;
        }
    }

    //set the neighbours for a given cell
    public void SetNeighbours(Cell cellToCheck)
    {
        int x = cellToCheck.xPos;
        int y = cellToCheck.yPos;        

        //neighbours[0] means the neighbour to the left, [1] is the one above, [2] to the right and [4] below

        //cells on the x = 0 column have no neighbours at x - 1
        if (x > 0)
        {
            //Debug.Log("hep");
            cellToCheck.neighbours[0] = maze[x - 1, y];
            //Debug.Log("hep2");
        }
        //cells on the y = (maze.height - 1) row have no neighbours at y + 1
        if (y < maze.GetLength(1) - 1)
        {
            cellToCheck.neighbours[1] = maze[x, y + 1];
        }        
        //cells on the x = maze.width column hace no neighbours at x + 1
        if (x < maze.GetLength(0) - 1)
        {
            cellToCheck.neighbours[2] = maze[x + 1, y];
        }
        //cells on the y = 0 row have no neighbours at y - 1
        if (y > 0)
        {
            cellToCheck.neighbours[3] = maze[x, y - 1];
        }
    }

    //create a maze with all the walls intact, ie. just walls
    private Cell[,] CreateMazeWithOnlyWalls(int width, int height)
    {
        Cell[,] maze = new Cell[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Cell cell = new Cell() { yPos = j, xPos = i };
                maze[i, j] = cell;
                Debug.Log("Here: " + " i: " + i + " j: " + j);
            }
        }
        return maze;
    }
}
