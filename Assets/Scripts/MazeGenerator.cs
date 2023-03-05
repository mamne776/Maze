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
    }

}
