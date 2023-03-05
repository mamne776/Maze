using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
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

        Cell[,] maze = CreateMazeWithOnlyWalls(height, width);



        //for testing
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


        return maze;

        /*
        Cell[,] testingMaze = new Cell[height, width];
        Cell firstCell = new Cell();

        firstCell.heightPos = 0;
        firstCell.widthPos = 0;
        firstCell.walls[1] = false;
        firstCell.walls[2] = false;
        firstCell.surroundingWalls = 2;

        Cell secondCell = new Cell();
        secondCell.heightPos = 0;
        secondCell.widthPos = 1;
        secondCell.walls[0] = false;
        secondCell.walls[1] = false;
        secondCell.surroundingWalls = 2;

        Cell thirdCell = new Cell();
        thirdCell.heightPos = 1;
        thirdCell.widthPos = 0;
        thirdCell.walls[2] = false;
        thirdCell.walls[3] = false;
        thirdCell.surroundingWalls = 2;

        Cell fourthCell = new Cell();
        fourthCell.heightPos = 1;
        fourthCell.widthPos = 1;
        fourthCell.walls[0] = false;
        fourthCell.walls[3] = false;
        fourthCell.surroundingWalls = 2;

        Cell fifthCell = new Cell()
        {
            heightPos = 0,
            widthPos = 2
        };
        fifthCell.walls[0] = false;
        fifthCell.walls[2] = false;
        fifthCell.surroundingWalls = 2;

        Cell sixthCell = new Cell()
        {
            heightPos = 1,
            widthPos = 2
        };
        sixthCell.walls[1] = false;
        sixthCell.walls[3] = false;
        sixthCell.surroundingWalls = 2;
        

        testingMaze[0, 0] = firstCell;
        testingMaze[0, 1] = fifthCell;
        testingMaze[1, 0] = thirdCell;
        testingMaze[1, 1] = fourthCell;
        testingMaze[0, 2] = secondCell;
        testingMaze[1, 2] = sixthCell;

        return testingMaze;
        */
    }

    //create maze full of walls
    private Cell[,] CreateMazeWithOnlyWalls(int h, int w) //h = height, w = width
    {
        Cell[,] maze = new Cell[h, w];
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                Cell cell = new Cell() {heightPos = i, widthPos = j};
                maze[i, j] = cell;
            }
        }
        return maze;
    }
}
