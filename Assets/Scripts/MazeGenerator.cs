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
        //Cell[,] maze = new Cell[height, width];
        //Debug.Log("Creating Maze!");
        maze = CreateMazeWithOnlyWalls(height, width);

        List<Cell> listOfUncheckedCells = new List<Cell>();

        //Start at a random cell
        int randH = Random.Range(0, height);
        int randW = Random.Range(0, width);
        Cell cell = maze[randH, randW];

        Debug.Log("cell pos:" + cell.heightPos + ", " + cell.widthPos);

        //mark this cell as checked
        cell.hasBeenChecked = true;

        //get a list of its neighbours
        SetNeighbours(cell);

        //For each neighbor, starting with a randomly selected neighbor:
        //If that neighbor hasn't been visited,
        //remove the wall between this cell and that neighbor,
        //and then recurse with that neighbor as the current cell.

        //for each neighbour, starting with randomly selected neighbour
        int randomForFirst = Random.Range(0, 4);

        List<int> orderNumbers = new List<int>() { 0, 1, 2, 3 };
        //pick first neighbour
        int first = orderNumbers[randomForFirst];
        //remove the orderNumber from the list
        orderNumbers.RemoveAt(randomForFirst);

        if (!cell.neighbours[first].hasBeenChecked && cell.neighbours[first] != null)
        {
            //remove the wall between cell and neighbour
            RemoveWallBetween(cell, cell.neighbours[first]);
        }

        //pick second
        int randomForSecond = Random.Range(0, 3);
        int second = orderNumbers[randomForSecond];
        orderNumbers.RemoveAt(randomForSecond);
        if (!cell.neighbours[second].hasBeenChecked && cell.neighbours[second] != null)
        {
            //remove the wall between cell and neighbour
            RemoveWallBetween(cell, cell.neighbours[second]);
        }

        //pick third
        int randomForThird = Random.Range(0, 2);
        int third = orderNumbers[randomForThird];
        orderNumbers.RemoveAt(randomForThird);
        if (!cell.neighbours[third].hasBeenChecked && cell.neighbours[third] != null)
        {
            //remove the wall between cell and neighbour
            RemoveWallBetween(cell, cell.neighbours[third]);
        }

        //pick last
        int fourth = orderNumbers[0];
        if (!cell.neighbours[fourth].hasBeenChecked && cell.neighbours[fourth] != null)
        {
            //remove the wall between cell and neighbour
            RemoveWallBetween(cell, cell.neighbours[fourth]);
        }

        /*
        //add the neighbours to the list of Cells we still need to check
        for (int i = 0; i < 4; i++)
        {
            //no nulls or already checked cells
            if (cell.neighbours[i] != null && !cell.neighbours[i].hasBeenChecked)
            {
                listOfUncheckedCells.Add(cell.neighbours[i]);
            }
        }
        */

        /*
        //dangerzone
        while (listOfUncheckedCells.Count > 0)
        {
            //select random neighbour
            int randNeighbour = Random.Range(0, 4);


            while (cell.neighbours[randNeighbour] == null)
            {
                randNeighbour = Random.Range(0, 4);





                //remove the wall
                switch (randNeighbour)
                {
                    case 0:
                        if (!cell.neighbours[0].hasBeenChecked)
                        {
                            //remove the wall on this cells left
                            cell.walls[0] = false;
                            cell.surroundingWalls--;
                            //remove the wall from the neighbours right
                            cell.neighbours[0].walls[2] = false;
                            cell.neighbours[0].surroundingWalls--;
                            //remove the cell from the list of cells that still need to be checked
                            listOfUncheckedCells.Remove(cell);
                        }
                        break;

                    case 1:
                        if (!cell.neighbours[1].hasBeenChecked)
                        {
                            cell.walls[1] = false;
                            cell.surroundingWalls--;

                            cell.neighbours[1].walls[3] = false;
                            cell.neighbours[1].surroundingWalls--;
                        }
                        break;

                    case 2:
                        if (!cell.neighbours[2].hasBeenChecked)
                        {
                            cell.walls[2] = false;
                            cell.surroundingWalls--;

                            cell.neighbours[2].walls[0] = false;
                            cell.neighbours[2].surroundingWalls--;
                        }
                        break;

                    case 3:
                        if (!cell.neighbours[3].hasBeenChecked)
                        {
                            cell.walls[3] = false;
                            cell.surroundingWalls--;

                            cell.neighbours[3].walls[1] = false;
                            cell.neighbours[3].surroundingWalls--;
                        }
                        break;
                }
            }
        }

        //for testing
        /*
        maze[0, 0].surroundingWalls = 3;
        maze[0, 0].walls[2] = false;

        maze[0, 1].surroundingWalls = 2;
        maze[0, 1].walls[0] = false;
        maze[0, 1].walls[2] = false;

        maze[0, 2].surroundingWalls = 1;
        maze[0, 2].walls[0] = false;
        maze[0, 2].walls[1] = false;
        maze[0, 2].walls[2] = false;

        maze[0, 3].surroundingWalls = 3;
        maze[0, 3].walls[0] = false;

        maze[1, 0].surroundingWalls = 3;
        maze[1, 0].walls[2] = false;

        maze[1, 1].surroundingWalls = 2;
        maze[1, 1].walls[0] = false;
        maze[1, 1].walls[1] = false;

        maze[1, 2].surroundingWalls = 1;
        maze[1, 2].walls[1] = false;
        maze[1, 2].walls[2] = false;
        maze[1, 2].walls[3] = false;

        maze[1, 3].surroundingWalls = 2;
        maze[1, 3].walls[0] = false;
        maze[1, 3].walls[1] = false;

        maze[2, 0].surroundingWalls = 3;
        maze[2, 0].walls[2] = false;

        maze[2, 1].surroundingWalls = 0;
        //maze[2, 1].walls[0] = false;
        //maze[2, 1].walls[2] = false;
        //maze[2, 1].walls[3] = false;

        maze[2, 2].surroundingWalls = 2;
        maze[2, 2].walls[0] = false;
        maze[2, 2].walls[3] = false;

        maze[2, 3].surroundingWalls = 3;
        maze[2, 3].walls[3] = false;

        maze[3, 1].surroundingWalls = 3;
        maze[3, 1].walls[3] = false;
        */
        return maze;
    }

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

    private void CheckCell(Cell cell)
    {
        do
        {

        } while (true);
    }

    private void SetNeighbours(Cell cellToCheck)
    {
        int h = cellToCheck.heightPos;
        int w = cellToCheck.widthPos;

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

    //create maze full of walls
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
